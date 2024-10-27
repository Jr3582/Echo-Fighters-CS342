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
    private bool isAttacking;

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
        if (Input.GetKeyDown(GetAttackKey())) {
            isAttacking = true;
        }
    }

    private void UpdateAnimations() {
        animator.SetFloat("Speed", Mathf.Abs(xInput));

        bool isJumping = !grounded && rb.velocity.y > 0;
        animator.SetBool("IsJumping", isJumping);

        bool isFalling = !grounded && rb.velocity.y <= 0;
        animator.SetBool("IsFalling", isFalling);

        animator.SetBool("IsAttacking", isAttacking);
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
}
