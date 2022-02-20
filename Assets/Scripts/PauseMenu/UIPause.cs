using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPause : MonoBehaviour {

    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject howToPlayPanel;

    public void ClickBack() {
        pausePanel.SetActive(true);
        optionsPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
    }

    public void ClickHowToPlay() {
        pausePanel.SetActive(false);
        howToPlayPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void ClickOptions() {
        pausePanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (pausePanel.activeSelf)
                PauseController.instance.RemovePause();
            else
                ClickBack();
        }
    }


    public void ClickExit() {
        Application.Quit();
    }

}
