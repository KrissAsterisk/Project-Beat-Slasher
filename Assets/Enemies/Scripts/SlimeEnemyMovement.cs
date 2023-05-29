using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemyMovement : MonoBehaviour
{
    public float jumpForce = 5f;
    public float moveSpeed = 2f;
    public float moveDuration = 1f; // Duration of the horizontal movement

    private Rigidbody2D rb;
    private Animator animator;
    private bool startupFinished = false;
    private bool isInRange = false;
    private bool canBeKilled = false;
    private bool isJumping = false; // New variable to track the jumping state

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        PlayStartupAnimation();
    }

    private void Update()
    {
        if (startupFinished)
        {
            if (isJumping) // Only move when the slime is jumping
            {
                Move();
            }

            if (isJumping && isInRange && canBeKilled && Input.GetKeyDown(KeyCode.Space))
            {
                Die();
            }

            if (isJumping && rb.velocity.y <= -1) // Stop moving if descending or at the peak of the jump
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

private void Move()
{
    // Calculate the distance to move based on moveSpeed and moveDuration
    float distance = moveSpeed * Time.deltaTime;
    Vector2 targetPosition = rb.position + new Vector2(-distance, 0f);

    // Move towards the target position using MovePosition
    rb.MovePosition(targetPosition);

    // Check if the target position is reached
    if (Mathf.Abs(targetPosition.x - rb.position.x) < 0.05f)
    {
        if (!isJumping || rb.velocity.y <= -1) // Stop the slime's movement if it's not jumping or descending
        {
            isJumping = false; // Set isJumping to false when the target position is reached
            rb.velocity = Vector2.zero; // Stop the slime's movement
        }
    }
}


    private void PlayStartupAnimation()
    {
        animator.SetTrigger("Startup");
        Invoke(nameof(SetStartupFinished), 1f);
    }

    private void SetStartupFinished()
    {
        startupFinished = true;
        animator.SetBool("StartupFinished", true);
        animator.SetBool("JumpToFall", true);
        canBeKilled = true; // Enable the ability to kill the slime once the startup animation finishes; unnecessary but im keeping it :)

        Jump(); // Call the Jump method to make the slime jump initially
    }

    private void Jump()
    {
        StartCoroutine(MoveAndJump());
    }

    private IEnumerator MoveAndJump()
{
    JumpToFall();

    // Move horizontally to the left for the specified duration
    float elapsedTime = 0f;
    while (elapsedTime < moveDuration)
    {
        if (isJumping)
        {
            Move();
        }
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    // Reset the horizontal velocity to zero
    rb.velocity = new Vector2(0f, rb.velocity.y);

    isJumping = true; // Set the jumping state to true when the slime jumps
}

    private void JumpToFall()
    {
        animator.SetTrigger("JumpToFall");
    }

    public void Die()
    {
        if (startupFinished)
        {
            startupFinished = false;
            animator.SetTrigger("Death");
            Destroy(gameObject, 1f); // Destroy the slime after 1 second (adjust as needed)
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerSpace") && startupFinished)
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerSpace") && startupFinished)
        {
            isInRange = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isJumping) // Stop updating movement when the slime has jumped
        {
            isJumping = false;
            rb.velocity = Vector2.zero;
        }
    }
}
