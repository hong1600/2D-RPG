using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill2 : MonoBehaviour
{
    BoxCollider2D box;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        if (Player.instance.transform.localScale.x < 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, -180f, 0);
        }

        Invoke("oncoll", 1f);

        Destroy(gameObject, 2.3f);
    }

    private void oncoll()
    {
        box.enabled = true;
    }
}
