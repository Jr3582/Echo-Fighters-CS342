using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Player : Character {
    [SerializeField]
    protected virtual int maxHealth { get; set; } = 100;
    [SerializeField]
    protected virtual int currentHealth { get; set; } = 100;
    [SerializeField]
    protected virtual float normalAttackCooldown{ get; set; } = 1.0f;
    [SerializeField]
    protected virtual float heavyAttackCooldown { get; set; } = 15.0f;
    [SerializeField]
    protected virtual int heavyAttackDamage { get; set; } = 20;
    [SerializeField]
    protected virtual int normalAttackDamage { get; set; } = 10;
    public float jumpSpeed, groundSpeed, acceleration;
    protected bool jumpInput;

    protected float minX = -12.5f;
    protected float maxX = 12.5f;

    protected float lastNormalAttackTime = 0f;
    protected float lastHeavyAttackTime = 0f;
    public int lives = 2;

    protected override void Update() {
        base.Update();
        HandleJump();
    }
    private void HandleJump() {
        if (jumpInput && grounded) {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
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
            transform.localScale = new Vector3(direction * Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }
    }

    public virtual void TakeDamage(int damage) {
        //Abstract;
    }

    protected void Die() {
        Debug.Log("Player has died.");
    }

    protected override void GetMovementInput() {
        xInput = GetHorizontalInput();
        jumpInput = GetJumpInput();
    }
    protected abstract float GetHorizontalInput();
    protected abstract bool GetJumpInput();
    protected abstract KeyCode GetAttackKey();
    protected abstract KeyCode GetHeavyAttackKey();
}
