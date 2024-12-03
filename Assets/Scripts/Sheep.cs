using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : LivestockFarming
{
    // Update is called once per frame
    void Update()
    {
        IconChange();
        ResourceProduct();
    }

    public override void GetProduct()
    {
        DataManager.instance.playerData.wool++;
        print(DataManager.instance.playerData.wool + " ¾ç¸ð È¹µæ");
    }
}
