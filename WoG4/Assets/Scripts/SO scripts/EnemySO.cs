using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public string name;
    public Sprite icon;
    public int lvl;
    public int HP;
    public int maxRedMP;
    public int maxBlueMP;
    public int maxYellowMP;
    public int maxGreenMP;
    public int maxBrownMP;
    public EnemySkillSO[] enemySkillSO;

    [Header("Skills")]
    public int firstSkillId;
    public int secondSkillId;
    public int thirdSkillId;
    public int fourthSkillId;
    public int fifthSkillId;
}
