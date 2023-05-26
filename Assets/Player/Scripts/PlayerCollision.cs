using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCollision : MonoBehaviour
{
    public BeatDetection beatDetection;
    public float beatDetectionDuration = 0.5f; // Duration for which beat detection remains true
    public int maxLives = 5;
    public TMP_Text lifeText;

    private int currentLives;
    private bool isInRange = false;
    private GameObject currentSlime;
    private bool isBeatExtended = false;
    private float beatDetectionTimer = 0f;

    private void Start()
    {
        currentLives = maxLives;
        UpdateLifeText();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            isInRange = true;
            currentSlime = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            isInRange = false;
            currentSlime = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (beatDetection.IsBeatDetected())
            {
                if (currentSlime != null)
                {
                    SlimeEnemyMovement slimeMovement = currentSlime.GetComponent<SlimeEnemyMovement>();
                    if (slimeMovement != null)
                    {
                        slimeMovement.Die();
                        return; // Exit the method to avoid reducing a life
                    }
                }
            }
            else
            {
                ExtendBeatDetection(); // Extend the beat detection duration
                ReduceLife(); // Reduce a life if the swing doesn't coincide with the beat
                
            }
        }

        // Update beat detection timer
        if (beatDetectionTimer > 0f)
        {
            beatDetectionTimer -= Time.deltaTime;
            if (beatDetectionTimer <= 0f)
            {
                isBeatExtended = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if (!beatDetection.IsBeatDetected() && !isBeatExtended)
            {
                ReduceLife();
            }
        }
    }

    private void ReduceLife()
    {
        currentLives--;
        UpdateLifeText();

        if (currentLives <= 0)
        {
            // Game over logic goes here
            Debug.Log("Game Over");
        }
    }

    private void UpdateLifeText()
    {
        if (lifeText != null)
        {
            lifeText.text = "Lives: " + currentLives;
        }
    }

    // Call this method to extend the duration of beat detection
    private void ExtendBeatDetection()
    {
        isBeatExtended = true;
        beatDetectionTimer = beatDetectionDuration;
    }
}