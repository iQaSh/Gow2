using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsManager : MonoBehaviour
{

    public int PlayerMaxHP;
    public Text playerHPText;
    public int PlayerLvl;
    public int PlayerXP;
    public int PlayerSP;
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



    public delegate void MPRecieveEvent();
    public static event MPRecieveEvent recieveMPEvent;


    private void Start()
    {
        playerHPText.text = $"{PlayerMaxHP}/{PlayerMaxHP}";

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
