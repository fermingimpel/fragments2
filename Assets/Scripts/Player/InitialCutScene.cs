using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialCutScene : CutScene {
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
        audioSource.PlayOneShot(dialogue);
    }

    public void InitialCutSceneEnded() {
        CutSceneRunning?.Invoke(false);
        enabled = false;
    }
}
