using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopProductInfo : MonoBehaviour
{
    public Sprite newSprite;
    public string name;
    public int price;


    public void GiveInfo()
    {
        ShopManager.instance.currentimage = newSprite;
        ShopManager.instance.currentName = name;
        ShopManager.instance.currentPrice = price;
    }
}
