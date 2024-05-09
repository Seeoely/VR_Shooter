using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnPoints : MonoBehaviour
{
    public Transform[] spawnPoints;

    private void Awake()
    {
        // Get all child spawn points
        spawnPoints = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPoints[i] = transform.GetChild(i);
        }
    }

    public Transform GetRandomSpawnPoint()
    {
        // Get a random spawn point from the array
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex];
    }
}
