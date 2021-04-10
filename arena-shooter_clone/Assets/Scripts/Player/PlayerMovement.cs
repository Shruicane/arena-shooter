using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;


//Networkbehaviour inherits from MonoBehaviour
public class PlayerMovement : NetworkedBehaviour
{
    private CharacterController characterController;
    public Transform cameraTransform;
    public Vector3 move = new Vector3(0, 0, 0);
    private float sprintSpeed = 3f;
    private float walkSpeed = 2f;
    private float pitch;
    private bool isSprinting;
    
    public float gravity = -2.5f;
    public float jumpForce = 25.0f;
    public float jumpHeight = 2.5f;
    
    public GameObject capsule;


    // Start is called before the first frame update
    void Start()
    {

        if (!IsLocalPlayer)
        {
            //disable audio and camera if the script is not executed on the local player
            cameraTransform.GetComponent<AudioListener>().enabled = false;
            cameraTransform.GetComponent<Camera>().enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer)
        {
            Jump();
            MovePlayer();  
            Look();
            characterController.Move(move * (isSprinting ? sprintSpeed/20 : walkSpeed/20));
        }
    }

    void Jump()
    {
        if (!PauseMenuScript.isPaused)
        {
            
            //is grounded not working, maybe because terrain?
            if (Input.GetButtonDown("Jump") && characterController.isGrounded)
            {
                move.y += Mathf.Sqrt(jumpHeight * jumpForce);
            }

            move.y += gravity * Time.deltaTime;
        }
    }
    
    void Look()
    {
        if (!PauseMenuScript.isPaused)
        {
            float mouseX = Input.GetAxis("Mouse X") * 3f;
            transform.Rotate(0, mouseX, 0);
            pitch -= Input.GetAxis("Mouse Y") * 3f;
            pitch = Mathf.Clamp(pitch, -90f, 90f);
            cameraTransform.localRotation = Quaternion.Euler(pitch, 0, 0);   
        }
    }

    void MovePlayer()
    {
        if (!PauseMenuScript.isPaused)
        {
            move.x = Input.GetAxis("Horizontal");
            move.z = Input.GetAxis("Vertical");
        }
        else
        {
            move.x = 0;
            move.z = 0;
        }

        move = Vector3.ClampMagnitude(move, 1f);
        move = transform.TransformDirection(move);
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) &&!Input.GetKey(KeyCode.D))
        {
            isSprinting = false;
        }
    
    }

    private void SetColor(Material color)
    {
        capsule.GetComponent<MeshRenderer>().material = color;
    }
}
