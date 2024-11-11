using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public SpriteRenderer player1SpriteRenderer;
    public SpriteRenderer player2SpriteRenderer;

    public List<Sprite> player1Characters = new List<Sprite>();
    public List<GameObject> player1Prefabs;

    public List<Sprite> player2Characters = new List<Sprite>();
    public List<GameObject> player2Prefabs;

    private int player1SelectedCharacter = 0;
    private int player2SelectedCharacter = 0;

    private GameObject player1SelectedPrefab;
    private GameObject player2SelectedPrefab;

    private void Start()
    {
        if (player1Characters.Count > 0 && player1Prefabs.Count > 0)
        {
            player1SpriteRenderer.sprite = player1Characters[player1SelectedCharacter];
            player1SelectedPrefab = player1Prefabs[player1SelectedCharacter];
        }

        if (player2Characters.Count > 0 && player2Prefabs.Count > 0)
        {
            player2SpriteRenderer.sprite = player2Characters[player2SelectedCharacter];
            player2SelectedPrefab = player2Prefabs[player2SelectedCharacter];
        }
    }

    // For Player 1
    public void NextOptionPlayer1()
    {
        player1SelectedCharacter = (player1SelectedCharacter + 1) % player1Characters.Count;
        player1SpriteRenderer.sprite = player1Characters[player1SelectedCharacter];
        player1SelectedPrefab = player1Prefabs[player1SelectedCharacter];
    }

    public void BackOptionPlayer1()
    {
        player1SelectedCharacter = (player1SelectedCharacter - 1 + player1Characters.Count) % player1Characters.Count;
        player1SpriteRenderer.sprite = player1Characters[player1SelectedCharacter];
        player1SelectedPrefab = player1Prefabs[player1SelectedCharacter];
    }

    // For Player 2
    public void NextOptionPlayer2()
    {
        player2SelectedCharacter = (player2SelectedCharacter + 1) % player2Characters.Count;
        player2SpriteRenderer.sprite = player2Characters[player2SelectedCharacter];
        player2SelectedPrefab = player2Prefabs[player2SelectedCharacter];
    }

    public void BackOptionPlayer2()
    {
        player2SelectedCharacter = (player2SelectedCharacter - 1 + player2Characters.Count) % player2Characters.Count;
        player2SpriteRenderer.sprite = player2Characters[player2SelectedCharacter];
        player2SelectedPrefab = player2Prefabs[player2SelectedCharacter];
    }

    public void PlayGame()
    {
        SceneManager.sceneLoaded += OnGameplaySceneLoaded;
        SceneManager.LoadScene("GameplayScene");
    }

    private void OnGameplaySceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameplayScene")
        {
            // Spawn Player 1
            if (player1SelectedPrefab != null)
            {
                Vector3 player1SpawnPosition = new Vector3(-6.5f, -2f, 0f);
                Instantiate(player1SelectedPrefab, player1SpawnPosition, Quaternion.identity);
            }

            // Spawn Player 2
            if (player2SelectedPrefab != null)
            {
                Vector3 player2SpawnPosition = new Vector3(6.5f, -2f, 0f); // Adjust as needed
                Instantiate(player2SelectedPrefab, player2SpawnPosition, Quaternion.identity);
            }
        }

        SceneManager.sceneLoaded -= OnGameplaySceneLoaded;
    }
}
