using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour, IDropHandler
{
    public int skillID;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("********************************** OnDrop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            Debug.Log("skillID = " + eventData.pointerDrag.GetComponent<PlayerSkillSlot>().skillID);
            skillID = eventData.pointerDrag.GetComponent<PlayerSkillSlot>().skillID;
            GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;

        }
    }
}

