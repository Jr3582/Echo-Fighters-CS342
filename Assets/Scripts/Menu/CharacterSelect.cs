using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public void SelectCharacter() {
        SceneManager.LoadScene("MapSelect");
    }

    public void GoBackToMainMenu() {
        if (CharacterManager.Instance != null) {
            Destroy(CharacterManager.Instance.gameObject);
            CharacterManager.Instance = null;
        }
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
