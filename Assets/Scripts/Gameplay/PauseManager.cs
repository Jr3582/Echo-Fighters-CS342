using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Slider musicSlider;
    public Slider effectSlider;

    private bool isPaused = false;

    void Start()
    {
        if (pauseMenuUI != null) {
            pauseMenuUI.SetActive(false);
        }

        if (MusicManager.Instance != null && MusicManager.Instance.backgroundMusic != null) {
            musicSlider.value = MusicManager.Instance.backgroundMusic.volume * 2;
        }

        if (SoundManager.Instance != null && SoundManager.Instance.effectSource != null) {
            effectSlider.value = SoundManager.Instance.effectSource.volume;
        }

        musicSlider.onValueChanged.AddListener(UpdateMusicVolume);
        effectSlider.onValueChanged.AddListener(UpdateEffectVolume);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            TogglePause();
        }
    }

    public void TogglePause() {
        isPaused = !isPaused;

        if (isPaused) {
            PauseGame();
        } else {
            ResumeGame();
        }
    }

    private void PauseGame() {
        Time.timeScale = 0f;
        if (pauseMenuUI != null) {
            pauseMenuUI.SetActive(true);
        }
    }

    private void ResumeGame() {
        Time.timeScale = 1f;
        if (pauseMenuUI != null) {
            pauseMenuUI.SetActive(false);
        }
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

    public void QuitToMainMenu() {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        MusicManager.Instance.PlayMenuMusic();
    }
}
