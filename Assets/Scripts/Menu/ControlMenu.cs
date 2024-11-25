using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMenu : MonoBehaviour {
    public void BackToSettings() {
        SoundManager.Instance.PlaySound(SoundManager.Instance.backButtonSound);
        Invoke(nameof(LoadSettings), .5f);
    }
    public void LoadSettings() {
        SceneManager.LoadScene("Settings");
    }
}
