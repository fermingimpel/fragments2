using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [SerializeField] bool overDoor;
    [SerializeField] bool canOpenDoor;

    [SerializeField] Key keyToOpenDoor;

    [SerializeField] AudioClip openDoorSound;
    [SerializeField] AudioClip lockedDoorSound;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject textOpenPivot;

    [SerializeField] Animator animator;

    private PlayerController player;

    void Start() {
        player = FindObjectOfType<PlayerController>();
    }

    void Update() {
        if (PauseController.instance.IsPaused)
            return;

        if (!canOpenDoor)
            return;

        if (overDoor) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (player.GetEquippedItem() && player.GetEquippedItem().itemInfo.item.name == keyToOpenDoor.itemInfo.item.name)
                    OpenDoor();
                else
                    LockedDoor();
            }
            textOpenPivot.transform.LookAt(player.transform.position);
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
        keyToOpenDoor.UsedItem();
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
