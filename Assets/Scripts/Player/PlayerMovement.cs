﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] CharacterController characterController;
    [SerializeField] float speedWalking;
    [SerializeField] float speedRunning;
    [SerializeField] float adsSlowPercentage;

    float actualSpeedWalking;
    float actualSpeedRunning;


    [SerializeField] float gravity = -9.81f;

    [SerializeField] float jumpHeight;

    [SerializeField] Transform groundCheck;

    [SerializeField] float groundDistance = 0.4f;

    [SerializeField] LayerMask groundMask;

    Vector3 velocity;
    Vector3 movement;

    public enum PlayerState { InGround, InAir, InStairs }
    [SerializeField] PlayerState playerState;

    public enum PlayerMovementState { Walking, Running }
    [SerializeField] PlayerMovementState playerMovementState;

    [SerializeField] AudioClip walkingSound;
    [SerializeField] AudioClip runningSound;
    [SerializeField] AudioClip jumpingSound;
    [SerializeField] AudioClip landingSound;
    [SerializeField] AudioSource sfxAudioSource;
    [SerializeField] AudioSource loopedSoundsAudioSource;

    bool canMove = true;
    bool canRun = true;

    void Awake() {
        PauseController.Pause += Pause;
    }

    void Start() {
        playerState = PlayerState.InGround;
        actualSpeedRunning = speedRunning;
        actualSpeedWalking = speedWalking;
    }

    void OnDestroy() {
        PauseController.Pause -= Pause;
    }

    void OnDisable() {
        PauseController.Pause -= Pause;
    }

    void Update() {
        if (PauseController.instance.IsPaused)
            return;

        if (!canMove)
            return;

        Inputs();
        Movement();
    }

    void Inputs() {
        if (playerState == PlayerState.InAir)
            return;

        if (Input.GetKey(KeyCode.LeftShift) && canRun) {
            loopedSoundsAudioSource.clip = runningSound;
            playerMovementState = PlayerMovementState.Running;
        }
        else {
            loopedSoundsAudioSource.clip = walkingSound;
            playerMovementState = PlayerMovementState.Walking;
        }
    }

    void Movement() {
        if (Physics.CheckSphere(groundCheck.position, groundDistance, groundMask))
            playerState = PlayerState.InGround;
        else
            playerState = PlayerState.InAir;

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        movement = transform.right * x + transform.forward * z;

        if (!loopedSoundsAudioSource.isPlaying && (Mathf.Abs(movement.x) > 0.5f || Mathf.Abs(movement.z) > 0.5f) && playerState != PlayerState.InAir)
            loopedSoundsAudioSource.Play();
        else if (((Mathf.Abs(movement.x) < 0.5f && Mathf.Abs(movement.z) < 0.5f) && loopedSoundsAudioSource.isPlaying) || playerState == PlayerState.InAir)
            loopedSoundsAudioSource.Stop();

        switch (playerState) {
            case PlayerState.InGround:
                if (velocity.y < 0)
                    velocity.y = -2f;

                if (Input.GetButtonDown("Jump")) {
                    sfxAudioSource.Play();
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
                break;
            case PlayerState.InAir:
                //
                break;
        }



        if (playerMovementState == PlayerMovementState.Walking)
            characterController.Move(movement * actualSpeedWalking * Time.deltaTime);
        else if (playerMovementState == PlayerMovementState.Running)
            characterController.Move(movement * actualSpeedRunning * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    // public void SlowPlayer() {
    //     StartCoroutine(Slow(50, 1));
    // }

    //public IEnumerator Slow(float percent, float time) {
    //    float aux = speed;
    //
    //    speed -= (speed / 100f) * percent;
    //    yield return new WaitForSeconds(time);
    //
    //    while (playerState == PlayerState.InAir)
    //        yield return null;
    //
    //    speed = aux;
    //}

    public void ReduceSpeed() {
        actualSpeedRunning = speedRunning - (speedRunning / 100f * adsSlowPercentage);
        actualSpeedWalking = speedWalking - (speedWalking / 100f * adsSlowPercentage);
        canRun = false;
    }

    public void ResetSpeed() {
        canRun = true;
        actualSpeedRunning = speedRunning;
        actualSpeedWalking = speedWalking;
    }

    public void SetCanMove(bool value) {
        canMove = value;
    }

    public void Pause() {
        if (PauseController.instance.IsPaused) {
            loopedSoundsAudioSource.Stop();
            return;
        }

        loopedSoundsAudioSource.Play();
    }
}
