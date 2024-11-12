using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public SpriteRenderer player1SpriteRenderer;
    public SpriteRenderer player2SpriteRenderer;

    public List<GameObject> player1Name = new List<GameObject>();
    public List<Sprite> player1Characters = new List<Sprite>();
    public List<GameObject> player1Prefabs;
    public List<GameObject> player1Descriptions = new List<GameObject>();

    public List<GameObject> player2Name = new List<GameObject>();
    public List<Sprite> player2Characters = new List<Sprite>();
    public List<GameObject> player2Prefabs;
    public List<GameObject> player2Descriptions = new List<GameObject>();

    private int player1SelectedCharacter = 0;
    private int player2SelectedCharacter = 0;

    private GameObject player1SelectedPrefab;
    private GameObject player2SelectedPrefab;

    private void Start()
    {
        UpdatePlayer1Selection();
        UpdatePlayer2Selection();
    }

    public void NextOptionPlayer1()
    {
        player1SelectedCharacter = (player1SelectedCharacter + 1) % player1Characters.Count;
        UpdatePlayer1Selection();
    }

    public void BackOptionPlayer1()
    {
        player1SelectedCharacter = (player1SelectedCharacter - 1 + player1Characters.Count) % player1Characters.Count;
        UpdatePlayer1Selection();
    }

    public void NextOptionPlayer2()
    {
        player2SelectedCharacter = (player2SelectedCharacter + 1) % player2Characters.Count;
        UpdatePlayer2Selection();
    }

    public void BackOptionPlayer2()
    {
        player2SelectedCharacter = (player2SelectedCharacter - 1 + player2Characters.Count) % player2Characters.Count;
        UpdatePlayer2Selection();
    }

    private void UpdatePlayer1Selection()
    {
        player1SpriteRenderer.sprite = player1Characters[player1SelectedCharacter];
        player1SelectedPrefab = player1Prefabs[player1SelectedCharacter];
        
        foreach (GameObject name in player1Name) name.SetActive(false);
        foreach (GameObject description in player1Descriptions) description.SetActive(false);
        
        if (player1Name.Count > player1SelectedCharacter)
            player1Name[player1SelectedCharacter].SetActive(true);
        
        if (player1Descriptions.Count > player1SelectedCharacter)
            player1Descriptions[player1SelectedCharacter].SetActive(true);
    }

    private void UpdatePlayer2Selection()
    {
        player2SpriteRenderer.sprite = player2Characters[player2SelectedCharacter];
        player2SelectedPrefab = player2Prefabs[player2SelectedCharacter];
        
        foreach (GameObject name in player2Name) name.SetActive(false);
        foreach (GameObject description in player2Descriptions) description.SetActive(false);
        
        if (player2Name.Count > player2SelectedCharacter)
            player2Name[player2SelectedCharacter].SetActive(true);
        
        if (player2Descriptions.Count > player2SelectedCharacter)
            player2Descriptions[player2SelectedCharacter].SetActive(true);
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
            GameObject player1Instance = null;
            GameObject player2Instance = null;

            if (player1SelectedPrefab != null)
            {
                Vector3 player1SpawnPosition = new Vector3(-6.5f, -2f, 0f);
                player1Instance = Instantiate(player1SelectedPrefab, player1SpawnPosition, Quaternion.identity);

                GameObject player1HealthBarUI = GameObject.Find("P1Health");
                GameObject player1CooldownUI = GameObject.Find("AttackCoolDownUIP1");
                GameObject player1Life1 = GameObject.Find("P1Life1");
                GameObject player1Life2 = GameObject.Find("P1Life2");

                if (player1HealthBarUI != null && player1CooldownUI != null)
                {
                    Player1 player1Script = player1Instance.GetComponent<Player1>();
                    AttackCoolDownUI player1ScriptCooldownUI = player1CooldownUI.GetComponent<AttackCoolDownUI>();

                    if (player1Script != null && player1ScriptCooldownUI != null)
                    {
                        player1Script.player1HealthBar = player1HealthBarUI.GetComponent<Image>();
                        player1Script.player1AttackCoolDownUI = player1ScriptCooldownUI;
                        player1ScriptCooldownUI.heavyAttackCooldown = player1Script.HeavyAttackCooldown;
                        player1Script.life1 = player1Life1;
                        player1Script.life2 = player1Life2;

                    }
                }
            }

            if (player2SelectedPrefab != null)
            {
                Vector3 player2SpawnPosition = new Vector3(6.5f, -2f, 0f);
                player2Instance = Instantiate(player2SelectedPrefab, player2SpawnPosition, Quaternion.identity);

                GameObject player2HealthBarUI = GameObject.Find("P2Health");
                GameObject player2CooldownUI = GameObject.Find("AttackCoolDownUIP2");
                GameObject player2Life1 = GameObject.Find("P2Life1");
                GameObject player2Life2 = GameObject.Find("P2Life2");

                if (player2HealthBarUI != null && player2CooldownUI != null)
                {
                    Player2 player2Script = player2Instance.GetComponent<Player2>();
                    AttackCoolDownUI player2ScriptCooldownUI = player2CooldownUI.GetComponent<AttackCoolDownUI>();

                    if (player2Script != null && player2ScriptCooldownUI != null)
                    {
                        player2Script.player2HealthBar = player2HealthBarUI.GetComponent<Image>();
                        player2Script.player2AttackCoolDownUI = player2ScriptCooldownUI;
                        player2ScriptCooldownUI.heavyAttackCooldown = player2Script.HeavyAttackCooldown;
                        player2Script.life1 = player2Life1;
                        player2Script.life2 = player2Life2;
                    }
                }
            }

            DynamicCameraController cameraController = FindObjectOfType<DynamicCameraController>();
            if (cameraController != null)
            {
                if (player1Instance != null) cameraController.player1 = player1Instance.transform;
                if (player2Instance != null) cameraController.player2 = player2Instance.transform;
            }
        }

        SceneManager.sceneLoaded -= OnGameplaySceneLoaded;
    }
}
