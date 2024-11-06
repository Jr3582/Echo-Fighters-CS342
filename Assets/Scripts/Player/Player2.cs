using UnityEngine;
using UnityEngine.UI;

public class Player2 : Player {
    private Player1 player1;
    public CircleCollider2D attackCollider;
    protected new int heavyAttackDamage = 12;
    protected new int normalAttackDamage = 100;
    protected new float normalAttackCooldown = 1.50f;
    protected new float heavyAttackCooldown = 20.0f;
    protected new int maxHealth = 125;
    protected new int currentHealth = 125;
    [SerializeField]
    private AttackCoolDownUI player2AttackCoolDownUI;
    public Image player2HealthBar;
    private bool isBlocking = false;

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
                player2AttackCoolDownUI.StartHeavyAttackCooldown();
            }
        }
    }

    private void CheckForDamage(int damage) {
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
            currentHealth -= (int)(damage * 0.6);
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
