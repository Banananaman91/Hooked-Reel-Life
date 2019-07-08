using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    [SerializeField] private Transform _lookAt;
    [SerializeField] private Transform _camTransform; // DELETE THIS

    private Camera _cam;

    private float _distance = 15.0f;
    private float _currentX;
    private float _currentY;
    //private float sensX = 4.0f;
    //private float sensY = 1.0f;


    private void Start()
    {
        _camTransform = transform;
        _cam = Camera.main;
    }
    private void Update()
    {
        _currentX += Input.GetAxis("Mouse X");
        _currentY -= Input.GetAxis("Mouse Y");
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -_distance);
        Quaternion rotation = Quaternion.Euler(_currentY, _currentX, 0);
        _camTransform.position = _lookAt.position + rotation * dir;
        _camTransform.LookAt(_lookAt.position);
        // Rotate with the player when they rotate on their own z axis
        _camTransform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);
    }
}
