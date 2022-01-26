using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public Image skillImage;
    public Image skillDragImage;
    public Text skillNameText;
    public Text skillDescriptionText;
    private SkillManager skillManager;
    public SkillPanelManager skillPanelManager;
    public int skillLevel;
    public int levelNeeded;
    public int SPNeeded;
    private Text upgradeButtonText;
    public GameObject fadePanel;
    public bool isActivated = false;

    public int skillButtonId;//Each button has one unique id correspond with the same order as the Skill array

    private void Start()
    {
        skillManager = FindObjectOfType<SkillManager>();
        skillPanelManager = FindObjectOfType<SkillPanelManager>();
        upgradeButtonText = GameObject.FindWithTag("UpgradeButtonText").GetComponent<Text>(); 
    }

    public void PressSkillButton()
    {
        SkillManager.instance.activateSkill = transform.GetComponent<Skill>();

        skillImage.sprite = SkillManager.instance.skills[skillButtonId].skillSprite;
        skillDragImage.sprite = SkillManager.instance.skills[skillButtonId].skillSprite;
        skillNameText.text = SkillManager.instance.skills[skillButtonId].skillName;
        skillDescriptionText.text = SkillManager.instance.skills[skillButtonId].skillDes;
        skillManager.skillId = skillButtonId;
        GameObject.FindWithTag("SkillToDrag").GetComponent<PlayerSkillSlot>().skillID = skillButtonId; //присваиваем skillID скрипту PlayerSkillSlot от иконки справа

        //skillPanelManager.skills[0].skillImage.sprite = SkillManager.instance.skills[skillButtonId].skillSprite;
        //skillPanelManager.skills[0].skillImage.GetComponent<PressSkill>().skillId = skillButtonId;



        if (skillLevel == 0)
        {
            upgradeButtonText.text = "Learn";
        }
        else
        {
            upgradeButtonText.text = "Upgrade";
        }
    }



}
