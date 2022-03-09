using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponAccesoryItem : ItemBase
{

    public static Action WeaponPickedUp;

    public override void HandleInteraction()
    {
    }

    public override void HandleInteraction(PlayerController player)
    {
        WeaponPickedUp?.Invoke();
        player.GetActualWeapon().EnableAccesories();
        gameObject.SetActive(false);
    }
}
