using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public string interactionID;
    // Start is called before the first frame update
    void Start()
    {
        //warn
        if(interactionID == "") throw new Exception("Interactive Object: " + gameObject.name + " has an empty InteractionID. This may cause unintended behavior during interaction. Please give this object an ID.");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
