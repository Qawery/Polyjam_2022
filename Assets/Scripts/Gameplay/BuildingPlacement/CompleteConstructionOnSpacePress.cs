using UnityEngine;

namespace Polyjam_2022
{
    public class CompleteConstructionOnSpacePress : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<ConstructionSite>().CompleteConstruction();
            }
        }
    }
}