using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlatform : MonoBehaviour
{
    BoxCollider2D box;

    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;
    [SerializeField] Transform desPos;
    [SerializeField] float speed;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();

        transform.position = startPos.position;
        desPos = endPos;
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, desPos.position, Time.deltaTime * speed);

        if (Vector2.Distance(transform.position, desPos.position) <= 0.05f)
        {
            if (desPos == endPos)
            {
                desPos = startPos;
            }
            else
            {
                desPos = endPos;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            DontDestroyOnLoad(collision.gameObject);
        }
    }
}
