using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : ItemBase {

    [SerializeField] AudioClip pickUpClip;
    
    private PlayerController player;

    public override void HandleInteraction(PlayerController player)
    {
        this.player = player;
    }

    public override void HandleInteraction()
    {
        FindObjectOfType<PlayerController>().PlayClip(pickUpClip);
        base.HandleInteraction();
    }


    public override void UseItem() {
        player.SetEquippedItem(this);
    }

    public override void UnequippedItem() {
        if (player)
            player.SetEquippedItem(null);
    }

}
