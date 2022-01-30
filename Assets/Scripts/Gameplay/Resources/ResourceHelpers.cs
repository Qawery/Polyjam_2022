using System;
using System.Linq;
using System.Collections.Generic;

namespace Polyjam_2022
{
    public static class ResourceHelpers
    {
        public static List<ResourceType> GetAllTypes()
        {
            var result = new List<ResourceType>();
            foreach (var resourceType in Enum.GetValues(typeof(ResourceType)).Cast<ResourceType>())
            {
                result.Add(resourceType);
            }
            return result;
        }

        public static bool HasCommonResources(IEnumerable<ResourceType> first, IEnumerable<ResourceType> second)
        {
            return GetCommonResources(first, second).Count > 0;
        }

        public static HashSet<ResourceType> GetCommonResources(IEnumerable<ResourceType> first, IEnumerable<ResourceType> second)
        {
            var result = new HashSet<ResourceType>();
            foreach (var firstType in first)
            {
                foreach (var secondType in second)
                {
                    if (firstType == secondType)
                    {
                        result.Add(firstType);
                    }
                }
            }
            return result;
        }
    }
}