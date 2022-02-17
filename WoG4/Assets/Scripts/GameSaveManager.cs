using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{

    public SaveData saveData;
    public List<ScriptableObject> objects = new List<ScriptableObject>();
    private PlayerStatsManager playerStatsManager;
    private SkillSlot skillSlot;

    private void Start()
    {
  //      saveData = FindObjectOfType<SaveData>();
        playerStatsManager = FindObjectOfType<PlayerStatsManager>();
        skillSlot = FindObjectOfType<SkillSlot>();

    }

    public void SaveScriptables()
    {
        SetSaveData();
        for (int i = 0; i < objects.Count; i++)
        {

            FileStream file = File.Create(Application.persistentDataPath +
                string.Format($"/{i}.dat"));
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(objects[i]);
            binary.Serialize(file, json);
            file.Close();
        }
    }

    public void LoadScriptables()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath +
                string.Format("/{0}.dat", i)))
            {
                FileStream file = File.Open(Application.persistentDataPath +
                    string.Format("/{0}.dat", i), FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file),
                    objects[i]);
                file.Close();
            }
        }
        SetSkillSlots();
        SetPlayerStats();


    }

    private void SetPlayerStats() // загрузка статов игрока
    {
        playerStatsManager.playerMaxHP = saveData.playerMaxHp;
        playerStatsManager.playerLvl = saveData.playerLevel;
        playerStatsManager.maxRedMP = saveData.maxRedMP;
        playerStatsManager.maxGreenMP = saveData.maxGreenMP;
        playerStatsManager.maxYellowMP = saveData.maxYellowMP;
        playerStatsManager.maxBlueMP = saveData.maxBlueMP;
        playerStatsManager.maxBrownMP = saveData.maxBrownMP;
    }

    public void SetSaveData()
    {

        //    saveData.playerName = playerStatsManager.playerName;
        saveData.playerMaxHp = playerStatsManager.playerMaxHP;
        saveData.playerSP = playerStatsManager.playerSP;
        saveData.playerXP = playerStatsManager.playerXP;
        Debug.Log("skillSlot.Length " + playerStatsManager.skillSlot.Length);
        for (int i = 0; i <= playerStatsManager.skillSlot.Length - 1; i++)
        {
 
            if (playerStatsManager.skillSlot[i].GetComponent<SkillSlot>().skillID != 999)
            {

                saveData.slotSkillId[i] = playerStatsManager.skillSlot[i].GetComponent<SkillSlot>().skillID;
                Debug.Log($"saving....  slot = {i} skillid = {saveData.slotSkillId[i]}");
            }else if (playerStatsManager.skillSlot[i].GetComponent<SkillSlot>().skillID >= 999)
            {
                saveData.slotSkillId[i] = 999;
                Debug.Log($"saving....  slot = {i} skillid = 999");
            }

        }
        saveData.playerLevel = playerStatsManager.playerLvl;
        saveData.maxRedMP = playerStatsManager.maxRedMP;
        saveData.maxGreenMP = playerStatsManager.maxGreenMP;
        saveData.maxYellowMP = playerStatsManager.maxYellowMP;
        saveData.maxBlueMP = playerStatsManager.maxBlueMP;
        saveData.maxBrownMP = playerStatsManager.maxBrownMP;
    }


    private void SetSkillSlots()
    {
        for (int i = 0; i <= playerStatsManager.skillSlot.Length - 1; i++)
        {
            if (saveData.slotSkillId[i] < 999)
            {
                Debug.Log($"slot = {i} skillid = {saveData.slotSkillId[i]}");
                playerStatsManager.skillSlot[i].GetComponent<SkillSlot>().skillID = saveData.slotSkillId[i];
                playerStatsManager.SetImagesToSkill(saveData.slotSkillId[i], i);

                playerStatsManager.skillSlot[i].GetComponent<SkillSlot>().SetSkillMP();

            }

        }
    }

}
