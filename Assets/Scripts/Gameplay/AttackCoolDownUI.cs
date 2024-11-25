using UnityEngine;
using UnityEngine.UI;

public class AttackCoolDownUI : MonoBehaviour {
    public Image cooldownBar;
    public float heavyAttackCooldown = 20.0f;
    private float cooldownTimeRemaining = 0.0f;

    void Update() {
        if (cooldownTimeRemaining > 0) {
            Debug.Log(Time.timeScale);
            cooldownTimeRemaining = Mathf.Max(cooldownTimeRemaining - Time.deltaTime, 0);
            UpdateCooldownBar();
        }
    }

    public void StartHeavyAttackCooldown() {
        cooldownTimeRemaining = heavyAttackCooldown;
    }

    private void UpdateCooldownBar() {
        if (cooldownBar != null) {
            cooldownBar.fillAmount = cooldownTimeRemaining / heavyAttackCooldown;
            // Debug.Log($"Cooldown Remaining: {cooldownTimeRemaining:F2} | Fill Amount: {cooldownBar.fillAmount:F2}");
        }
    }
}
