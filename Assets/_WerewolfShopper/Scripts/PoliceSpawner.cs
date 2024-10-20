using System.Collections;
using UnityEngine;

public class PoliceSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] policeSpawnPoints;
    [SerializeField] private float spawnRate;
    [SerializeField] private GameObject policePrefab;
    private int spawnIndex;

    public void StartSpawning()
    {
        StartCoroutine("SpawnPolice");
    }

    public void StopSpawning()
    {
        StopCoroutine("SpawnPolice");
    }

    IEnumerator SpawnPolice()
    {
        while (true)
        {
            Instantiate(policePrefab, policeSpawnPoints[spawnIndex].position, Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
            spawnIndex++;
            if(spawnIndex >= policeSpawnPoints.Length)
            {
                spawnIndex = 0;
            }
        }
    }
}
