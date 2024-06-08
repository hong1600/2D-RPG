using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoint = new List<Transform>();
    [SerializeField] GameObject enemy;

    public int enemyCount;
    [SerializeField]int maxCount;
    float curTime;
    [SerializeField] float spawnTime;

    bool stopEnemy;

    public static EnemySpawn instance;
    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (enemyCount < maxCount && curTime >= spawnTime)
        {
            enemySpawn();
        }
        curTime += Time.deltaTime;
    }

    private void enemySpawn()
    {
        curTime = 0;

        int rand = Random.Range(0, 3);

        Instantiate(enemy, spawnPoint[rand].position, Quaternion.identity);

        enemyCount++;
    }

}
