using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill4 : MonoBehaviour
{
    BoxCollider2D box;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        Destroy(gameObject, 3);
    }
}
