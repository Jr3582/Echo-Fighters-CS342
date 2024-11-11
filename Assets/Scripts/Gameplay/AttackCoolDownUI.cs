using UnityEngine;
using UnityEngine.UI;

public class AttackCoolDownUI : MonoBehaviour {
    public Image cooldownBar;
    public float heavyAttackCooldown = 20.0f;
    private float cooldownTimeRemaining = 0.0f;

    void Update() {
        if (cooldownTimeRemaining > 0) {
            cooldownTimeRemaining -= Time.deltaTime;
            UpdateCooldownBar();
        }
    }

    public void StartHeavyAttackCooldown() {
        cooldownTimeRemaining = heavyAttackCooldown;
    }

    private void UpdateCooldownBar() {
        if (cooldownBar != null) {
            cooldownBar.fillAmount = cooldownTimeRemaining / heavyAttackCooldown;
        }
    }
}
