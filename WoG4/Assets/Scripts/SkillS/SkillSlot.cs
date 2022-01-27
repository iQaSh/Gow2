using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour, IDropHandler
{
    public int skillID;
    public GameObject MPText;
    private WizardSkills wizardSkills;
    private PlayerStatsManager playerStatsManager;
    public bool readyToUse = false;
    public GameObject fadePanel;



    public int skillSlotMP;
    private string skillColor;

    private void Start()
    {
        playerStatsManager = FindObjectOfType<PlayerStatsManager>();
        wizardSkills = FindObjectOfType<WizardSkills>();
    }

    private void OnEnable()
    {
        PlayerStatsManager.recieveMPEvent += MPListener;
    }

    void MPListener()
    {

        if (skillSlotMP > 2 && readyToUse == false)
        {
            if (skillColor == "red" && playerStatsManager.redMP >= skillSlotMP)
            {
                fadePanel.SetActive(false);
                readyToUse = true;
            }
            if (skillColor == "blue" && playerStatsManager.blueMP >= skillSlotMP)
            {
                fadePanel.SetActive(false);
                readyToUse = true;
            }
            if (skillColor == "yellow" && playerStatsManager.yellowMP >= skillSlotMP)
            {
                fadePanel.SetActive(false);
                readyToUse = true;
            }
            if (skillColor == "green" && playerStatsManager.greenMP >= skillSlotMP)
            {
                fadePanel.SetActive(false);
                readyToUse = true;
            }
            if (skillColor == "brown" && playerStatsManager.brownMP >= skillSlotMP)
            {
                fadePanel.SetActive(false);
                readyToUse = true;
            }
        }
            


    }

    public void OnDrop(PointerEventData eventData)
    
    {

        if (eventData.pointerDrag != null)
        {

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            skillID = eventData.pointerDrag.GetComponent<PlayerSkillSlot>().skillID;
            GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
            MPText.SetActive(true);


            var result = wizardSkills.getSkillMP(skillID);//кортеж Tuple с цветом и количеством MP
            skillSlotMP = result.MP;
           
            skillColor = result.color;
            Debug.Log("skillColor = " + skillColor);
            if (skillColor == "red")
            {
                Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++SetRed MP = " + skillSlotMP);
                MPText.GetComponent<Text>().color = Color.red;
            }
                
            if (skillColor == "blue")
                MPText.GetComponent<Text>().color = Color.blue;
            if (skillColor == "yellow")
                MPText.GetComponent<Text>().color = Color.yellow;
            if (skillColor == "green")
                MPText.GetComponent<Text>().color = Color.green;
            if (skillColor == "brown")
                MPText.GetComponent<Text>().color = Color.black;
            MPText.GetComponent<Text>().text = $"{skillSlotMP}";
            
        }
    }

    public void UseSkill()
    {
        Debug.Log("SkillId = " + skillID);
    }
}

