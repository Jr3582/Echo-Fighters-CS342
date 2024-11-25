using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RoundScript : MonoBehaviour {
    public static RoundScript Instance { get; private set; }
    public TextMeshProUGUI roundText1;
    public TextMeshProUGUI roundText2;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI gameOverText;
    private int countdown = 3;
    private int roundNumber = 0;
    void Start() {
        roundText2.gameObject.SetActive(false);
        countdownText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
    }
    public void StartNewRound() {
        roundNumber++;
        UpdateRoundText();
    }

    private void UpdateRoundText() {
        roundText1.text = "Round: " + roundNumber;
        roundText2.text = "Round " + roundNumber;

        StartCoroutine(PauseAndCountDown());
    }

    private IEnumerator PauseAndCountDown() {
        Time.timeScale = 0;

        roundText2.gameObject.SetActive(true); 
        yield return new WaitForSecondsRealtime(3);
        roundText2.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(1);
        countdownText.gameObject.SetActive(true);
        while (countdown >= 0) {
            countdownText.text = "" + countdown;
            yield return new WaitForSecondsRealtime(1);
            countdown--;
            if(countdown == -1) {
                countdownText.text = "Fight!";
                yield return new WaitForSecondsRealtime(1);
                countdownText.gameObject.SetActive(false);
            }
        }
        countdown = 3;
        countdownText.text = "3";

        Time.timeScale = 1;
    }

    public void GameIsOverText() {
        gameOverText.gameObject.SetActive(true);
    }

    void Update() {
        
    }
}
