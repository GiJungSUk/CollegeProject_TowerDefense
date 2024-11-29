using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class ChangeNowSlot : MonoBehaviour
{
    [SerializeField]
    private int saveNumber;
    public void ChangeNowSlot_()
    {
        DataManager.instance.nowSlot = saveNumber;
    }
}
