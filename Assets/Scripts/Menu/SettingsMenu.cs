using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider effectSlider;

    private void Start() {
        if (MusicManager.Instance != null && MusicManager.Instance.backgroundMusic != null) {
            musicSlider.value = MusicManager.Instance.backgroundMusic.volume * 2;
            Debug.LogError("MusicManager is missing!");
        }

        if (SoundManager.Instance != null && SoundManager.Instance.effectSource != null) {
            effectSlider.value = SoundManager.Instance.effectSource.volume;
            Debug.LogError("SoundManager is missing!");
        }

        musicSlider.onValueChanged.AddListener(UpdateMusicVolume);
        effectSlider.onValueChanged.AddListener(UpdateEffectVolume);
    }

    private void UpdateMusicVolume(float value) {
        if (MusicManager.Instance != null && MusicManager.Instance.backgroundMusic != null) {
            MusicManager.Instance.backgroundMusic.volume = value * 0.5f;
        }
    }

    private void UpdateEffectVolume(float value) {
        if (SoundManager.Instance != null && SoundManager.Instance.effectSource != null) {
            SoundManager.Instance.effectSource.volume = value;
        }
    }

    public void GoBackToMainMenu() {
        SoundManager.Instance.PlaySound(SoundManager.Instance.backButtonSound);
        Invoke(nameof(LoadMainMenu), .5f);
    }

    public void GoToControlsMenu() {
        SoundManager.Instance.PlaySound(SoundManager.Instance.backButtonSound);
        Invoke(nameof(LoadControlsMenu), .5f);
    }
    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadControlsMenu() {
        SceneManager.LoadScene("Controls");
    }
}
