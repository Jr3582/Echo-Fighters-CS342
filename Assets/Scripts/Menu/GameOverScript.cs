using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public void BackToCharacterSelect() {
        if (CharacterManager.Instance != null) {
            Destroy(CharacterManager.Instance.gameObject);
            CharacterManager.Instance = null;
        }
        SceneManager.LoadScene("CharacterSelect");
    }
    public void BackToMapSelect() {
        SceneManager.LoadScene("MapSelect");
    }
}
