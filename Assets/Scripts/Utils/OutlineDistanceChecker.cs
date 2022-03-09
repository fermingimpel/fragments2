using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class OutlineDistanceChecker : MonoBehaviour {

    [SerializeField] Outline outline;
    [SerializeField] Transform player;
    [SerializeField] float distanceToDrawOutline = 6;

    void Start() {
        outline = GetComponent<Outline>();
        player = FindObjectOfType<PlayerController>().transform;
    }

    void FixedUpdate() {
        if(Vector3.Distance(transform.position, player.position) < distanceToDrawOutline) 
            outline.eraseRenderer = false;
        else
            outline.eraseRenderer = true;
    }
}
