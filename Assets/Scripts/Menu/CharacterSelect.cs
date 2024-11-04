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
        SceneManager.LoadScene("MainMenu");
    }
}
