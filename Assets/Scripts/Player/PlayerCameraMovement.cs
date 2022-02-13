using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour {
 
    [SerializeField] float mouseSensitivity;

    [SerializeField] Transform playerBody;

    float xRotation = 0;

    float horizontalRecoil = 0;
    float verticalRecoil = 0;

    bool canMove = true;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        // if (GameStateManager.instance.GetState() == GameStateManager.GameState.Paused)
        //     return;

        if (!canMove)
            return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


        mouseX += horizontalRecoil;
        mouseY += verticalRecoil;

        horizontalRecoil = 0;
        verticalRecoil = 0;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void SetCanMove(bool value) {
        canMove = value;
    }

    public void AddRecoil(float vertical, float horizontal) {
        verticalRecoil += vertical;
        horizontalRecoil += horizontal;
    }
}
