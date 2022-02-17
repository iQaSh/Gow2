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
    private SkillPanelManager skillPanelManager;



    public int skillSlotMP;
    private string skillColor;

    private void Start()
    {
        playerStatsManager = FindObjectOfType<PlayerStatsManager>();
        wizardSkills = FindObjectOfType<WizardSkills>();
        skillPanelManager = FindObjectOfType<SkillPanelManager>();
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
            GetComponent<Image>().sprite = skillPanelManager.playerSkills[skillID].Icon;
            //playerStatsManager.SetImagesToSkill(skillID);
            MPText.SetActive(true);

            SetSkillMP();
            //var result = wizardSkills.getSkillMP(skillID);//кортеж Tuple с цветом и количеством MP
            //skillSlotMP = result.MP;

            //skillColor = result.color;
            //Debug.Log("skillColor = " + skillColor);
            //if (skillColor == "red")
            //{
            //    MPText.GetComponent<Text>().color = Color.red;
            //}

            //if (skillColor == "blue")
            //    MPText.GetComponent<Text>().color = Color.blue;
            //if (skillColor == "yellow")
            //    MPText.GetComponent<Text>().color = Color.yellow;
            //if (skillColor == "green")
            //    MPText.GetComponent<Text>().color = Color.green;
            //if (skillColor == "brown")
            //    MPText.GetComponent<Text>().color = Color.black;
            //MPText.GetComponent<Text>().text = $"{skillSlotMP}";

        }
    }

    public void SetSkillMP()
    {
        
        var result = wizardSkills.getSkillMP(skillID);//кортеж Tuple с цветом и количеством MP
        skillSlotMP = result.MP;

        skillColor = result.color;
        Debug.Log("skillColor = " + skillColor);
        if (skillColor == "red")
        {
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

        MPText.SetActive(true);
        Debug.Log("-------------------------SetSkillMP " + skillSlotMP);
        MPText.GetComponent<Text>().text = $"{skillSlotMP}";
    }




    public void UseSkill()
    {
        Debug.Log("SkillId = " + skillID);
    }
}

