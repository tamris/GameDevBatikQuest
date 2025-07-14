using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private PlayerController playerController;

    private float mobileInputX = 0f;
    private Vector2 moveInput;

    private enum MovementState { idle = 0, run = 1, jump = 2, attack = 3, death = 4 }

    [Header("Jump Settings")]
    [SerializeField] private LayerMask jumpableGround;
    private BoxCollider2D coll;

    [Header("Health System")]
    public int maxHealth = 100;
    private int currentHealth;
    public TextMeshProUGUI healthText;
    public Image healthBarFill;

    [Header("Coin System")]
    public int currentCoin = 0;
    public TextMeshProUGUI coinText;

    [Header("Knockback Settings")]
    [SerializeField] private float knockBackTime = 0.2f;
    [SerializeField] private float knockBackThrust = 10f;
    private bool isKnockedBack = false;

    private bool isAttacking = false;
    private bool isDead = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        playerController = new PlayerController();

        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    private void Start()
    {
        currentCoin = 0;
        if (coinText != null)
        {
            coinText.text = currentCoin.ToString();
        }
    }

    private void OnEnable()
    {
        playerController.Enable();
        playerController.Movement.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerController.Movement.Move.canceled += ctx => moveInput = Vector2.zero;
        playerController.Movement.Jump.performed += ctx => Jump();
        playerController.Movement.Attack.performed += ctx => Attack();
    }

    private void OnDisable()
    {
        playerController.Disable();
    }

    private void Update()
    {
        if (isDead) return;

        if (Application.isMobilePlatform)
        {
            moveInput = new Vector2(mobileInputX, 0f);
        }
        else
        {
            moveInput = playerController.Movement.Move.ReadValue<Vector2>();
        }
    }

    private void FixedUpdate()
    {
        if (isKnockedBack || isAttacking || isDead) return;

        Vector2 targetVelocity = new Vector2((moveInput.x + mobileInputX) * moveSpeed, rb.velocity.y);
        rb.velocity = targetVelocity;

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (isDead)
        {
            anim.SetInteger("state", (int)MovementState.death);
            return;
        }

        MovementState state;

        float horizontal = moveInput.x != 0 ? moveInput.x : mobileInputX;

        if (horizontal != 0f)
        {
            state = MovementState.run;
            sprite.flipX = horizontal < 0f;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jump;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void Jump()
    {
        if (isDead) return;

        if (isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void Attack()
    {
        if (isAttacking || isDead) return;

        isAttacking = true;
        anim.SetInteger("state", (int)MovementState.attack);

        StartCoroutine(ResetAttackState());
    }

    private IEnumerator ResetAttackState()
    {
        yield return new WaitForSeconds(0.5f); // Sesuaikan dengan durasi animasi attack
        isAttacking = false;
    }

    public void MoveRight(bool isPressed)
    {
        if (isPressed)
            mobileInputX = 1f;
        else if (mobileInputX == 1f)
            mobileInputX = 0f;
    }

    public void MoveLeft(bool isPressed)
    {
        if (isPressed)
            mobileInputX = -1f;
        else if (mobileInputX == -1f)
            mobileInputX = 0f;
    }

    public void MobileJump()
    {
        if (isGrounded() && !isDead)
        {
            Jump();
        }
    }

    public void TakeDamage(int damage, Vector2 direction)
    {
        if (isKnockedBack || isDead) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;

            // Stop movement and trigger death animation
            rb.velocity = Vector2.zero;
            anim.SetInteger("state", (int)MovementState.death);
            rb.bodyType = RigidbodyType2D.Kinematic;
            coll.enabled = false;

            Debug.Log("Player Mati");

            // Optional: Restart level after delay
            StartCoroutine(HandleDeath());
        }
        else
        {
            StartCoroutine(HandleKnockback(direction.normalized));
        }

        UpdateHealthUI();
    }

    private IEnumerator HandleKnockback(Vector2 direction)
    {
        isKnockedBack = true;
        rb.velocity = Vector2.zero;

        Vector2 force = direction * knockBackThrust * rb.mass;
        rb.AddForce(force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(1.5f); // tunggu animasi death selesai
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void addBatik(int amount)
    {
        currentCoin += amount;
        if (coinText != null)
        {
            coinText.text = "" + currentCoin.ToString();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthBarFill != null)
        {
            float fillAmount = Mathf.Clamp01((float)currentHealth / maxHealth);
            healthBarFill.fillAmount = fillAmount;
        }
    }
}
