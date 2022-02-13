using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public static SceneController instance;

    void Awake() {
        if(instance != null) {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

}
