using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsManager : MonoBehaviour
{
    //public String playerName;
    [Header("Main Stats")]
    public int playerMaxHP;
    public Text playerHPText;
    public int playerLvl;
    public int playerXP;
    public int playerSP;


    [Header("MP Stats")]
    public int maxRedMP;
    public int maxGreenMP;
    public int maxYellowMP;
    public int maxBlueMP;
    public int maxBrownMP;

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
    public GameObject[] skillSlot;


    
    //   public SaveData saveData;
    private SkillPanelManager skillPanelManager;
    private GameSaveManager gameSaveManager;





    public delegate void MPRecieveEvent();
    public static event MPRecieveEvent recieveMPEvent;

    public void SetImagesToSkill(int ID, int slot)
    {

        skillSlot[slot].GetComponent<Image>().sprite = skillPanelManager.playerSkills[ID].Icon;

    }


    private void Start()
    {
        gameSaveManager = FindObjectOfType<GameSaveManager>();

        skillPanelManager = FindObjectOfType<SkillPanelManager>();

        LoadDataOnStart();
    }

    private void LoadDataOnStart()//загрузка сохранения и установка данных
    {
        gameSaveManager.LoadScriptables();
        playerHPText.text = $"{playerMaxHP}/{playerMaxHP}";
        blueMPText.text = $"0/{maxBlueMP}";
        redMPText.text = $"0/{maxRedMP}";
        greenMPText.text = $"0/{maxGreenMP}";
        yellowMPText.text = $"0/{maxYellowMP}";
        brownMPText.text = $"0/{maxBrownMP}";

    }

    public void RecieveMana(GameObject gem)
    {
      

        if (gem.tag == "BlueGem")
        {      
            blueMP++;
            blueMPText.text = $"{blueMP}/100";
            
        }
        if (gem.tag == "RedGem")
        {
            redMP++;
            redMPText.text = $"{redMP}/100";

        }
        if (gem.tag == "YellowGem")
        {
            yellowMP++;
            yellowMPText.text = $"{yellowMP}/100";

        }
        if (gem.tag == "GreenGem")
        {
            greenMP++;
            greenMPText.text = $"{greenMP}/100";

        }
        if (gem.tag == "BrownGem")
        {
            brownMP++;
            brownMPText.text = $"{brownMP}/100";

        }
        recieveMPEvent();
    }



}
