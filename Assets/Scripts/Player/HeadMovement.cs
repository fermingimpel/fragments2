using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour {

    [SerializeField] Transform head;

    [SerializeField] float headMovementSpeed;
    [SerializeField] float headMovementAltitude;
    
   float t = 0;
   float multiplier = 1;
   float originalY = 0;
   float posY = 0;
   
    [SerializeField] PlayerMovement playerMovement;

    private void Start() {
    }

    void Update() {
        if (PauseController.instance.IsPaused)
            return;

        if (!playerMovement.GetIsMoving())
            return;

        head.localPosition = new Vector3(head.localPosition.x, CalculateMovemenet(), head.localPosition.z);
        t += Time.deltaTime * headMovementSpeed;
    }

    float CalculateMovemenet() {
        float finalY = Mathf.Lerp(posY, originalY + headMovementAltitude * multiplier, t);

        if (finalY == originalY + headMovementAltitude * multiplier) {
            posY = finalY;
            t = 0f;
            multiplier *= -1f;
        }

        return finalY;
    }

}
