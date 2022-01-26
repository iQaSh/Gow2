using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnSkills : MonoBehaviour
{
    public SkillButton[] skillButton;
    private PlayerSkillSlot playerSkillSlot;

    private void Start()
    {
        playerSkillSlot = FindObjectOfType<PlayerSkillSlot>();

    }

    public void LearnSkill()
    {
        Debug.Log(skillButton[playerSkillSlot.skillID].skillNameText.text);
        skillButton[playerSkillSlot.skillID].isActivated = true;
        skillButton[playerSkillSlot.skillID].fadePanel.SetActive(false);
    }
}
