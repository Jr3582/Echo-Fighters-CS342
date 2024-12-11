using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player1 : Player {
    private Player2 player2;
    public CircleCollider2D attackCollider;
    [SerializeField] public AttackCoolDownUI player1AttackCoolDownUI;
    [SerializeField] public Image player1HealthBar;
    public RuntimeAnimatorController oldAnimationController;
    public RuntimeAnimatorController newAnimationController;
    public SpriteRenderer spriteRenderer;
    public Sprite oldSprite;
    public Sprite newSprite;
    public string prefabName;
    protected override Image HealthBar => player1HealthBar;
    protected override void Start() {
        base.Start();
        player1AttackCoolDownUI.StartHeavyAttackCooldown();
        player2 = FindObjectOfType<Player2>();
        maxHealth = currentHealth;
        StartCoroutine(CheckHealthPeriodically());
    }
    protected override void Update() {
        base.Update();
        HandleAttack();
    }
    private IEnumerator CheckHealthPeriodically() {
        while(true) {
            if (prefabName == "P1JawnSeena" && ((float)currentHealth / maxHealth) < 0.50f) {
                spriteRenderer.sprite = newSprite;
                animator.runtimeAnimatorController = newAnimationController;
                groundSpeed = 10.0f;
                jumpSpeed = 8.0f;
                NormalAttackDamage = 15;
            } else if(prefabName == "P1JawnSeena" && ((float)currentHealth / maxHealth) >= 0.50f) {
                spriteRenderer.sprite = oldSprite;
                animator.runtimeAnimatorController = oldAnimationController;
                groundSpeed = 5.0f;
                jumpSpeed = 4.0f;
                NormalAttackDamage = 10;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    protected override float GetHorizontalInput() {
        if (Input.GetKey(KeyCode.A)) return -1f;
        if (Input.GetKey(KeyCode.D)) return 1f;
        return 0f;
    }

    private void HandleAttack() {
        float currentTime = Time.time;

        if (Input.GetKeyDown(GetAttackKey()) && player2.currentHealth >= 0) {
            if (currentTime - lastNormalAttackTime >= NormalAttackCooldown) {
                ResetAttack();
                TriggerAttack();
                lastNormalAttackTime = currentTime;
                CheckForDamage(NormalAttackDamage);
            }
        } else if (Input.GetKeyDown(GetHeavyAttackKey()) && player2.currentHealth >= 0) {
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

    internal void ResetForNewRound((float, float, float) value)
    {
        throw new NotImplementedException();
    }
}
