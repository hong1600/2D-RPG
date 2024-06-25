using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SKill1Btn : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI skill1Exp;

    private void OnMouseEnter()
    {
        skill1Exp.enabled = true;
    }

    private void OnMouseExit()
    {
        skill1Exp.enabled = false;
    }
}
