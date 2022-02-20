using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour {

    [SerializeField] GameObject exitPanel;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject options;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject controls;
    [SerializeField] Image fade;
    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (exitPanel.activeSelf || options.activeSelf || credits.activeSelf || controls.activeSelf)
                OnClickBack();
            else
                OnClickExit();
        }
    }

    public void OnClickPlay() {
        anim.SetTrigger("out");
    }

    public void StartGame() {
        SceneController.instance.LoadScene("MainGame");
    }

    public void OnClickExit() {
        exitPanel.SetActive(true);
    }

    public void OnClickBack() {
        menu.SetActive(true);
        exitPanel.SetActive(false);
        options.SetActive(false);
        credits.SetActive(false);
        controls.SetActive(false);
    }

    public void OnClick(GameObject panel) {
        menu.SetActive(false);
        panel.SetActive(true);
    }
   
    public void TurnFade() {
        fade.gameObject.SetActive(!fade.gameObject.activeSelf);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
