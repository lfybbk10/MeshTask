using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float _moveSpeed = 5f;       
    private float _lookSpeed = 2f;       
    private bool _isControlling; 

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _isControlling = !_isControlling;
        }

        if (_isControlling)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");  

            Vector3 move = transform.right * h + transform.forward * v;
            transform.position += move * _moveSpeed * Time.deltaTime;
            
            float mouseX = Input.GetAxis("Mouse X") * _lookSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * _lookSpeed;

            transform.Rotate(Vector3.up, mouseX, Space.World); 
            transform.Rotate(Vector3.right, -mouseY); 
        }
    }
}