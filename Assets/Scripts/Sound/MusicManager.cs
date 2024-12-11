using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public AudioSource backgroundMusic;
    public AudioClip menuMusic;
    public AudioClip gameplayMusic;
    public AudioClip warpedCityMusic;
    public AudioClip frenzyForestMusic;
    public AudioClip cyberAlleyMusic;
    public AudioClip rumbleRailsMusic;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        backgroundMusic.volume = 0.10f;
        PlayMenuMusic();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "GameplayWarpedCity") {
            PlayWarpedCityMusic();
        } else if (scene.name == "GameplayRumbleRails") {
            PlayRumbleRailsMusic();
        } else if (scene.name == "GameplayFrenzyForest") {
            PlayFrenzyForestMusic();
        } else if(scene.name == "GameplayCyberAlley") {
            PlayCyberAlleyMusic();
        } else {
            PlayMenuMusic();
        }
    }

    public void PlayMenuMusic() {
        if (backgroundMusic.clip != menuMusic) {
            backgroundMusic.clip = menuMusic;
            backgroundMusic.loop = true;
            backgroundMusic.Play();
        }
    }

    public void PlayWarpedCityMusic() {
        if (backgroundMusic.clip != warpedCityMusic) {
            backgroundMusic.clip = warpedCityMusic;
            backgroundMusic.loop = true;
            backgroundMusic.Play();
        }
    }
    public void PlayRumbleRailsMusic() {
        if (backgroundMusic.clip != rumbleRailsMusic) {
            backgroundMusic.clip = rumbleRailsMusic;
            backgroundMusic.loop = true;
            backgroundMusic.Play();
        }
    }
    public void PlayFrenzyForestMusic() {
        if (backgroundMusic.clip != frenzyForestMusic) {
            backgroundMusic.clip = frenzyForestMusic;
            backgroundMusic.loop = true;
            backgroundMusic.Play();
        }
    }
    public void PlayCyberAlleyMusic() {
        if (backgroundMusic.clip != cyberAlleyMusic) {
            backgroundMusic.clip = cyberAlleyMusic;
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
