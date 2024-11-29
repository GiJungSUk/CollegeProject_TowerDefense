using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOnEble : MonoBehaviour
{
    private void OnEnable()
    {
        ShopManager.instance.UpdateProduct();
    }
}
