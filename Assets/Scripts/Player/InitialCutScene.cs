using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialCutScene : CutScene {

    [SerializeField] PlayerHUD hud;

    protected override void Update() {
        if (!runningCutScene)
            return;

        if(blackScreen.color.a > 0) {
            alpha -= Time.deltaTime * screenFadeSpeed;
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, alpha);
        }
    }

    public void StartInitialCutScene() {
        CutSceneRunning?.Invoke(true);
        animator.SetTrigger("PlayInitialCutscene");
        alpha = blackScreen.color.a;
        runningCutScene = true;
    }
    public void PlayInitialAudio() {
        hud.SetEnabledInitialText(true);
    }

    public void InitialCutSceneEnded() {
        hud.SetEnabledInitialText(false);
        CutSceneRunning?.Invoke(false);
        enabled = false;
    }
}
