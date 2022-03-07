using System;
using System.Collections;
using System.Collections.Generic;
using cakeslice;
using UnityEngine;

public class ADSQuestMechanic : MonoBehaviour
{
    private Weapon Weapon;
    
    [SerializeField] private Outline paintingOutline;
    
    private void Start()
    {
        Weapon = GetComponent<Weapon>();
    }

    private void Update()
    {
        if (Weapon.GetSightState() == Weapon.WeaponSightState.Ads)
        {
            paintingOutline.eraseRenderer = false;
        }
        else
        {
            paintingOutline.eraseRenderer = true;
        }
    }
}
