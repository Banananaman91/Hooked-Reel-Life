using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    [SerializeField] private Transform _lookAt;
    [SerializeField] private Transform _camTransform;   // DELETE THIS


    private Camera _cam;

    private float _distance = 15.0f;    // DISTANCE TO STAY BEHIND PLAYER
    private float _currentX;
    private float _currentY;
    //private float _sensitivityX = 1.0f;
    //private float _sensitivityY = 1.0f;


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
        Vector3 _direction = new Vector3(0, 0, -_distance);
        Quaternion _rotation = Quaternion.Euler(_currentY, _currentX, 0);
        _camTransform.position = _lookAt.position + _rotation * _direction; 
        _camTransform.LookAt(_lookAt.position);

        // ROTATE WITH PLAYERS Z AXIS WHEN THE PLAYER ROTATES

        // _camTransform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);       /* da fuq even is dis? */
        // _camTransform.rotation = _lookAt.localRotation;      /* This works but obvs not what is needed */
        Vector3 _targetPosition = new Vector3(0, 0, _camTransform.localRotation.z);
        _camTransform.LookAt(_targetPosition);

    }
}
