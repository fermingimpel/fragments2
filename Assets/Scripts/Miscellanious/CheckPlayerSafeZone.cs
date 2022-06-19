using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckPlayerSafeZone : MonoBehaviour {
    public bool isPlayerIn = false;
    Transform player;
    public static Action<bool> PlayerInteract;
    HordeManager horde;
    void Start() {
        player = FindObjectOfType<PlayerController>().transform;
        horde = FindObjectOfType<HordeManager>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.transform == player) {
            isPlayerIn = true;
            PlayerInteract?.Invoke(isPlayerIn);
            FindObjectOfType<AudioManager>().StopMusic();
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.transform == player) {
            isPlayerIn = false;
            PlayerInteract?.Invoke(isPlayerIn);
            if(horde.GetIsInHorde())
                FindObjectOfType<AudioManager>().StartFightMusic();
            else
                FindObjectOfType<AudioManager>().StartAmbientMusic();
        }
    }

}
