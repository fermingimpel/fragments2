using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bell : MonoBehaviour, PuzzleInteractable {

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip bellSound;

    [SerializeField] List<int> bellHitsID;

    public static Action<Bell> ShootedBell;


    void ShootBell() {
        ShootedBell?.Invoke(this);
        audioSource.PlayOneShot(bellSound);
    }

    public int GetBellHitId(int index) {
        if (index >= bellHitsID.Count || bellHitsID[index] == 0)
            return -1;

        return bellHitsID[index];
    }

    public void Interact() {
        ShootBell();
    }
}
