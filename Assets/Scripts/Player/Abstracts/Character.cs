using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public abstract class Character : MonoBehaviour {
    public Rigidbody2D rb;
    [Range(0f, 1f)]
    public float groundDecay;
    public bool grounded;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    public Animator animator;
    protected float xInput;

    protected virtual void Start() {
        //Abstract
    }

    protected virtual void Update() {
        GetMovementInput();
        UpdateAnimations();
    }

    protected abstract void GetMovementInput();
    protected virtual void MoveWithInput() {}

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

    private void UpdateAnimations() {
        animator.SetFloat("Speed", Mathf.Abs(xInput));
        bool isJumping = !grounded && rb.velocity.y > 0;
        animator.SetBool("IsJumping", isJumping);
        bool isFalling = !grounded && rb.velocity.y <= 0;
        animator.SetBool("IsFalling", isFalling);
    }
}
