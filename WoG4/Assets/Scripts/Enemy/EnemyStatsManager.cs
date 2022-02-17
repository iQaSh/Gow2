using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatsManager : MonoBehaviour
{
    [Header("Main Stats")]
    public int enemyMaxHP;
    public Text enemyHPText;
    public int enemyLvl;
    public Image icon;


    [Header("MP Stats")]
    public int maxRedMP;
    public int maxGreenMP;
    public int maxYellowMP;
    public int maxBlueMP;
    public int maxBrownMP;

    [Header("SkillSlots")]
    public Image firstSkillSlot;
    public Image secondSkillSlot;
    public Image thirdSkillSlot;
    public Image fourthSkillSlot;
    public Image fifthSkillSlot;

    [Header("Not Editable Stats")]
    public Text redMPText;
    public Text greenMPText;
    public Text yellowMPText;
    public Text blueMPText;
    public Text brownMPText;
    public int redMP;
    public int greenMP;
    public int yellowMP;
    public int blueMP;
    public int brownMP;

    [Header("Other")]
    public EnemySO enemySO;
    public GameObject enemySkillPrefab;
    public GameObject skillsContainer;

    private void Start()
    {
        SetStats();
    }

    private void SetStats()//Установка данных из СО
    {
        maxRedMP = enemySO.maxRedMP;
        maxGreenMP = enemySO.maxGreenMP;
        maxYellowMP = enemySO.maxYellowMP;
        maxBlueMP = enemySO.maxBlueMP;
        maxBrownMP = enemySO.maxBrownMP;

        redMPText.text = $"0/{maxRedMP}";
        greenMPText.text = $"0/{maxGreenMP}";
        yellowMPText.text = $"0/{maxYellowMP}";
        blueMPText.text = $"0/{maxBlueMP}";
        brownMPText.text = $"0/{maxBrownMP}";

        enemyMaxHP = enemySO.HP;
        enemyHPText.text = $"{enemyMaxHP}/{enemyMaxHP}";

        icon.sprite = enemySO.icon;

        SetSkills();

    }

    void SetSkills()
    {
        for (int i = 0; i < enemySO.enemySkillSO.Length; i++)
        {
            GameObject skill = Instantiate(enemySkillPrefab, skillsContainer.transform.position, Quaternion.identity);
            
            skill.transform.SetParent(skillsContainer.transform);
            skill.transform.localScale = new Vector3(1, 1, 1);
            skill.GetComponent<Image>().sprite = enemySO.enemySkillSO[i].icon;


  
            skill.GetComponent<EnemySkillInfo>().name = enemySO.enemySkillSO[i].skillName;
            skill.GetComponent<EnemySkillInfo>().description = enemySO.enemySkillSO[i].description;
            skill.GetComponent<EnemySkillInfo>().id = enemySO.enemySkillSO[i].skillID;
        }
    }

}
