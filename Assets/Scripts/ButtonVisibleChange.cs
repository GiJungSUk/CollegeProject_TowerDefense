using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonVisibleChange : MonoBehaviour
{
    public GameObject uiObject;


    public void SetActiveChange()
    {
        if (uiObject.activeSelf)
        {
            uiObject.SetActive(false);
        }
        else
        {
            uiObject.SetActive(true);
        }
    }
}
