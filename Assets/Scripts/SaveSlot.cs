using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSlot : MonoBehaviour
{
    public TextMeshProUGUI[] nameText;
    public TextMeshProUGUI[] timeText;

    public GameObject namePopup;
    public TMP_InputField inputField;

    private bool[] saved;
    void Start()
    {
        saved = new bool[3];

        for(int i = 0; i < 3; i++)
        {
            if (File.Exists(Application.persistentDataPath + "/" + i.ToString()))
            {
                saved[i] = true;
                InsertInfoSlot(i);
            }
            else
            {
                nameText[i].text = "NONE";
                saved[i] = false;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) && inputField.isFocused)
        {
            InsertInfo();
        }
    }

    public void InsertInfo()
    {
        if (namePopup.activeSelf)
        {
            if (!string.IsNullOrEmpty(inputField.text))
            {
                DateTime currentTime = DateTime.Now;
                DataManager.instance.playerData.name = inputField.text;
                string formattedTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss");
                DataManager.instance.playerData.datetime = formattedTime;

                DataManager.instance.SaveData();
                InsertInfoSlot(DataManager.instance.nowSlot);

                DataManager.instance.DataClear();
                namePopup.SetActive(false);
            }
        }
    }

    public void CheckSlot()
    {
        if (saved[DataManager.instance.nowSlot])
        {
            DataManager.instance.LoadData();
            SceneManager.LoadScene("Main");
        }
        else
        {
            namePopup.SetActive(true);
        }
    }

    private void InsertInfoSlot(int i)
    {
        DataManager.instance.nowSlot = i;
        DataManager.instance.LoadData();

        nameText[i].text = DataManager.instance.playerData.name;
        timeText[i].text = "Stage " + DataManager.instance.playerData.stage.ToString() + "\n" + DataManager.instance.playerData.datetime;

        DataManager.instance.DataClear();

        saved[i] = true;
    }


}
