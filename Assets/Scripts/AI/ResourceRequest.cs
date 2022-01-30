using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyjam_2022
{
    public class ResourceRequest
    {
        public ResourceType ResourceType { get; set; } = 0;

        public IResourceLocation Destination { get; set; } = null;

        public int Priority { get; set; } = 0;

        public int Amount { get; set; } = 0;


    }
}