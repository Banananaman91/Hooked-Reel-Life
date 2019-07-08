using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    //private float rotX;
    //private float rotY;
    [SerializeField] private float _rotZ;
    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        PlayerTurn(); // turn to face the player
        PlayerRotate(); // rotate the player itself
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        //forward and back controls
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("w")) // if w and shift are pressed
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * _moveSpeed * 2.5f;
        }
        else if (Input.GetKey("w") && !Input.GetKey(KeyCode.LeftShift)) // if only w is pressed
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * _moveSpeed;
        }
        else if (Input.GetKey("s") && !Input.GetKey(KeyCode.LeftShift)) // if only s is pressed
        {
            transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * _moveSpeed;
        }

        //left and right controls
        if (Input.GetKey("a") && !Input.GetKey("d")) // if only a is pressed
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * _moveSpeed;
        }
        else if (Input.GetKey("d") && !Input.GetKey("a")) // if only d is pressed
        {
            transform.position -= transform.TransformDirection(Vector3.left) * Time.deltaTime * _moveSpeed;
        }

    }

    void PlayerTurn() // EXPERIMENTING ATM
    {
        //rotX += Input.GetAxis("Mouse Y") * Time.deltaTime * rotatespeed;
        //rotY += Input.GetAxis("Mouse X") * Time.deltaTime * rotatespeed;

        //transform.rotation = Quaternion.Euler(rotX, rotY, 0);
        //cam.transform.rotation = Quaternion.Euler(rotX, rotY, 0);

        transform.localEulerAngles = new Vector3(Camera.main.transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z); // rotates the player to always face in the direction of the camera
    }

    void PlayerRotate()
    {
        // if q is pressed rotate body left
        if (Input.GetKey("q"))
        {
            transform.Rotate(new Vector3(0, 0, _rotZ) * Time.deltaTime * _rotateSpeed);
        }
        // if r is pressed rotate body right
        if (Input.GetKey("e"))
        {
            transform.Rotate(new Vector3(0, 0, -_rotZ) * Time.deltaTime * _rotateSpeed);
        }
    }
}
