using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    Button savaBtn;

    void Awake()
    {
        savaBtn = GetComponent<Button>();

        savaBtn.onClick.AddListener(() => DataManager.instance.SaveData());
    }

   
}
