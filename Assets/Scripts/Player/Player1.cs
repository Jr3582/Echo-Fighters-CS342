using System;
using UnityEngine;
using UnityEngine.UI;

public class Player1 : Player {
    private Player2 player2;
    public CircleCollider2D attackCollider;
    protected new int heavyAttackDamage = 12;
    protected new int normalAttackDamage = 200;
    protected new float normalAttackCooldown  = 0.75f;
    protected new float heavyAttackCooldown = 12.50f;
    [SerializeField] private AttackCoolDownUI player1AttackCoolDownUI;
    [SerializeField] public Image player1HealthBar;
    private bool isBlocking = false;

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
        HandleBlock();
    }

    private void HandleBlock() {
        if (Input.GetKey(GetBlockKey())) {
            StartBlocking();
        } else {
            StopBlocking();
        }
    }

    private void StopBlocking() {
        if(isBlocking) {
            isBlocking = false;
            animator.SetBool("IsBlocking", false);
        }
    
    }

    private void StartBlocking() {
        if(!isBlocking) {
            isBlocking = true;
            animator.SetBool("IsBlocking", true);
        }
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
            }
        } else if (Input.GetKeyDown(GetHeavyAttackKey())) {
            if (currentTime - lastHeavyAttackTime >= heavyAttackCooldown) {
                TriggerHeavyAttack();
                lastHeavyAttackTime = currentTime;
                CheckForDamage(heavyAttackDamage);
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
            currentHealth -= (int)(damage * 0.8);
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
