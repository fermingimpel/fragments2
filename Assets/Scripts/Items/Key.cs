using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : ItemBase {

    [SerializeField] AudioSource audioSource;
    
    private PlayerController player;

    public override void HandleInteraction(PlayerController player)
    {
        this.player = player;
    }

    public override void HandleInteraction()
    {
        audioSource.Play();
        base.HandleInteraction();
    }


    public override void UseItem() {
        player.SetEquippedItem(this);
    }
    
}
