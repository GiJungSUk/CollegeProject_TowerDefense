using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeAnimal : UpgradeObject
{
    private UIManager uiManager;
    private AnimalInformation animalInfo;
    private LivestockFarming live;

    private void Awake()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        animalInfo = GetComponent<AnimalInformation>();
        live = GetComponent<LivestockFarming>();
    }
    public override void Upgrade()
    {
        if (DataManager.instance.playerData.money >= animalInfo.upgradePrice)
        {
            DataManager.instance.playerData.money -= animalInfo.upgradePrice;
            ShopManager.instance.GoldTextUpdate();
            animalInfo.upgradePrice += 10;

            animalInfo.level += 1;
            live.productTime -= live.productTime * 0.1f;

            Instantiate(upgradeEffect, gameObject.transform.position, Quaternion.identity);
            print(gameObject);
            uiManager.InputInformation(gameObject);
            
            print("업그레이드");
        }
    }
}
