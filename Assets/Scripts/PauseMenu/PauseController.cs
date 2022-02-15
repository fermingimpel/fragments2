﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseController : MonoBehaviour {

    [SerializeField] GameObject pauseMenu;
    public static PauseController instance;

    public bool IsPaused;

    public static Action Pause;
    
    void Awake() {
        CutScene.CutSceneRunning += PauseInteractions;
        PlayerController.PlayerDead += EndGame;
        if (instance != null) {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    void OnDisable() {
        CutScene.CutSceneRunning -= PauseInteractions;
        PlayerController.PlayerDead -= EndGame;
    }
    void OnDestroy() {
        CutScene.CutSceneRunning -= PauseInteractions;
        PlayerController.PlayerDead -= EndGame;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) && !IsPaused) {
            Time.timeScale = 0.0f;
            IsPaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseMenu.SetActive(true);
            Pause?.Invoke();
        }
    }

    public void RemovePause() {
        IsPaused = false;
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Pause?.Invoke();
    }

    void EndGame() {
        IsPaused = true;
        Pause?.Invoke();
    }

    void PauseInteractions(bool value) {
        IsPaused = value;
        Pause?.Invoke();
    }

}
