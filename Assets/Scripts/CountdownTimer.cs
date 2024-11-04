using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour {
    public int startTime = 180;
    private float currentTime;
    public TextMeshProUGUI countdownText;
    void Start() {
        currentTime = startTime;
    }

    void Update() {
        currentTime -= Time.deltaTime;

        currentTime = Mathf.Max(0, currentTime);

        countdownText.text = string.Format("{0:00}", currentTime);

        if (currentTime <= 0) {
            Debug.Log("Time's up!");
        }
    }
}
