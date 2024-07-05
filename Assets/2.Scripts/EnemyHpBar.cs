using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{
    [SerializeField] GameObject hpbarObj;

    List<Transform> enemyList = new List<Transform>();
    List<GameObject> enemyhpbarList = new List<GameObject>();

    Camera cam;

    private void Start()
    {
        cam = Camera.main;

        GameObject[] targetEnemy = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < targetEnemy.Length; i++)
        {
            enemyList.Add(targetEnemy[i].transform);
            GameObject ihpbar = Instantiate(hpbarObj, targetEnemy[i].transform.position,
                Quaternion.identity, transform);
            enemyhpbarList.Add(ihpbar);
        }

    }

    private void Update()
    {
        for (int i = 0;i < enemyhpbarList.Count;i++) 
        {
            enemyhpbarList[i].transform.position = 
                cam.WorldToScreenPoint(enemyList[i].position + new Vector3(0, 1, 0));
        }
    }
}
