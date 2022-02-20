using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField] AudioClip ambientMusic;
    [SerializeField] AudioClip fightMusic;
    [SerializeField] AudioSource audioSource;

    void Start() {
        audioSource.clip = ambientMusic;
        audioSource.Play();
    }

    public void SetMusic(AudioClip music) {
        if(audioSource.clip != music) {
            audioSource.clip = music;
            audioSource.Play();
            audioSource.loop = true;
        }
    }
}
