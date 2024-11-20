using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public void SelectCharacter() {
        SoundManager.Instance.PlaySound(SoundManager.Instance.continueButtonSound);
        Invoke(nameof(LoadMapSelect), .5f);
    }

    public void LoadMapSelect() {
        SceneManager.LoadScene("MapSelect");
    }

    public void GoBackToMainMenu() {
        if (CharacterManager.Instance != null) {
            Destroy(CharacterManager.Instance.gameObject);
            CharacterManager.Instance = null;
        }
        SoundManager.Instance.PlaySound(SoundManager.Instance.backButtonSound);
        Invoke(nameof(LoadMainMenu), .5f);
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToCharacterInfo() {
        if (CharacterManager.Instance != null) {
            Destroy(CharacterManager.Instance.gameObject);
            CharacterManager.Instance = null;
        }
        SceneManager.LoadScene("InfoScene");
    }
}
