using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy/Skills")]
public class EnemySkillSO : ScriptableObject
{
    public int skillID;
    public int skillLvl;
    public string skillName;
    public string description;
    public Sprite icon;
}
