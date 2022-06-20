using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField] AudioClip ambientMusic;
    [SerializeField] AudioClip fightMusic;
    [SerializeField] AudioSource audioSource;

    void Start() {
        PlayerController.PlayerDead += StopMusic;

        audioSource.clip = ambientMusic;
        audioSource.Play();
    }
    private void OnDisable() {
        PlayerController.PlayerDead -= StopMusic;
    }
    public void SetMusic(AudioClip music) {
        if(audioSource.clip != music) {
            audioSource.clip = music;
            audioSource.Play();
            audioSource.loop = true;
        }
    }

    public void StartFightMusic() {
        SetMusic(fightMusic);
    }

    public void StartAmbientMusic() {
        SetMusic(ambientMusic);
    }

    public void StopMusic() {
        audioSource.Stop();
    }
}
