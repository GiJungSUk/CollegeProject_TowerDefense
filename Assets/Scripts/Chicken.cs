using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : LivestockFarming
{


    // Update is called once per frame
    void Update()
    {
        IconChange();
        ResourceProduct();
    }

    public override void GetProduct()
    {
        DataManager.instance.playerData.egg++;
        print(DataManager.instance.playerData.egg + " °è¶õ È¹µæ");
    }
}
