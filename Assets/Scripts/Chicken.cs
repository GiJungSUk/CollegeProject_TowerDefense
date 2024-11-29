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
        base.GetProduct();
        // 계란을 얻음 코드
    }
}
