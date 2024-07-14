using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cut1 : MonoBehaviour
{
    BoxCollider2D box;
    [SerializeField] GameObject cam; 

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(scene());
        }
    }

    IEnumerator scene()
    {
        cam.SetActive(true);

        yield return new WaitForSeconds(3f);

        cam.SetActive(false);
        gameObject.SetActive(false);
    }

}
