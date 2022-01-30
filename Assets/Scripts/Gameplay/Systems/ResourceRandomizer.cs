using System;
using System.Collections.Generic;
using Lifecycle;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Polyjam_2022
{
    [Serializable]
    public class ResourceDensity
    {
        public ResourceSource Source;
        public float AverageDensityOverSquareMeter = 0.1f;
    }
    
    public class ResourceRandomizer : MonoBehaviour
    {
        [SerializeField] private List<ResourceDensity> resourceDensitySettings = null;
        [SerializeField] private BoxCollider boundsCollider = null;
        [SerializeField] private Transform resourceParent = null;

        [Inject] private IWorld world = null;
        
        public void Randomize()
        {
            Vector3 boundsColliderPos = boundsCollider.transform.position;
            Vector3 min = boundsColliderPos - 0.5f * boundsCollider.size;
            Vector3 max = boundsColliderPos + 0.5f * boundsCollider.size;
            Vector3 dif = max - min;
            float totalArea = dif.x * dif.z;

            foreach (var densitySetting in resourceDensitySettings)
            {
                int totalAmount = (int) (densitySetting.AverageDensityOverSquareMeter * totalArea);
                for (int i = 0; i < totalAmount; ++i)
                {
                    world.Instantiate(densitySetting.Source,
                        new Vector3(Random.Range(min.x, max.x), 0, Random.Range(min.z, max.z)), Quaternion.identity,
                        resourceParent);
                }
            }
        }
    }
}