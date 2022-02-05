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
        saveData.maxRedMP = playerStatsManager.redMP;
        saveData.maxGreenMP = playerStatsManager.greenMP;
        saveData.maxYellowMP = playerStatsManager.yellowMP;
        saveData.maxBlueMP = playerStatsManager.blueMP;
        saveData.maxBrownMP = playerStatsManager.brownMP;
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



            }

        }
    }

}
