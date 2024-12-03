using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class PlayerData
{
    public string name;
    public string datetime;
    public int hp = 100;
    public int stage = 1;
    public int money = 100;

    public int tomatoSeed=0;
    public int carrotSeed=0;
    public int pumpkinSeed=0;
    public int wheatSeed = 0;
    public int beetSeed = 0;

    public int tomato = 0;
    public int pumpkin = 0;
    public int wheat = 0;
    public int beet = 0;
    public int carrot = 0;

    public int milk = 0;
    public int egg = 0;
    public int wool = 0;

    public int feed = 0;

    public List<Vector3> objectPosition = new List<Vector3>(); 
    public List<string> objectName = new List<string>();
    public List<int> objectLevel = new List<int>();
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;
    public PlayerData playerData = new PlayerData();
    
    string path;

    public int nowSlot;

    void Awake()
    {
        path = Application.persistentDataPath + "/";
        print(path);

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    public void SaveData()
    {
        SavePosition();
        GameManager.Instance.sceneLoaded = true;

        string data = JsonUtility.ToJson(playerData);
        File.WriteAllText(path + nowSlot.ToString(), data );
    }

    public void SavePosition()
    {
        Collider[] colliders = Physics.OverlapSphere(new Vector3(0.3f, 0, -10) , 30f);

        foreach (Collider collider in colliders)
        {
            if(collider.gameObject.tag == "Object")
            {
                playerData.objectPosition.Add(collider.gameObject.transform.position);
                playerData.objectName.Add(collider.gameObject.GetComponent<ObjectInformation>().name);
                playerData.objectLevel.Add(collider.gameObject.GetComponent<ObjectInformation>().level);
            }
        }
       
    }

    public void LoadData()
    {
        string data =  File.ReadAllText(path + nowSlot);
        playerData = JsonUtility.FromJson<PlayerData>(data);
        playerData.tomatoSeed = 10;
        playerData.pumpkinSeed = 10;
    }

    public void DataClear()
    {
        nowSlot = -1;
        playerData = new PlayerData();
    }
}
