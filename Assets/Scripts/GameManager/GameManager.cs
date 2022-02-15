using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private void Awake() {
        PlayerController.PlayerDead += EndGame; 
    }

    void OnDisable() {
        PlayerController.PlayerDead += EndGame;
    }

    void EndGame() {
        SceneController.instance.LoadScene("Loose", 5);
    }



}
