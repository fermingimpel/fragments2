using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour {
 
    [SerializeField] float horizontalSensitivity;
    [SerializeField] float verticalSensitivity;
    [SerializeField] float adsSensitivityReduce;
    float actualHorizontalSensivitiy;
    float actualVerticalSensitivity;

    [SerializeField] Transform playerBody;

    float xRotation = 0;

    float horizontalRecoil = 0;
    float verticalRecoil = 0;

    bool canMove = true;

    void Start() {
        actualHorizontalSensivitiy = horizontalSensitivity;
        actualVerticalSensitivity = verticalSensitivity;
        Cursor.lockState = CursorLockMode.Locked;
        UpdateSensitivity();
    }

    void Update() {
        if (PauseController.instance.IsPaused)
            return;

        if (!canMove)
            return;

        float mouseX = Input.GetAxis("Mouse X") * actualHorizontalSensivitiy * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * actualVerticalSensitivity * Time.deltaTime;


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
    
    public void ReduceSensitivity() {
        actualHorizontalSensivitiy = horizontalSensitivity - (horizontalSensitivity / 100f * adsSensitivityReduce);
        actualVerticalSensitivity = verticalSensitivity - (verticalSensitivity / 100f * adsSensitivityReduce);
    }

    public void ResetSensitivity() {
        actualHorizontalSensivitiy = horizontalSensitivity;
        actualVerticalSensitivity = verticalSensitivity;
    }

}
