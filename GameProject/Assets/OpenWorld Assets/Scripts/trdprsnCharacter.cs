using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trdprsnCharacter : MonoBehaviour
{

    [SerializeField] float movespeed;
    [SerializeField] float rotatespeed;
    private float rotX;
    private float rotY;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Playerturn();
    }

    private void FixedUpdate()
    {
        Playermovement();
    }

    void Playermovement()
    {
        //forward and back controls
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("w")) // if w and shift are pressed
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movespeed * 2.5f;
        }
        else if (Input.GetKey("w") && !Input.GetKey(KeyCode.LeftShift)) // if only w is pressed
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movespeed;
        }
        else if (Input.GetKey("s") && !Input.GetKey(KeyCode.LeftShift)) // if only s is pressed
        {
            transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * movespeed;
        }

        //left and right controls
        if (Input.GetKey("a") && !Input.GetKey("d")) // if only a is pressed
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * movespeed;
        }
        else if (Input.GetKey("d") && !Input.GetKey("a")) // if only d is pressed
        {
            transform.position -= transform.TransformDirection(Vector3.left) * Time.deltaTime * movespeed;
        }

    }

    void Playerturn()
    {
        //rotX += Input.GetAxis("Mouse Y") * Time.deltaTime * rotatespeed;
        //rotY += Input.GetAxis("Mouse X") * Time.deltaTime * rotatespeed;

        //transform.rotation = Quaternion.Euler(rotX, rotY, 0);
        //cam.transform.rotation = Quaternion.Euler(rotX, rotY, 0);

        transform.localEulerAngles = new Vector3(Camera.main.transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z); // rotates the player to always face in the direction of the camera
    }
}
