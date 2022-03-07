using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZKill : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerController>().Hit(9999);
        else if (other.CompareTag("Enemy"))
            other.GetComponent<Enemy>().Hit(99999, Vector3.zero, Vector3.zero);
    }
}
