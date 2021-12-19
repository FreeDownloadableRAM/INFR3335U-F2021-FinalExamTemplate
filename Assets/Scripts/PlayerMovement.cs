using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    //Reference the character controller
    public CharacterController controller;

    //reference the camera
    //public Transform cam;

    public Camera cam;

    //camera brain
    public CinemachineFreeLook brain;

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

    //finding tag
    GameObject joystickReference;
    GameObject cameraReference;
    GameObject cinemachineReference;

    //camera look at
    //private float camDirX, camDirZ, camDirY;

    //Photon
    public PhotonView view;
    public PhotonTransformViewClassic transformView;

    // Start is called before the first frame update
    void Start()
    {
        //get rigidbody component
        //rb = GetComponent<Rigidbody>();

        //Photon
        //view = GetComponentInParent<PhotonView>();

        //transformView = GetComponentInParent<PhotonTransformViewClassic>();

        view = GetComponent<PhotonView>();
        transformView = GetComponent<PhotonTransformViewClassic>();
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
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * movementSpeed * Time.deltaTime);

            }
        }

        
    }

   
    //Set data section
    public void SetData(GameObject cameraParent)
    {
        //get and set references
        joystickReference = GameObject.FindWithTag("Movement Joystick");
        cameraReference = GameObject.FindWithTag("MainCamera");
        cinemachineReference = GameObject.FindWithTag("Cinemachine Camera");

        //set
        movementJoystick = joystickReference.GetComponent<Joystick>();
        /*
        cam = cameraReference.GetComponent<Camera>();
        CinemachineFreeLook brain = cinemachineReference.GetComponent<CinemachineFreeLook>();
        brain.LookAt = this.transform;
        brain.Follow = this.transform;
        */
        //movementJoystick[] tempJoystickList = cameraParent.GetComponent


        //movementJoystick = cameraParent.transform.Find("Canvas").GetComponentInChildren<Joystick>();
        //movementJoystick = cameraParent.transform.GetChild(2).GetComponentInChildren<Joystick>();
        //movementJoystick = cameraParent.GameObject.FindGameObjectsWithTag("Movement Joystick");
        //Joystick movementJoystick = cameraParent.GetComponentInChildren<Joystick>();
        
        cam = cameraParent.GetComponentInChildren<Camera>();
        brain = cameraParent.GetComponentInChildren<CinemachineFreeLook>();
        //CinemachineFreeLook brain = cameraParent.GetComponentInChildren<CinemachineFreeLook>();
        brain.LookAt = this.transform;
        brain.Follow = this.transform;
        
        //CinemachineVirtualCamera brain = cameraParent.GetComponentInChildren<CinemachineVirtualCamera>();


    }



}
