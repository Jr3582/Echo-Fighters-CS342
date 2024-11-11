using UnityEngine;
using UnityEngine.UI;

public class Player2 : Player {
    private Player1 player1;
    public CircleCollider2D attackCollider;
    [SerializeField] private AttackCoolDownUI player2AttackCoolDownUI;
    [SerializeField] private Image player2HealthBar;
    protected override Image HealthBar => player2HealthBar;

    protected override void Start() {
        base.Start();
        player2AttackCoolDownUI.StartHeavyAttackCooldown();
        player1 = FindObjectOfType<Player1>();
        maxHealth = currentHealth;
    }
    protected override void Update() {
        base.Update();
        HandleAttack();
    }


    protected override float GetHorizontalInput() {
        if (Input.GetKey(KeyCode.LeftArrow)) return -1f;
        if (Input.GetKey(KeyCode.RightArrow)) return 1f;
        return 0f;
    }

    protected override bool GetJumpInput() {
        return Input.GetKey(KeyCode.UpArrow);
    }

    protected override void MoveWithInput() {
        if (Mathf.Abs(xInput) > 0) {
            float increment = xInput * acceleration;
            float newSpeed = Mathf.Clamp(rb.velocity.x + increment, -groundSpeed, groundSpeed);
            rb.velocity = new Vector2(newSpeed, rb.velocity.y);

            Vector3 newPosition = transform.position + new Vector3(newSpeed * Time.deltaTime, 0, 0);
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX); // Clamp position
            transform.position = newPosition;

            Vector3 currentScale = transform.localScale;
            float direction = Mathf.Sign(xInput);
            transform.localScale = new Vector3(-direction * Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }
    }

    protected void HandleAttack() {
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
                player2AttackCoolDownUI.StartHeavyAttackCooldown();
            }
        }
    }
    protected void CheckForDamage(int damage) {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackCollider.bounds.center, attackCollider.radius);
        foreach (var hitCollider in hitColliders) {
            if (hitCollider.CompareTag("Player1")) {
                Player1 player1 = hitCollider.GetComponentInParent<Player1>();
                if (player1 != null) {
                    player1.TakeDamage(damage);
                }
            }
        }
    }
    public override void TakeDamage(int damage) {
        if (!isBlocking) {
            currentHealth -= damage;
            float healthPercentage = (float)currentHealth / maxHealth;
            player2HealthBar.fillAmount = healthPercentage;
            HasBeenHit();
        } else if (isBlocking) {
            currentHealth -= (int)(damage * DamageReduction);
            float healthPercentage = (float)currentHealth / maxHealth;
            player2HealthBar.fillAmount = healthPercentage;
            HasBeenHit();
        }
        if (currentHealth <= 0) {
            lives -= 1;
            Die();
        }
    }

    protected override KeyCode GetAttackKey() {
        return KeyCode.B;
    }
    protected override KeyCode GetHeavyAttackKey() {
        return KeyCode.M;
    }
        protected override KeyCode GetBlockKey() {
        return KeyCode.N;
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
