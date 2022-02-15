using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    void Start() {
        PlayerController.PlayerDead += EndGame;
    }

    void OnDisable() {
        PlayerController.PlayerDead += EndGame;
    }

    void EndGame() {
        SceneController.instance.LoadScene("Loose", 5);
    }



}
