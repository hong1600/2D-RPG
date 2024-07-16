using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class Portal1 : MonoBehaviour
{
    CapsuleCollider2D cap;
    GameObject player;
    Rigidbody2D playerrigid;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerrigid = player.GetComponent<Rigidbody2D>();
        cap = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerrigid.simulated = false;
            SceneLoad.LoadScene(2);
        }
    }
}
