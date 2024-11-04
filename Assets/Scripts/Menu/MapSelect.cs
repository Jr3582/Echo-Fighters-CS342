using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelect : MonoBehaviour
{
    public void SelectMap() {
        SceneManager.LoadScene("GameplayScene");
    }

    public void GoBackToCharacterSelect() {
        SceneManager.LoadScene("CharacterSelect");
    }
}
