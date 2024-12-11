using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour {
    public int startTime = 180;
    private float currentTime;
    public TextMeshProUGUI countdownText;
    private Player1 player1;
    private Player2 player2;
    private RoundScript roundScript;
    void Start() {
        currentTime = startTime;

        player1 = FindObjectOfType<Player1>();
        player2 = FindObjectOfType<Player2>();
        roundScript = FindObjectOfType<RoundScript>();
    }

    void Update() {
        currentTime -= Time.deltaTime;

        currentTime = Mathf.Max(0, currentTime);

        countdownText.text = string.Format("{0:00}", currentTime);

        if (currentTime <= 0) {
            if(((float)player1.currentHealth / player1.maxHealth) > ((float)player2.currentHealth / player2.maxHealth)) {
                player2.TakeDamage(1000);
                ResetRoundStuff();
            } else if (((float)player1.currentHealth / player1.maxHealth) < ((float)player2.currentHealth / player2.maxHealth)){
                player1.TakeDamage(1000);
                ResetRoundStuff();
            } else {
                ResetRoundStuff();
            }
        }
    }
    private void ResetRoundStuff() {
        Time.timeScale = 0;
        player1.ResetForNewRound(player1.startingPosition);
        player2.ResetForNewRound(player2.startingPosition);
        roundScript.StartNewRound();
    }
    public void ResetTimer() {
        currentTime = 180;
    }
}
