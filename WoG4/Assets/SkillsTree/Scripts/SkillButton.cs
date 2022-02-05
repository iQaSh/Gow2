using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public PlayerSkillSO playerSkillSO;
    public Image skillImage;
    public Image skillImg;
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
        skillImg.sprite = playerSkillSO.Icon;
        skillButtonId = playerSkillSO.skillID;
    }

    public void PressSkillButton()
    {
        SkillManager.instance.activateSkill = transform.GetComponent<Skill>();

        skillImage.sprite = skillImg.sprite;
        skillDragImage.sprite = skillImg.sprite;
        skillNameText.text = playerSkillSO.skillName;
        skillDescriptionText.text = playerSkillSO.description;
        skillManager.skillId = skillButtonId;
        GameObject.FindWithTag("SkillToDrag").GetComponent<PlayerSkillSlot>().skillID = playerSkillSO.skillID; //присваиваем skillID скрипту PlayerSkillSlot от иконки справа

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
