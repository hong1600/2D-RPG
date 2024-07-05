using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    SkillData skilldata;

    [SerializeField] GameObject tooltip;
    [SerializeField] GameObject skill1;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI aktText;
    [SerializeField] TextMeshProUGUI aktValueText;

    public void setupToolTip()
    {
        nameText.text = skilldata.skillName;
        descriptionText.text = skilldata.skillDescription;
        aktValueText.text = skilldata.skillAtk.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
            tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
    }

}
