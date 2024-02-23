//Code by Adam Boyd for Zong Programming Test

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IO_Deposit : InteractiveObject
{
    #region ATTRIBUTES

    #region Public Attributes
    public enum DepositBehavior {ParticleEffect, ReturnToCheckpoint};
    public DepositBehavior depositBehavior;
    public ParticleSystem particles;
    public AudioSource audioSource;

    #endregion Public Attributes

    #region Private Attributes
    PlayerController playerController;

    #endregion Private Attribuites

    #endregion ATTRIBUTES

    #region METHODS
    
    public override void StartInteract(PlayerController player)
    {
        //set player reference
        playerController = player;

        if(player.bPlayerHasPickup == true) Deposit();
    }
    void Deposit()
    {
        Debug.Log("Deposit called");
        
        if(depositBehavior == 0) 
        {            
            particles.Play();
            playerController.hud.ShowSuccessMessage(interactionID);
            playerController.bPlayerHasPickup = false;
        }
        else 
        {
            audioSource.Play();
            ReturnToCheckpoint();
        }
        
    }

    void ReturnToCheckpoint()
    {
        //set player position to checkpoint location
        playerController.gameObject.transform.position = playerController.checkpointLocation.position;

        playerController.hud.ToggleUI(true);
        playerController.SetCursorToMovementMode(false);
        playerController.bMenuOpen = true;
    }

    #endregion METHODS
}
