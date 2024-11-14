using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Player : Character {
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int currentHealth;
    [SerializeField] protected int HeavyAttackDamage = 12;
    [SerializeField] protected int NormalAttackDamage = 8;
    [SerializeField] protected float NormalAttackCooldown = 0.5f;
    [SerializeField] public float HeavyAttackCooldown = 12.5f;
    [SerializeField] protected float DamageReduction = 0.5f;
    public float jumpSpeed, groundSpeed, acceleration;
    protected bool jumpInput;
    protected float minX = -27.5f;
    protected float maxX = 27.5f;
    protected float lastNormalAttackTime = 0f;
    protected float lastHeavyAttackTime = 0f;
    public int lives = 2;
    public GameObject life1;
    public GameObject life2;
    protected abstract Image HealthBar { get; }
    private Vector3 startingPosition;
    protected RoundScript roundScript;
    protected CountdownTimer timer;
    protected bool isBlocking = false;
    protected void Awake() {
        startingPosition = transform.position;
        roundScript = FindObjectOfType<RoundScript>();
        timer = FindObjectOfType<CountdownTimer>();
    }

    protected override void Update() {
        base.Update();
        HandleJump();
        HandleBlock();
    }
    protected void HandleBlock() {
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
        if (lives >= -1) {
            animator.SetTrigger("IsDead");
            if (lives == 1) {
                life1.SetActive(false);
            } else if (lives == 0) {
                life2.SetActive(false);
            } 
        } else {
            GameOver();
        }
    }

    private void GameOver() {
        Debug.Log("Game is over");
    }

    public void FreezeOnLastFrame() {
        animator.speed = 0;
        StartCoroutine(PauseAfterDeath());
    }

    private IEnumerator PauseAfterDeath() {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1;

        Player[] players = FindObjectsOfType<Player>();
        foreach (Player player in players) {
            player.ResetForNewRound(player.startingPosition);
        }
        roundScript.StartNewRound();
        animator.speed = 1;
    }

    public void ResetForNewRound(Vector3 startPosition) {
        currentHealth = maxHealth;
        transform.position = startPosition;
        HealthBar.fillAmount = 1;
        timer.ResetTimer();
    }

    protected override void GetMovementInput() {
        xInput = GetHorizontalInput();
        jumpInput = GetJumpInput();
    }
    protected abstract float GetHorizontalInput();
    protected abstract bool GetJumpInput();
    protected abstract KeyCode GetAttackKey();
    protected abstract KeyCode GetHeavyAttackKey();
    protected abstract KeyCode GetBlockKey();
}