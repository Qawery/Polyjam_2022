using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class ResourceCost
    {
        public List<(ResourceType, int)> costPerResourceType = new List<(ResourceType, int)>();

        public bool CanPlayerPay(Player player)
        {
            foreach(var cost in costPerResourceType)
            {
                if(cost.Item2 <= player.GetResourceValue(cost.Item1))
                {
                    return false;
                }    
            }
            return true;
        }

        public void ApplyToPlayer(Player player)
        {
            foreach (var cost in costPerResourceType)
            {
                var currentValue = player.GetResourceValue(cost.Item1);
                Assert.IsTrue(cost.Item2 <= currentValue);
                player.SetResourceValue(cost.Item1, currentValue - cost.Item2);
            }
        }
    }
}
