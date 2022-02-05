using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Player/Skills")]
public class PlayerSkillSO : ScriptableObject
{
    public int skillID;
    public string skillName;
    public string description;
    public Sprite Icon;
    public int levelNeeded;
    public int SPNeeded;
    public int skillLevel;
    public bool isActive;
    public bool isActivated;
    


}
