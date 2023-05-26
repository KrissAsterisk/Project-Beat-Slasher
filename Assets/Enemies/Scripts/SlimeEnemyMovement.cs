using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemyMovement : MonoBehaviour
{
    public float jumpForce = 5f;
    public float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool startupFinished = false;
    private bool isInRange = false;
    private bool canBeKilled = false;

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
            Move();
        }

        if (isInRange && canBeKilled && Input.GetKeyDown(KeyCode.Space))
        {
            Die();
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
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
        canBeKilled = true; // Enable the ability to kill the slime once the startup animation finishes
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
        if (collision.gameObject.CompareTag("Platform"))
        {
            JumpToFall();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
