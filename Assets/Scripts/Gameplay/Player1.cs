using System;
using UnityEngine;
using UnityEngine.UI;

public class Player1 : Player {
    private Player2 player2;
    public CircleCollider2D attackCollider;
    [SerializeField] private AttackCoolDownUI player1AttackCoolDownUI;
    [SerializeField] private Image player1HealthBar;

    protected override Image HealthBar => player1HealthBar;
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
            if (currentTime - lastNormalAttackTime >= NormalAttackCooldown) {
                TriggerAttack();
                lastNormalAttackTime = currentTime;
                CheckForDamage(NormalAttackDamage);
            }
        } else if (Input.GetKeyDown(GetHeavyAttackKey())) {
            if (currentTime - lastHeavyAttackTime >= HeavyAttackCooldown) {
                TriggerHeavyAttack();
                lastHeavyAttackTime = currentTime;
                CheckForDamage(HeavyAttackDamage);
                player1AttackCoolDownUI.StartHeavyAttackCooldown();
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
        if (!isBlocking) {
            currentHealth -= damage;
            float healthPercentage = (float)currentHealth / maxHealth;
            player1HealthBar.fillAmount = healthPercentage;
            HasBeenHit();
        } else if (isBlocking) {
            currentHealth -= (int)(damage * DamageReduction);
            float healthPercentage = (float)currentHealth / maxHealth;
            player1HealthBar.fillAmount = healthPercentage;
            HasBeenHit();
        }
        if (currentHealth <= 0) {
            lives -= 1;
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
    protected override KeyCode GetBlockKey() {
        return KeyCode.G;
    }

    private void TriggerAttack() {
        animator.SetBool("IsAttacking", true);
    }

    private void TriggerHeavyAttack() {
        animator.SetBool("IsHeavyAttack", true);
    }

    private void HasBeenHit() {
        animator.SetTrigger("IsHit");
    }

    public void ResetAttack() {
        animator.SetBool("IsAttacking", false);
    }

    public void ResetHeavyAttack() {
        animator.SetBool("IsHeavyAttack", false);
    }
}
