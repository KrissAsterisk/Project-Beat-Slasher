using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private bool isInRange = false;
    private GameObject currentSlime;

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
        if (isInRange && Input.GetKeyDown(KeyCode.Space))
        {
            if (currentSlime != null)
            {
                SlimeEnemyMovement slimeMovement = currentSlime.GetComponent<SlimeEnemyMovement>();
                if (slimeMovement != null)
                {
                    slimeMovement.Die();
                }
            }
        }
    }
}








