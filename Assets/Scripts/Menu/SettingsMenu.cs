using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public void GoBackToMainMenu() {
        SoundManager.Instance.PlaySound(SoundManager.Instance.backButtonSound);
        Invoke(nameof(LoadMainMenu), .5f);
    }
    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
