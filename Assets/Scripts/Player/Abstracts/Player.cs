using UnityEngine;

public abstract class Player : Character {
    
    protected override void GetMovementInput() {
        xInput = GetHorizontalInput();
        jumpInput = GetJumpInput();
    }
    protected override void MoveWithInput() {
        if (Mathf.Abs(xInput) > 0) {
            float increment = xInput * acceleration;
            float newSpeed = Mathf.Clamp(rb.velocity.x + increment, -groundSpeed, groundSpeed);
            rb.velocity = new Vector2(newSpeed, rb.velocity.y);

            Vector3 currentScale = transform.localScale;
            float direction = Mathf.Sign(xInput);
            transform.localScale = new Vector3(direction * Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }
    }
    protected abstract float GetHorizontalInput();
    protected abstract bool GetJumpInput();
}
