using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressSkill : MonoBehaviour
{
    public int skillId;

    public void UseSkill()
    {
        Debug.Log("SkillId = " + this.skillId);
    }
}


