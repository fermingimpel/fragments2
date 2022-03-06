using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMansion : MonoBehaviour {

    BoxCollider bc;

    void Start() {
        bc = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            bc.enabled = false;
            other.GetComponentInChildren<EscapeCutScene>().enabled = true;
            other.GetComponentInChildren<EscapeCutScene>().EscapeStartCutScene();
        }
    }

}
