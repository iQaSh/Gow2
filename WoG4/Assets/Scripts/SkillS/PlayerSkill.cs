using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    public PlayerSkillSO playerSkillSO;
    public Text skillName;
    public Text SP;
    public Image skillImage;
    public GameObject fadePanel;
    private PlayerSkillSlot playerSkillSlot;


    // Start is called before the first frame update
    void Start()
    {
        playerSkillSlot = FindObjectOfType<PlayerSkillSlot>();
        skillName.text = playerSkillSO.skillName;
        SP.text = $"{playerSkillSO.SPNeeded} SP";
        skillImage.sprite = playerSkillSO.Icon;

    }

    public void ActivateSkill()
    {
        fadePanel.SetActive(false);
        playerSkillSlot.GetComponent<Image>().sprite = playerSkillSO.Icon;
        playerSkillSlot.skillID = playerSkillSO.skillID;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
