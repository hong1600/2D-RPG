using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossSkillDestroy : MonoBehaviour
{
    BoxCollider2D box;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        Invoke("oncoll", 0.5f);
        Destroy(gameObject, 1);
    }

    private void oncoll()
    {
        box.enabled = true;
    }
}
