using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    //Reference the character controller
    public CharacterController controller;

    //reference the camera
    public Transform cam;

    //Initializes
    public Joystick movementJoystick;
    //public Joystick cameraJoystick;

    public float movementSpeed;
    //public float viewSpeed;
    public float jumpForce;

    //set rigidbody
    //private Rigidbody rb;
    
    private float dirX, dirZ, dirY;
    float turnSmoothVelocity;
    public float turnSmoothTime;

    //camera look at
    //private float camDirX, camDirZ, camDirY;

    //Photon
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        //get rigidbody component
        //rb = GetComponent<Rigidbody>();

        //Photon
        view = GetComponent<PhotonView>();

    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            //movement

            dirX = movementJoystick.Horizontal * movementSpeed;
            dirZ = movementJoystick.Vertical * movementSpeed;

            //camDirX = cameraJoystick.Horizontal * viewSpeed;
            //camDirZ = cameraJoystick.Vertical * viewSpeed;

            float horizontal = dirX;
            float vertical = dirZ;

            //store the direction
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                //rotate to point towards where we are going
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * movementSpeed * Time.deltaTime);

            }
        }

        
    }

}
