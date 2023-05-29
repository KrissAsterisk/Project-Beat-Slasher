using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCollision : MonoBehaviour
{
    public BeatDetection beatDetection;
    public int maxLives = 5;
    public TMP_Text lifeText;

    private int currentLives;
    private GameObject currentSlime;

    private void Start()
    {
        currentLives = maxLives;
        UpdateLifeText();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            currentSlime = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            currentSlime = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
            else
            {
                ReduceLife(); // Reduce a life if the swing doesn't coincide with the beat
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("BOING!");
        if (collision.collider.CompareTag("Enemy"))
        {
            ReduceLife();
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
}
