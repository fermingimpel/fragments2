﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    [SerializeField] bool canUseKey;
    [SerializeField] bool canPickUp;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject textPickUpPivot;
    [SerializeField] Transform player;

    //[SerializeField] List<Transform> keySpawnPositions;

    void Start() {
        player = FindObjectOfType<PlayerController>().transform;
    }

    void Update() {
        if (PauseController.instance.IsPaused)
            return;

        if (canPickUp) {
            textPickUpPivot.transform.LookAt(player.position);
            if(Input.GetKeyDown(KeyCode.E))
                PickUp();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            canPickUp = true;
            textPickUpPivot.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            canPickUp = false;
            textPickUpPivot.SetActive(false);
        }
    }

    void PickUp() {
        canPickUp = false;
        canUseKey = true;
        audioSource.Play();
        transform.position = new Vector3(0, -999, 0);
    }

    //public void SpawnKey() {
    //    transform.position = keySpawnPositions[Random.Range(0, keySpawnPositions.Count)].position;
    //}

    public void UseKey() {
        canUseKey = false;
        Destroy(this.gameObject, 0.1f);
    }

    public bool GetCanUseKey() {
        return canUseKey;
    }

}
