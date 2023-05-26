using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    public GameObject slimePrefab;
    public float beatDuration = 1f;
    public float spawnDelay = 0.2f;
    public int[] spawnBeats;

    private float spawnTime;

    private void Start()
    {
        spawnTime = beatDuration - spawnDelay;
        StartCoroutine(SpawnSlimes());
    }

    private IEnumerator SpawnSlimes()
    {
        yield return new WaitForSeconds(spawnDelay);

        foreach (int beat in spawnBeats)
        {
            yield return new WaitForSeconds(spawnTime);

            Instantiate(slimePrefab, transform.position, Quaternion.identity);

            yield return null;
        }
    }
}
