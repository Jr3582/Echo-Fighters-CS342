using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public AudioSource backgroundMusic;
    public AudioClip menuMusic;
    public AudioClip gameplayMusic;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        backgroundMusic.volume = 0.10f;
        PlayMenuMusic();
    }

    public void PlayMenuMusic() {
        if (backgroundMusic.clip != menuMusic) {
            backgroundMusic.clip = menuMusic;
            backgroundMusic.loop = true;
            backgroundMusic.Play();
        }
    }
    
    public void PlayGameplayMusic() {
        if (backgroundMusic.clip != gameplayMusic) {
            backgroundMusic.clip = gameplayMusic;
            backgroundMusic.loop = true;
            backgroundMusic.Play();
        }
    }

    public void StopMusic() {
        if (backgroundMusic != null) {
            backgroundMusic.Stop();
        }
    }
}
