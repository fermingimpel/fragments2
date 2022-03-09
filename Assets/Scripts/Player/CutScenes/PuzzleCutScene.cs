using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCutScene : CutScene {

    [SerializeField] PlayerHUD hud;

    void Awake() {
        BellPuzzle.BellPuzzleCompleted += StartPuzzleCutScene;
    }

    void OnDisable() {
        BellPuzzle.BellPuzzleCompleted -= StartPuzzleCutScene;

    }

    protected override void Update() {
        if(!runningCutScene)
            return;

        if (blackScreen.color.a > 0) {
            alpha -= Time.deltaTime * screenFadeSpeed;
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, alpha);
        }
    }

    public void StartPuzzleCutScene() {
        CutSceneRunning?.Invoke(true);

        FindObjectOfType<PlayerController>().transform.position = new Vector3(11.707f, 6.42f, 12.823f);
        FindObjectOfType<PlayerController>().transform.eulerAngles = new Vector3(0,-180,0);
        FindObjectOfType<PlayerCameraMovement>().transform.localEulerAngles = Vector3.zero;

        animator.SetTrigger("PlayBellPuzzleCutScene");
        alpha = 1f;
        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, alpha);
        runningCutScene = true;
    }

    public void PuzzleCutSceneEnded() {
        hud.SetBellPuzzleTextEnabled(false);
        CutSceneRunning?.Invoke(false);
        enabled = false;
    }

    public void PlayText() {
        hud.SetBellPuzzleTextEnabled(true);
    }
}
