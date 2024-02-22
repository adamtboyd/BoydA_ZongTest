//Code by Adam Boyd for Zong Programming Test

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerController : MonoBehaviour
{
    
    #region ATTRIBUTES

    #region Public Attributes
    public Camera playerCamera;
    public InteractiveObject focusedInteractiveObject;
    public float moveRate = 1f;
    public float turnRate = 200f;
    public float pitchRate = 200f;
    public float lookThreshold = 0.1f;
    public float interactionRange = 100f;
    
    #endregion

    #region Private Attributes
    float pawnRotation, camRotation = 0f;
    LayerMask interactionLayer;
    bool bCanInteract, bLookDirectionUpdated, bPawnPositionUpdated, bHitSuccessful = false;
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
        
        //get interaction layer mask index
        interactionLayer = LayerMask.GetMask("Interaction");
    }

    void Update()
    {
        //player motion update method calls
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Look(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    void FixedUpdate()
    {
        //only process raycast on change in look direction to save on resources
        if(bLookDirectionUpdated || bPawnPositionUpdated)
        {
            RaycastHit rayHit;
            //interactive object acquisition
            bHitSuccessful = Physics.Raycast(
                playerCamera.transform.position, //orgin
                playerCamera.transform.forward,  //direction
                out rayHit,                      //out hit result
                interactionRange,                //raycast length
                interactionLayer);               //layer mask to check
            
            //if his info is good, process hit and set interaction control bool, else set control bool to false
            if(bHitSuccessful) bCanInteract = ProcessInteractiveObjectHit(rayHit);
            else 
            {
                bCanInteract = false;
                if(focusedInteractiveObject != null)  focusedInteractiveObject = null;
            }
        }
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

        bPawnPositionUpdated = deltaPosition == new UnityEngine.Vector3() ? false : true;
        
        //apply delta vector adjusted by rate and normalized by frame timing
        if(bPawnPositionUpdated) transform.position += moveRate * Time.deltaTime * deltaPosition;

        return;
    }

    void Look(float LeftRight, float UpDown)
    { 
        //check input values against dead zone
        float pawnDeltaRotation = Math.Abs(LeftRight) > lookThreshold ? LeftRight * turnRate * Time.deltaTime : 0f;
        float camDeltaRotation = Math.Abs(UpDown) > lookThreshold ? UpDown * pitchRate * Time.deltaTime : 0f;
        
        //short circuit and return if both delta values are in the dead zone
        if(camDeltaRotation == 0f & pawnDeltaRotation == 0f)
        {
            bLookDirectionUpdated = false;
            return;
        }
        //else continue with method execution
        else
        {
            bLookDirectionUpdated = true;
        }

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

    /// <summary>
    /// Processes new RaycastHit information for the 'Interaction' layer mask. 
    /// Sets the local var 'focusedInteractiveObject's script reference if both of the following are true: 
    /// a) The hit object has a valid IntObj script. 
    /// b) That object is not already set as the focues IntObj. 
    /// If no valid InteractiveObject is found in the raycast, the local var 'focusedInteractiveObject' is nulled.
    /// </summary>
    /// <param name="L_RayHit"></param>
    /// <returns>A boolean where true indicates that the reference in focusedInteractiveObject has changed and false indicates that no change in that reference has occured. Useful in notifying UI events.</returns>
    bool ProcessInteractiveObjectHit(RaycastHit L_RayHit)
    {
        //if the reference is null and the hit object carries the InteractiveObject script, set the reference and return true
        if(focusedInteractiveObject == null & L_RayHit.transform.gameObject.GetComponent<InteractiveObject>() != null )
        {
            focusedInteractiveObject = L_RayHit.transform.gameObject.GetComponent<InteractiveObject>();
            return true;
        }
        //else return false
        else
        {
            //do nothing
            return false;
        }
    }
    #endregion Movement Methods

    #endregion METHODS
}
