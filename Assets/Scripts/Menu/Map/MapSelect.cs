using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelect : MonoBehaviour {

    public void GoBackToCharacterSelect() {
        Debug.Log("Returning to CharacterSelect scene...");

        if (CharacterManager.Instance != null) {
            Debug.Log("Destroying DontDestroyOnLoad CharacterManager.");
            Destroy(CharacterManager.Instance.gameObject);
            CharacterManager.Instance = null;
        }

        SceneManager.LoadScene("CharacterSelect");
    }
}
