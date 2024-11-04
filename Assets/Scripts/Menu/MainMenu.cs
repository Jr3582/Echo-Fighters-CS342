using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void OpenSettings() {
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
