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
        base.GetProduct();
        //  우유를 얻는 코드
    }
}
