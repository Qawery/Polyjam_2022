using UnityEngine;
using UnityEngine.Assertions;

namespace Polyjam_2022
{
    public class ResourceSource : MonoBehaviour, IResourceHolder
    {
        [SerializeField, Range(0.0f, 1000.0f)] private float maxCapacity = 0.0f;
        [SerializeField, Range(0.0f, 1000.0f)] private float startingAmmount = 0.0f;

        public Resources Resources { get; private set; }

        private void Awake()
        {
            Assert.IsTrue(startingAmmount <= maxCapacity);
            Resources = new Resources(maxCapacity, startingAmmount);
        }
    }
}