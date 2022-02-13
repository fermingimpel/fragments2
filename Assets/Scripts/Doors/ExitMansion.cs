using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMansion : MonoBehaviour {

    BoxCollider bc;

    void Start() {
        bc = GetComponent<BoxCollider>();
        bc.enabled = false;
    }

    public void EnableExit() {
        bc.enabled = true;
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) 
            SceneController.instance.LoadScene("End");
    }

}
