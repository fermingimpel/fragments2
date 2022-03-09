using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITutorial : MonoBehaviour {

    bool tutorialEnabled = false;
    [SerializeField] GameObject tutorialObject;

    void Awake() {
        WeaponAccesoryItem.WeaponPickedUp += EnableTutorial;
    }

    void OnDisable() {
        WeaponAccesoryItem.WeaponPickedUp -= EnableTutorial;
    }

    void Update() {
        if (!tutorialEnabled)
            return;

        if (Input.GetKeyDown(KeyCode.E))
            EndTutorial();

    }

    void EnableTutorial() {
        Debug.Log("chuchetumare");
        tutorialEnabled = true;
        tutorialObject.SetActive(true);
        PauseController.instance.ExternalPause();
    }

    public void EndTutorial() {
        PauseController.instance.RemovePause();
        gameObject.SetActive(false);
    }
}
