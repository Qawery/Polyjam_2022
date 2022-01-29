using UnityEngine;

namespace Polyjam_2022
{
    public class SelectionManager : MonoBehaviour
    {
        private Unit selectedUnit;

        private void Update()
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    var clickedUnit = hitInfo.collider.gameObject.GetComponentInParent<Unit>();
                    selectedUnit = clickedUnit;
                }
                else if (Input.GetMouseButtonDown(1) && selectedUnit != null)
                {
                    selectedUnit.NavMeshAgent.SetDestination(hitInfo.point);
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    selectedUnit = null;
                }
            }
        }
    }
}