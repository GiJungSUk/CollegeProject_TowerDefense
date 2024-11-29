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
        base.GetProduct();
        // 양모를 얻는 코드
    }
}
