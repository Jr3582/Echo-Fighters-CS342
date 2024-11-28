using System;
using UnityEngine;
using UnityEngine.UI;

public class Player1 : Player {
    private Player2 player2;
    public CircleCollider2D attackCollider;
    [SerializeField] public AttackCoolDownUI player1AttackCoolDownUI;
    [SerializeField] public Image player1HealthBar;

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
                ResetAttack();
                TriggerAttack();
                lastNormalAttackTime = currentTime;
                CheckForDamage(NormalAttackDamage);
            }
        } else if (Input.GetKeyDown(GetHeavyAttackKey())) {
            if (currentTime - lastHeavyAttackTime >= HeavyAttackCooldown) {
                ResetHeavyAttack();
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

            ApplyKnockback(player2.transform.position, 300f);
        } else if (isBlocking) {
            currentHealth -= (int)(damage * DamageReduction);
            float healthPercentage = (float)currentHealth / maxHealth;
            player1HealthBar.fillAmount = healthPercentage;
            HasBeenHit();

            ApplyKnockback(player2.transform.position, 100f);
        }
        if (currentHealth <= 0) {
            lives -= 1;
            Debug.Log(lives);
            StartCoroutine(Die());
        }
    }
    private void ApplyKnockback(Vector3 attackerPosition, float knockbackForce) {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null) return;
        Vector2 knockbackDirection = (transform.position - attackerPosition).normalized;
        rb.AddForce(knockbackDirection * knockbackForce);
    }
    protected override bool GetJumpInput() {
        return Input.GetKey(KeyCode.W);
    }

    protected override KeyCode GetAttackKey() {
        return KeyCode.R;
    }
    protected override KeyCode GetHeavyAttackKey() {
        return KeyCode.Y;
    }
    protected override KeyCode GetBlockKey() {
        return KeyCode.T;
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
    public void ResetToIdle() {
        animator.Play("Idle");
    }
}
