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

        instance = this;
    }

    public void LoadScene(string sceneName) {
        StartCoroutine(ChangeScene(sceneName, 0f));
    }

    public void LoadScene(string sceneName, float delay) {
        StartCoroutine(ChangeScene(sceneName, delay));
    }

    IEnumerator ChangeScene(string sceneName, float delay) {
        yield return new WaitForSeconds(delay);
        if (sceneName == "MainGame") {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        SceneManager.LoadScene(sceneName);
    }

}
