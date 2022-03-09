using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CutScene : MonoBehaviour {
    [SerializeField] protected Image blackScreen;
    [SerializeField] protected float screenFadeSpeed;

    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected AudioClip dialogue;

    [SerializeField] protected Animator animator;

    protected float alpha = 0;

    protected bool runningCutScene = false;

    public static Action<bool> CutSceneRunning;

    protected virtual void Start() {}

    protected virtual void Update() {}
}
