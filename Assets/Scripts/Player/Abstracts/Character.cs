using System;
using UnityEngine;

public abstract class Character : MonoBehaviour {
    public Rigidbody2D rb;
    public float groundSpeed, jumpSpeed, acceleration;
    [Range(0f, 1f)]
    public float groundDecay;
    public bool grounded;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    public Animator animator;

    public float health = 100f;
    private int lives = 2;

    private int attackCounter = 0;
    private float lastAttackTime = 0f;
    private float lastNormalAttackTime = 0f;
    private const float normalAttackCooldown = 0.5f;
    private const float maxComboDelay = 1.0f;
    private const float heavyAttackCooldown = 10.0f;
    private float lastHeavyAttackTime = 5.0f;
    protected float xInput;
    protected bool jumpInput;
    
    void Start() {
    }

    void Update() {
        GetMovementInput();
        HandleJump();
        UpdateAnimations();
        HandleAttack();
    }

    protected abstract void GetMovementInput();
    protected virtual void MoveWithInput() {}
    protected abstract KeyCode GetAttackKey();

    private void HandleAttack() {
    float currentTime = Time.time;

    if (Input.GetKeyDown(GetAttackKey())) {
        if (attackCounter >= 3) {
            if (currentTime - lastHeavyAttackTime >= heavyAttackCooldown) {
                TriggerHeavyAttack();
                lastHeavyAttackTime = currentTime;
                attackCounter = 0;
            } else {
                Debug.Log("Heavy attack is on cooldown.");
            }
        } else {
            if (currentTime - lastNormalAttackTime >= normalAttackCooldown) {
                if (currentTime - lastAttackTime <= maxComboDelay) {
                    attackCounter++;
                } else {
                    attackCounter = 1;
                }
                lastAttackTime = currentTime;
                lastNormalAttackTime = currentTime;
                TriggerAttack();
            } else {
                Debug.Log("Normal attack is on cooldown.");
            }
        }
    }
}


    private void TriggerAttack() {
        animator.SetBool("IsAttacking", true);
    }

    private void TriggerHeavyAttack() {
        animator.SetBool("IsHeavyAttack", true);
    }

    private void UpdateAnimations() {
        animator.SetFloat("Speed", Mathf.Abs(xInput));

        bool isJumping = !grounded && rb.velocity.y > 0;
        animator.SetBool("IsJumping", isJumping);

        bool isFalling = !grounded && rb.velocity.y <= 0;
        animator.SetBool("IsFalling", isFalling);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") || animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2")) {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) {
                animator.SetBool("IsAttacking", false);
            }
        }
    }

    private void HandleJump() {
        if (jumpInput && grounded) {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    void FixedUpdate() {
        CheckGround();
        ApplyFriction();
        MoveWithInput();
    }

    private void ApplyFriction() {
        if (grounded && xInput == 0 && rb.velocity.y <= 0) {
            rb.velocity *= groundDecay;
        }
    }

    private void CheckGround() {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            lives -= 1;
            if (lives == 0) {
                Die();
            }
        }
    }

    private void Die() {
        Debug.Log("Character has died.");
    }

    public void ResetAttack() {
        animator.SetBool("IsAttacking", false);
    }
    public void ResetHeavyAttack() {
        animator.SetBool("IsHeavyAttack", false);
    }

}
