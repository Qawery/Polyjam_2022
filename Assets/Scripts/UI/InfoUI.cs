using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Polyjam_2022
{
    public class InfoUI : MonoBehaviour
    {
        [Inject] private Image image = null;
        [Inject] private TextMeshProUGUI text = null;

        private void Update()
        {
            image.enabled = true;
            var sb = new StringBuilder();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit, float.MaxValue))
            {
                var hitGameObject = hit.collider.gameObject;
                var unit = hitGameObject.GetComponentInParent<Unit>();
                if (unit != null)
                {
                    sb.AppendLine("Unit");
                    sb.AppendLine();
                    PrintResources(sb, unit.Resources);
                }

                var building = hitGameObject.GetComponentInParent<Building>();
                if (building != null)
                {
                    sb.AppendLine(building.BuildingData.BuildingName);
                    sb.AppendLine();
                    PrintResources(sb, building.Resources);
                    sb.AppendLine();
                    sb.AppendLine($"Health: {building.HealthPoints.CurrentValue}/{building.HealthPoints.MaxValue}");
                    sb.AppendLine();
                    sb.AppendLine("Production Data:");
                    foreach (var recipe in building.BuildingData.Recipes)
                    {
                        sb.Append("Consumes: [");
                        foreach (var requirement in recipe.ConsumptionData)
                        {
                            sb.Append($"{requirement.RequiredAmount} {requirement.ResourceType}, ");
                        }
                        sb.AppendLine("]");
                        sb.Append("Produces: [");
                        foreach (var productionData in recipe.ProductionData)
                        {
                            sb.Append($"{productionData.ProducedAmount} ");
                            if (productionData.ProductionType == ProductionType.Unit)
                            {
                                sb.Append("units, ");
                            }
                            else
                            {
                                sb.Append($"{productionData.ProducedResource}, ");
                            }
                        }

                        sb.AppendLine($"]");
                        sb.AppendLine($"Every {recipe.ProductionCooldown} seconds.");
                    }
                }
                
                var constructionSite = hitGameObject.GetComponentInParent<ConstructionSite>();
                if (constructionSite != null)
                {
                    sb.AppendLine($"{constructionSite.BuildingData.BuildingName} Construction Site");
                    sb.Append("Needs: [");
                    foreach (var requirement in constructionSite.BuildingData.ConstructionResourceRequirements)
                    {
                        sb.Append($"{requirement.RequiredAmount} {requirement.ResourceType}, ");
                    }
                    sb.AppendLine("]");
                    PrintResources(sb, constructionSite.Resources);
                }
                
                var resourceSource = hitGameObject.GetComponentInParent<ResourceSource>();
                if (resourceSource != null)
                {
                    sb.AppendLine("Resource site");
                    sb.AppendLine();
                    PrintResources(sb, resourceSource.Resources);
                }
            }
            
            text.text = sb.ToString();
            image.enabled = text.text != "";
        }

        private void PrintResources(StringBuilder sb, ResourceManager resources)
        {
            sb.AppendLine($"Resources in inventory: {resources.CurrentTotalAmount}/{resources.MaxCapacity}");
            sb.AppendLine();
            sb.AppendLine("Resources:");
            foreach (var (type, amount) in resources.GetAllCurrentAmountsPerType())
            {
                sb.AppendLine($"\t{type}: {amount}");
            }
        }
    }
}