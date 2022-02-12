using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] CharacterController characterController;
    [SerializeField] float speed;

    [SerializeField] float gravity = -9.81f;

    [SerializeField] float jumpHeight;

    [SerializeField] Transform groundCheck;

    [SerializeField] float groundDistance = 0.4f;

    [SerializeField] LayerMask groundMask;

    Vector3 velocity;
    Vector3 movement;

    enum PlayerState { InGround, InAir }
    [SerializeField] PlayerState playerState;

    void Start() {
        playerState = PlayerState.InGround;
    }
    void Update() {
        // if (GameStateManager.instance.GetState() == GameStateManager.GameState.Paused)
        //     return;



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

        characterController.Move(movement * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public void SlowPlayer() {
        StartCoroutine(Slow(50, 1));
    }

    public IEnumerator Slow(float percent, float time) {
        float aux = speed;

        speed -= (speed / 100f) * percent;
        yield return new WaitForSeconds(time);

        while (playerState == PlayerState.InAir)
            yield return null;

        speed = aux;
    }
}
