using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelect : MonoBehaviour {

    public void GoBackToCharacterSelect() {
        CharacterManager.Instance.InitializeUIReferences();
        CharacterManager.Instance.ResetCharacterSelection();
        SceneManager.LoadScene("CharacterSelect");
    }
}
