using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public Transform[] spawnPoints;

    public Transform GetSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }
}
