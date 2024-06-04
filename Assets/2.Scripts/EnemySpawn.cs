using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoint = new List<Transform>();
    [SerializeField] float spawnTime = 3;
    [SerializeField] GameObject enemy;

    void Start()
    {
        InvokeRepeating("enemySpawn", 1, spawnTime);
    }

    private void enemySpawn()
    {
        int rand = Random.Range(0, 3);

        Instantiate(enemy, spawnPoint[rand].position, Quaternion.identity);
    }
}
