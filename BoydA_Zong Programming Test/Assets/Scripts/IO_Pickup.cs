//Code by Adam Boyd for Zong Programming Test

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IO_Pickup : InteractiveObject
{
    PlayerController playerController;

    #region METHODS
public override void StartInteract(PlayerController player)
    {
        base.StartInteract(player);
        playerController = player;
        Pickup();
    }
    public void Pickup()
    {
        Debug.Log("Pickup called");
        playerController.hud.gameObject.SetActive(true);
        playerController.bMenuOpen = true;
        playerController.bPlayerHasPickup = true;
        playerController.SetCursorToMovementMode(false);

        gameObject.SetActive(false);
    }

    #endregion METHODS

}
