using UnityEngine;

public class Player1 : Player {

    protected override float GetHorizontalInput() {
        if (Input.GetKey(KeyCode.A)) return -1f;
        if (Input.GetKey(KeyCode.D)) return 1f;
        return 0f;
    }

    protected override bool GetJumpInput() {
        return Input.GetKey(KeyCode.W);
    }

    protected override KeyCode GetAttackKey() {
        return KeyCode.F;
    }
}
