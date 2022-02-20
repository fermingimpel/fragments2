using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeCutScene : CutScene {
   protected override void Update() {
        if (!runningCutScene)
            return;
        if(blackScreen.color.a < 1.0f) {
            alpha += Time.deltaTime * screenFadeSpeed;
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, alpha);
        }
    }

    public void EscapeStartCutScene() {
        CutSceneRunning?.Invoke(true);
        animator.SetTrigger("Escape");
        alpha = 0;
        runningCutScene = true;
    }

    public void PlayEscapeAudio() {

    }

    public void EscapeCutSceneEnded() {
        CutSceneRunning?.Invoke(false);
        SceneController.instance.LoadScene("End");
        enabled = false;
    }
}
