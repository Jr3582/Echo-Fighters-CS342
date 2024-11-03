using UnityEngine;
using UnityEngine.UI;

public class Player1 : Player {
    private Player2 player2;
    public CircleCollider2D attackCollider;
    protected new int heavyAttackDamage = 12;
    protected new int normalAttackDamage = 8;
    protected new float normalAttackCooldown  = 0.75f;
    protected new float heavyAttackCooldown = 12.50f;
    
    [SerializeField]
    private AttackCoolDownUI player1AttackCoolDownUI;
    public Image player1HealthBar;

    protected override void Start() {
        base.Start();
        player1AttackCoolDownUI.StartHeavyAttackCooldown();
        player2 = FindObjectOfType<Player2>();
        maxHealth = currentHealth;
    }
    protected override void Update() {
        base.Update();
        HandleAttack();
    }

    protected override float GetHorizontalInput() {
        if (Input.GetKey(KeyCode.A)) return -1f;
        if (Input.GetKey(KeyCode.D)) return 1f;
        return 0f;
    }

    private void HandleAttack() {
        float currentTime = Time.time;

        if (Input.GetKeyDown(GetAttackKey())) {
            if (currentTime - lastNormalAttackTime >= normalAttackCooldown) {
                TriggerAttack();
                lastNormalAttackTime = currentTime;
                CheckForDamage(normalAttackDamage);
            } else {
                Debug.Log("Normal attack is on cooldown.");
            }
        } else if (Input.GetKeyDown(GetHeavyAttackKey())) {
            if (currentTime - lastHeavyAttackTime >= heavyAttackCooldown) {
                TriggerHeavyAttack();
                lastHeavyAttackTime = currentTime;
                CheckForDamage(heavyAttackDamage);
                player1AttackCoolDownUI.StartHeavyAttackCooldown();
            } else {
                Debug.Log("Heavy attack is on cooldown.");
            }
        }
    }
    private void CheckForDamage(int damage) {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackCollider.bounds.center, attackCollider.radius);
        foreach (var hitCollider in hitColliders) {
            if (hitCollider.CompareTag("Player2")) {
                Player2 player2 = hitCollider.GetComponentInParent<Player2>();
                if (player2 != null) {
                    player2.TakeDamage(damage);
                }
            }
        }
    }
    public override void TakeDamage(int damage) {
        currentHealth -= damage;
        float healthPercentage = (float)currentHealth / maxHealth;
        player1HealthBar.fillAmount = healthPercentage;
        if (currentHealth <= 0) {
            Die();
        }
    }
    protected override bool GetJumpInput() {
        return Input.GetKey(KeyCode.W);
    }

    protected override KeyCode GetAttackKey() {
        return KeyCode.F;
    }
    protected override KeyCode GetHeavyAttackKey() {
        return KeyCode.H;
    }

    private void TriggerAttack() {
        animator.SetBool("IsAttacking", true);
    }

    private void TriggerHeavyAttack() {
        animator.SetBool("IsHeavyAttack", true);
    }

    public void ResetAttack() {
        animator.SetBool("IsAttacking", false);
    }

    public void ResetHeavyAttack() {
        animator.SetBool("IsHeavyAttack", false);
    }
}
