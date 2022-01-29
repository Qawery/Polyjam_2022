using UnityEngine;

namespace Polyjam_2022
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField, Range(0.0f, 1000.0f)] private float movementSpeed;
        [SerializeField, Range(0.0f, 1000.0f)] private float zoomSpeed;

        private void Update()
        {
            Vector3 movementDirection = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                movementDirection.z += movementSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                movementDirection.z -= movementSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.A))
            {
                movementDirection.x -= movementSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                movementDirection.x += movementSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.E))
            {
                movementDirection.y += zoomSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                movementDirection.y -= zoomSpeed * Time.deltaTime;
            }

            var newPosition = transform.position + movementDirection;

            newPosition.y = Mathf.Clamp(newPosition.y, 2.0f, 10.0f);
            newPosition.x = Mathf.Clamp(newPosition.x, -10.0f, 10.0f);
            newPosition.z = Mathf.Clamp(newPosition.z, -10.0f, 10.0f);

            transform.position = newPosition;
        }
    }
}