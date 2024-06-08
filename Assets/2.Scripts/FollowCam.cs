using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowCam : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    GameObject player;
    Transform playerTrs;

    private void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        player = null;
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");

            if(player != null) 
            {
                playerTrs = player.transform;
                cam.Follow = playerTrs;
                cam.LookAt = playerTrs;
            }
        }
    }
}
