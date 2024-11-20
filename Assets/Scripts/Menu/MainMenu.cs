using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        SoundManager.Instance.PlaySound(SoundManager.Instance.menuButtonSound);
        Invoke(nameof(LoadCharacterSelect), 2f);
    }

    public void LoadCharacterSelect () {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void OpenSettings() {
        SoundManager.Instance.PlaySound(SoundManager.Instance.menuButtonSound);
        Invoke(nameof(LoadSettings), .5f);
    }

    public void LoadSettings() {
        SceneManager.LoadScene("Settings");
    }

    public void QuitGame() {
        if (UnityEditor.EditorApplication.isPlaying) {
            UnityEditor.EditorApplication.isPlaying = false;
        } else {
            Application.Quit();
        }
    }
}
