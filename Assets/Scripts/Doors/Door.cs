using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [SerializeField] bool overDoor;
    [SerializeField] bool canOpenDoor;

    [SerializeField] Key keyToOpenDoor;
    [SerializeField] Transform player;

    [SerializeField] AudioClip openDoorSound;
    [SerializeField] AudioClip lockedDoorSound;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject textOpenPivot;

    [SerializeField] Animator animator;

    void Start() {
        player = FindObjectOfType<PlayerController>().transform;
    }

    void Update() {
        if (PauseController.instance.IsPaused)
            return;

        if (!canOpenDoor)
            return;

        if (overDoor) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (keyToOpenDoor.GetCanUseKey())
                    OpenDoor();
                else
                    LockedDoor();
            }
            textOpenPivot.transform.LookAt(player.position);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (!canOpenDoor)
            return;

        if (other.CompareTag("Player")) {
            overDoor = true;
            textOpenPivot.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other) {
        if (!canOpenDoor)
            return;

        if (other.CompareTag("Player")) {
            overDoor = false;
            textOpenPivot.SetActive(false);
        }
    }

    void OpenDoor() {
        keyToOpenDoor.UseKey();
        animator.Play("OpenDoor");
        audioSource.PlayOneShot(openDoorSound);
        canOpenDoor = false;
        textOpenPivot.SetActive(false);
        overDoor = false;
    }

    void LockedDoor() {
        audioSource.PlayOneShot(lockedDoorSound);
    }

}
