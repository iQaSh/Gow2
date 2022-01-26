using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject skillsMenu;

    public void SkillsMenu()
    {
        //GameObject skillsMenu = GameObject.FindWithTag("SkillsPanel");
        //skillsMenu.SetActive(true);
        
        if (skillsMenu.activeSelf)
        {
            skillsMenu.SetActive(false);
        }
        else
        {
            skillsMenu.SetActive(true);
        }
    }
}
