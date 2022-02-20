using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameInstance : MonoBehaviour {

    float horSensitivity = 100;
    float verSensitivity = 100;
    float sfxVol = 10;
    float musicVol = 10;

    [SerializeField] AudioMixer sfxMixer;
    [SerializeField] AudioMixer musicMixer;

    [HideInInspector]
    public float HorizontalSensitivity { get { return horSensitivity; } set { horSensitivity = value; }  }

    [HideInInspector]
    public float VerticalSensitivity { get { return verSensitivity; } set { verSensitivity = value; } }

    [HideInInspector]
    public float SfxVolume { get { return sfxVol; } set {
            sfxVol = value;
            sfxMixer.SetFloat("Volume", -20 + (sfxVol * 2));
            if (sfxVol < 0.1f)
                sfxMixer.SetFloat("Volume", -80);
        }
    }
    [HideInInspector]
    public float MusicVolume { get { return musicVol; } set {
            musicVol = value;
            musicMixer.SetFloat("Volume", -13 + (musicVol * 2));
            if (musicVol < 0.1f)
                musicMixer.SetFloat("Volume", -80);
        }
    }


    #region Singleton
    public static GameInstance instance;

    void Awake() {
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (instance != null) {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }
    #endregion

}
