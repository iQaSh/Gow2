using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class BlankSkill
{
    public Image skillImage;
    public int skillId;

}


public class SkillPanelManager : MonoBehaviour
{

    public BlankSkill[] skills;
    public PlayerSkillSO[] playerSkills;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
