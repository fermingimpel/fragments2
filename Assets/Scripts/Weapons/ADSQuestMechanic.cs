using System;
using System.Collections;
using System.Collections.Generic;
using cakeslice;
using UnityEngine;

public class ADSQuestMechanic : MonoBehaviour
{
    private Weapon Weapon;
    
    [SerializeField] private Outline paintingOutline;
    [SerializeField] Transform painting;
    [SerializeField] Transform player;
    [SerializeField] float distance;

    private void Start()
    {
        Weapon = GetComponent<Weapon>();
        player = FindObjectOfType<PlayerController>().transform;
    }

    void FixedUpdate()
    {
        Vector3 dir = (player.position - painting.position).normalized;
        float dotProd = Vector3.Dot(dir, painting.forward);

        if (Weapon.GetSightState() == Weapon.WeaponSightState.Ads && dotProd >= 0f && Vector3.Distance(painting.position, player.position) <= distance)
        {
            paintingOutline.eraseRenderer = false;
        }
        else
        {
            paintingOutline.eraseRenderer = true;
        }
    }
}
