using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : LivestockFarming
{
    void Update()
    {
        IconChange();
        ResourceProduct();
    }

    public override void GetProduct()
    {
        DataManager.instance.playerData.milk++;
        print(DataManager.instance.playerData.milk + " ¿ìÀ¯ È¹µæ");
    }
}
