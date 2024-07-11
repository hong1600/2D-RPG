using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BExplosion : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 0.4f);
    }
}
