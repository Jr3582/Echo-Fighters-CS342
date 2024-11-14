using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoScene : MonoBehaviour
{
    public void BackToSelectCharacter() {
        SceneManager.LoadScene("CharacterSelect");
    }
}
