using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Polyjam_2022
{
	public class Building : MonoBehaviour, IResourceHolder
	{
		[Inject] private IUnitSpawner unitSpawner = null;
		private BuildingData buildingData;
		private readonly List<(Recipe recipe, float timer)> recipeTimers = new List<(Recipe recipe, float timer)>();

		public BuildingData BuildingData
		{
			set
			{
				Resources = new ResourceManager(value.ResourceCapacity, 
					value.Recipes.SelectMany(recipe =>
						recipe.ConsumptionData.Select(data => data.ResourceType)
							.Union(recipe.ProductionData.Select(data => data.ProducedResource))));
				buildingData = value;
				foreach (var recipe in buildingData.Recipes)
				{
					recipeTimers.Add((recipe, recipe.ProductionCooldown));
				}
			}
			get => buildingData;
		}

		public ResourceManager Resources { get; private set; }

		private void Update()
		{
			for(int i = 0; i < recipeTimers.Count; ++i)
			{
				var (recipe, timer) = recipeTimers[i];
				timer -= Time.deltaTime;
				if (timer < 0)
				{
					if (recipe.CheckResources(Resources))
					{
						recipe.ConsumeResources(Resources);
						Produce(recipe);
						recipeTimers[i] = (recipe, recipe.ProductionCooldown);
					}
					else
					{
						recipeTimers[i] = (recipe, timer);
					}
				}
			}
		}

		private void Produce(Recipe recipe)
		{
			foreach (ProductionData productionData in recipe.ProductionData)
			{
				switch (productionData.ProductionType)
				{
					case ProductionType.Unit:
					{
						for (int i = 0; i < productionData.ProducedAmount; ++i)
						{
							unitSpawner.QueueUnitSpawn();
						}
						break;
					}
					case ProductionType.Resource:
					{
						Resources.InsertResource(productionData.ProducedResource, productionData.ProducedAmount);
						break;
					}
					default:
					{
						throw new ArgumentOutOfRangeException();
					}
				}
			}
		}
	}
}