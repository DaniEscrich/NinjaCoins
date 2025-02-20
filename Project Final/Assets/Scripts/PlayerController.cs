using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isAlive = true;
    public float RunSpeed;
    public float HorizontalSpeed;
    public Rigidbody rb;
    float horizontalInput;

    private float originalSpeed;  // Para almacenar la velocidad original

    [SerializeField] private float JumpForce = 350;
    [SerializeField] private LayerMask GroundMask;

    // Animations
    private Animator animator;
    private bool isJumping = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        originalSpeed = RunSpeed; // Guardamos la velocidad original
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) return;

        horizontalInput = Input.GetAxis("Horizontal");

        // Detect if the player is grounded
        float playerHeight = GetComponent<Collider>().bounds.size.y;
        bool IsGrounded = Physics.Raycast(transform.position, Vector3.down, (playerHeight / 2) + 0.1f, GroundMask);

        // Handle running animation
        if (IsGrounded)
        {
            animator.SetBool("isRunning", true);  // Animation for running
        }
        else
        {
            animator.SetBool("isRunning", false);  // Stop running animation when in the air
        }

        // Check if we press space to jump
        if (Input.GetKeyDown(KeyCode.Space) && isAlive && IsGrounded)
        {
            Jump();
        }

        // Handle jumping animation
        if (isJumping)
        {
            animator.SetBool("isJumping", true);  // Set jumping animation
        }
        else
        {
            animator.SetBool("isJumping", false);  // Stop jumping animation when grounded
        }
    }

    public void FixedUpdate()
    {
        if (isAlive)
        {
            Vector3 forwardMovement = transform.forward * RunSpeed * Time.fixedDeltaTime;
            Vector3 horizontalMovement = transform.right * horizontalInput * HorizontalSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMovement + horizontalMovement);
        }
    }

    public void Jump()
    {
        isJumping = true;
        SoundManager.PlaySound("Jump");
        rb.AddForce(Vector3.up * JumpForce);
    }

    // Handle landing
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;  // Player is back on the ground
            animator.SetBool("isJumping", false);  // Stop jump animation
        }

        if (collision.gameObject.name == "Graphic")
        {
            Dead();
        }

        if (collision.gameObject.name == "Coin(Clone)")
        {
            SoundManager.PlaySound("Coin");
            Destroy(collision.gameObject);
            GameManager.MyInstance.Score += 1;
        }
    }

    // Función que activa el powerup de velocidad
    public void ActivateSpeedBoost(float boostMultiplier, float boostDuration)
    {
        StartCoroutine(SpeedBoostCoroutine(boostMultiplier, boostDuration));
    }

    // Coroutine para manejar la duración del powerup de velocidad
    private IEnumerator SpeedBoostCoroutine(float boostMultiplier, float boostDuration)
    {
        RunSpeed *= boostMultiplier;  // Aumentamos la velocidad

        // Esperamos durante la duración del powerup
        yield return new WaitForSeconds(boostDuration);

        // Restauramos la velocidad original
        RunSpeed = originalSpeed;
    }

    public void Dead()
    {
        isAlive = false;
        animator.SetTrigger("Dead");  // Play the death animation
        GameManager.MyInstance.GameoverPanel.SetActive(true);
    }
}
