using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    public GameObject slimePrefab;
    public BeatDetection beatDetection;
    public float spawnDelay = 0.2f;
    public float beatThreshold = 4f;

    private void Start()
    {
        StartCoroutine(SpawnSlimes());
    }

    private IEnumerator SpawnSlimes()
    {
        yield return new WaitForSeconds(spawnDelay);

        while (true)
        {
            yield return new WaitUntil(() => beatDetection.IsBeatDetected());

            Instantiate(slimePrefab, transform.position, Quaternion.identity);

            yield return null;
        }
    }
}

