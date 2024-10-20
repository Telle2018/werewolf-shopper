using System.Collections;
using UnityEngine;

public class PersonSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnRate;
    [SerializeField] private GameObject personPrefab;
    private int spawnIndex;

    public void StartSpawning()
    {
        StartCoroutine("SpawnPerson");
    }

    public void StopSpawning()
    {
        StopCoroutine("SpawnPerson");
    }

    IEnumerator SpawnPerson()
    {
        while (true)
        {
            Instantiate(personPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
            spawnIndex++;
            if (spawnIndex >= spawnPoints.Length)
            {
                spawnIndex = 0;
            }
        }
    }
}
