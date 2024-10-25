using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public Rigidbody2D rb;
    public float groundSpeed, jumpSpeed, acceleration;
    [Range (0f, 1f)]
    public float groundDecay;
    public bool grounded;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    public Animator animator;

    float xInput;
    float yInput;
    bool jumpInput;
    void Start() {
    }

    void Update() {
        GetInput();
        HandleJump();
        UpdateAnimations();
    }

    private void UpdateAnimations() {
        animator.SetFloat("Speed", Mathf.Abs(xInput));

        bool isJumping = !grounded && rb.velocity.y > 0;
        animator.SetBool("IsJumping", isJumping);

        bool isFalling = !grounded && rb.velocity.y <= 0;
        animator.SetBool("IsFalling", isFalling);
    }

    private void MoveWithInput() {
        if (Mathf.Abs(xInput) > 0) {
            float increment = xInput * acceleration;
            float newSpeed = Mathf.Clamp(rb.velocity.x + increment, -groundSpeed, groundSpeed);
            rb.velocity = new Vector2(newSpeed, rb.velocity.y);

            Vector3 currentScale = transform.localScale;
            float direction = Mathf.Sign(xInput);
            transform.localScale = new Vector3(direction * Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }
    }

    private void HandleJump()
    {
        if (jumpInput && grounded) {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    private void GetInput() {
        if(Input.GetKey(KeyCode.A)) {
            xInput = -1f;
        } else if (Input.GetKey(KeyCode.D)) {
            xInput = 1f;
        } else {
            xInput = 0f;
        }
        jumpInput = Input.GetKey(KeyCode.W);
    }

    void FixedUpdate() {
        CheckGround();
        ApplyFriction();
        MoveWithInput();
    }

    private void ApplyFriction() {
        if(grounded && xInput == 0 && rb.velocity.y <= 0) {
            rb.velocity *= groundDecay;
        }
    }

    private void CheckGround() {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }
}