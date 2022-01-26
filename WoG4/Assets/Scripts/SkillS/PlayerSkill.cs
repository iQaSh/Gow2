using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    public PlayerSkillsSO playerSkillsSO;
    public Text skillName;
    public Text SP;
    public Image skillImage;
    public GameObject fadePanel;
    private PlayerSkillSlot playerSkillSlot;


    // Start is called before the first frame update
    void Start()
    {
        playerSkillSlot = FindObjectOfType<PlayerSkillSlot>();
        skillName.text = playerSkillsSO.skillName;
        SP.text = $"{playerSkillsSO.SPNeeded} SP";
        skillImage.sprite = playerSkillsSO.Icon;

    }

    public void ActivateSkill()
    {
        fadePanel.SetActive(false);
        playerSkillSlot.GetComponent<Image>().sprite = playerSkillsSO.Icon;
        playerSkillSlot.skillID = playerSkillsSO.skillID;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
