using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInformation : MonoBehaviour
{
    public enum Type
    {
        Tower,
        Animal,
        Building_B,
        Building_S,
        Plant
    }

    public int price;
    public int level;
    public int upgradePrice;
    public string name;
    public Sprite objectPic;
    public Type type;
    [HideInInspector]
    public float time = 0f;

}
