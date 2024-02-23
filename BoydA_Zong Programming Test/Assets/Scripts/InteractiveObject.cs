//Code by Adam Boyd for Zong Programming Test

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    #region ATTRIBUTES

    #region Public Attribuites
        public string interactionID;
    #endregion Public Attribuites

    #region Private Attributes
    private List<string> InteractBehaviors; 
    #endregion Private Attributes
    
    #endregion ATTRIBUTES

    #region METHODS

    #region MonoBehavior Methods
    // Start is called before the first frame update
    void Start()
    {
        //throw a warning if the ID field is blank
        if(interactionID == "") throw new Exception("Interactive Object: " + gameObject.name + " has an empty InteractionID. This may cause unintended behavior during interaction. Please give this object an ID.");

        //Set interact behaviors
        InteractBehaviors = new List<string>();
        if(gameObject.GetComponent<IO_Pickup>() != null) InteractBehaviors.Add("Pickup");
        if(gameObject.GetComponent<IO_Deposit>() != null) InteractBehaviors.Add("Deposit");
    }
    #endregion MonoBehavior Methods

    #region Virtual Methods
    public virtual void StartInteract(PlayerController player)
    {
        
    }
    #endregion Virtual Methods

    #endregion METHODS
}
