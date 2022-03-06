using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAccesoryItem : ItemBase
{
    public override void HandleInteraction()
    {
    }

    public override void HandleInteraction(PlayerController player)
    {
        //Activar accesorio
        gameObject.SetActive(false);
    }
}
