using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialCutScene : MonoBehaviour {

    [SerializeField] Image blackScreen;
    [SerializeField] float screenFadeSpeed;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip dialogue;

    [SerializeField] Animator animator;

    float alpha = 0;
    void Start() {
        animator.SetTrigger("PlayInitialCutscene");
        alpha = blackScreen.color.a;
        PauseController.instance.IsPaused = true;
    }

    void Update() {
        if(blackScreen.color.a > 0) {
            alpha -= Time.deltaTime * screenFadeSpeed;
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, alpha);
        }
    }

    public void PlayAudio() {
        audioSource.PlayOneShot(dialogue);
    }

    public void InitialCutSceneEnded() {
        PauseController.instance.IsPaused = false;
        Destroy(blackScreen);
        Destroy(this);
    }
}
