using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour {
 
    [SerializeField] float mouseSensitivity;

    [SerializeField] Transform playerBody;

    float xRotation = 0;
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
       // if (GameStateManager.instance.GetState() == GameStateManager.GameState.Paused)
       //     return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
