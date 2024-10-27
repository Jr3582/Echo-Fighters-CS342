using UnityEngine;

public class Player2 : Player {

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

            Vector3 currentScale = transform.localScale;
            float direction = Mathf.Sign(xInput);
            transform.localScale = new Vector3(-direction * Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }
    }
    protected override KeyCode GetAttackKey() {
        return KeyCode.K;
    }
}
