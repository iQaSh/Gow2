using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsManager : MonoBehaviour
{
    //public String playerName;
    public int playerMaxHP = 100;
    public Text playerHPText;
    public int playerLvl;
    public int playerXP;
    public int playerSP;
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





    public delegate void MPRecieveEvent();
    public static event MPRecieveEvent recieveMPEvent;

    public void SetImagesToSkill(int ID, int slot)
    {
        //for (int i = 0; i < skillSlot.Length; i++)
        //{
        //    if (ID < 999)
        //    {
        //        Debug.Log("skill id = " + ID);
        //        skillSlot[i].GetComponent<Image>().sprite = skillPanelManager.playerSkills[ID].Icon;
        //    }
        //}
        skillSlot[slot].GetComponent<Image>().sprite = skillPanelManager.playerSkills[ID].Icon;

    }


    private void Start()
    {
        playerHPText.text = $"{playerMaxHP}/{playerMaxHP}";
        skillPanelManager = FindObjectOfType<SkillPanelManager>();

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
