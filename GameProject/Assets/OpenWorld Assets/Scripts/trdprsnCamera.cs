using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trdprsnCamera : MonoBehaviour
{
    [SerializeField] Transform lookAt;
    [SerializeField] Transform camTransform; // DELETE THIS

    private Camera cam;

    private float distance = 15.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    //private float sensX = 4.0f;
    //private float sensY = 1.0f;


    private void Start()
    {
        camTransform = transform;
        cam = Camera.main;
    }
    private void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY -= Input.GetAxis("Mouse Y");
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
        // Rotate with the player when they rotate on their own z axis
        camTransform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);
    }
}
