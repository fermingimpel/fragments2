using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour {
 
    [SerializeField] float horizontalSensitivity;
    [SerializeField] float verticalSensitivity;
    [SerializeField] Transform playerBody;

    float xRotation = 0;

    float horizontalRecoil = 0;
    float verticalRecoil = 0;

    bool canMove = true;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        UpdateSensitivity();
    }

    void Update() {
        if (PauseController.instance.IsPaused)
            return;

        if (!canMove)
            return;

        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity * Time.deltaTime;


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

    public void UpdateSensitivity() {
        horizontalSensitivity = GameInstance.instance.HorizontalSensitivity;
        verticalSensitivity = GameInstance.instance.VerticalSensitivity;
    }
}
