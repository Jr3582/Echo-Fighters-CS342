using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MapSelector : MonoBehaviour
{
    public Image mapPreviewImage;
    public TextMeshProUGUI mapNameText;
    public List<MapData> maps;
    private int selectedMapIndex = 0;

    private void Start() {
        if (maps.Count > 0)
        {
            DisplayMapInfo(selectedMapIndex);
        }
    }

    public void NextMap() {
        SoundManager.Instance.PlaySound(SoundManager.Instance.arrowButtonSound);
        selectedMapIndex = (selectedMapIndex + 1) % maps.Count;
        DisplayMapInfo(selectedMapIndex);
    }

    public void PreviousMap() {
        SoundManager.Instance.PlaySound(SoundManager.Instance.arrowButtonSound);
        selectedMapIndex = (selectedMapIndex - 1 + maps.Count) % maps.Count;
        DisplayMapInfo(selectedMapIndex);
    }

    private void DisplayMapInfo(int index) {
        MapData selectedMap = maps[index];
        mapPreviewImage.sprite = selectedMap.mapPreview;
        mapNameText.text = selectedMap.mapName;
    }

    public void StartGame() {
        SoundManager.Instance.PlaySound(SoundManager.Instance.continueButtonSound);
        MusicManager.Instance.PlayGameplayMusic();
        Invoke(nameof(LoadGameplay), .5f);
    }

    public void LoadGameplay() {
        string sceneToLoad = maps[selectedMapIndex].sceneName;
        SelectMapAndStartGame(sceneToLoad);
    }

    public void SelectMapAndStartGame(string mapSceneName) {
        CharacterManager.Instance.LoadGameplayScene(mapSceneName);
    }
}
