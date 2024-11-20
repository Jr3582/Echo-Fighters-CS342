using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelect : MonoBehaviour {

    public void GoBackToCharacterSelect() {
        if (CharacterManager.Instance != null) {
            Destroy(CharacterManager.Instance.gameObject);
            CharacterManager.Instance = null;
        }

        SoundManager.Instance.PlaySound(SoundManager.Instance.backButtonSound);
        Invoke(nameof(LoadMainMenu), .5f);
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("CharacterSelect");
    }
}
