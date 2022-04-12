using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckPlayerSafeZone : MonoBehaviour {
    public bool isPlayerIn = false;
    Transform player;
    public static Action<bool> PlayerInteract;
    void Start() {
        player = FindObjectOfType<PlayerController>().transform;
    }

    void OnTriggerEnter(Collider other) {
        if (other.transform == player) {
            isPlayerIn = true;
            PlayerInteract?.Invoke(isPlayerIn);
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.transform == player) {
            isPlayerIn = false;
            PlayerInteract?.Invoke(isPlayerIn);
        }
    }

}
