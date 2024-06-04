using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowCam : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    Transform player;

    private void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        player = null;
    }

    void Update()
    {
        if (player == null)
        {
        }
    }
}
