//Code by Adam Boyd for Zong Programming Test

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    #region ATTRIBUTES

    #region Public Attributes
    public float moveRate = 1f;
    public float turnRate = 100f;
    public float pitchRate = 100f;
    public float lookThreshold = 0.1f;
    public PlayerInput pInput;
    public Camera playerCamera;
    #endregion

    #region Private Attributes
    public float pawnRotation, camRotation = 0f;
    #endregion Private Attributes

    #region Properties
    private float PawnRotation
    {
        get =>  pawnRotation;
        
        set //control accumulator var to simplify math
        {
            if(Math.Abs(value) > 360) pawnRotation = value % 360;
            pawnRotation = value;
        }
    }
    private float CamRotation{
        get => camRotation;
        
        set //constrain look angle to a 180 degree range
        {
            if(value > 90) camRotation = 90;
            else if (value < -90) camRotation = -90;
            else camRotation = value;
        }
    }
    #endregion Properties

    #endregion ATTRIBUTES

    #region METHODS

    #region MonoBehavior Methods
    void Start()
    {
        //lock cursor to screen and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //set starting rotation values
        PawnRotation = transform.rotation.y;
        CamRotation = playerCamera.transform.rotation.x;
    }

    void Update()
    {
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Look(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
    }
    #endregion MonoBehavior Methods

    #region Movement Methods
    void Move(float LeftRight, float ForwardBack)
    {
        //create and calculate delta vector for movement
        UnityEngine.Vector3 deltaPosition = new UnityEngine.Vector3 ();

        deltaPosition += 
            transform.right * LeftRight +    //x axis movement
            transform.forward * ForwardBack; //z axis movement
        
        //apply delta vector adjusted by rate and normalized by frame timing
        transform.position += moveRate * Time.deltaTime * deltaPosition;

        return;
    }

    void Look(float LeftRight, float UpDown)
    { 
        //check input values against dead zone
        float pawnDeltaRotation = Math.Abs(LeftRight) > lookThreshold ? LeftRight * turnRate * Time.deltaTime : 0f;
        float camDeltaRotation = Math.Abs(UpDown) > lookThreshold ? UpDown * pitchRate * Time.deltaTime : 0f;

        //Set left/right rotation of player pawn
        if(pawnDeltaRotation != 0f) 
        {
            PawnRotation += pawnDeltaRotation;
            transform.rotation = UnityEngine.Quaternion.Euler(0, PawnRotation, 0);
        }

        //Set up/down rotation of the player camera
        if(camDeltaRotation != 0f) 
        {
            CamRotation += camDeltaRotation;
            playerCamera.transform.rotation = UnityEngine.Quaternion.Euler(-CamRotation, playerCamera.transform.eulerAngles.y, playerCamera.transform.eulerAngles.z);
        }

        return;
    }
    #endregion Movement Methods

    #endregion METHODS
}
