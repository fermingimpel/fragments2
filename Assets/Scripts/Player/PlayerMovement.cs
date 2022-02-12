using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] CharacterController characterController;
    [SerializeField] float speedWalking;
    [SerializeField] float speedRunning;

    [SerializeField] float gravity = -9.81f;

    [SerializeField] float jumpHeight;

    [SerializeField] Transform groundCheck;

    [SerializeField] float groundDistance = 0.4f;

    [SerializeField] LayerMask groundMask;

    Vector3 velocity;
    Vector3 movement;

    public enum PlayerState { InGround, InAir }
    [SerializeField] PlayerState playerState;

    public enum PlayerMovementState { Walking, Running}
    [SerializeField] PlayerMovementState playerMovementState;

    void Start() {
        playerState = PlayerState.InGround;
    }
    void Update() {
        // if (GameStateManager.instance.GetState() == GameStateManager.GameState.Paused)
        //     return;

        Inputs();
        Movement();
    }

    void Inputs() {
        if (playerState == PlayerState.InAir)
            return;

        if (Input.GetKey(KeyCode.LeftShift))
            playerMovementState = PlayerMovementState.Running;
        else
            playerMovementState = PlayerMovementState.Walking;
    }

    void Movement() {
        if (Physics.CheckSphere(groundCheck.position, groundDistance, groundMask))
            playerState = PlayerState.InGround;
        else
            playerState = PlayerState.InAir;

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        movement = transform.right * x + transform.forward * z;

        switch (playerState) {
            case PlayerState.InGround:
                if (velocity.y < 0)
                    velocity.y = -2f;

                if (Input.GetButtonDown("Jump"))
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

                break;
            case PlayerState.InAir:
                //
                break;
        }

        if (playerMovementState == PlayerMovementState.Walking)
            characterController.Move(movement * speedWalking * Time.deltaTime);
        else if (playerMovementState == PlayerMovementState.Running)
            characterController.Move(movement * speedRunning * Time.deltaTime);

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
}
