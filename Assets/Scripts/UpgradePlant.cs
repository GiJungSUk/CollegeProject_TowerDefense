using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePlant : UpgradeObject
{
    private Farming farming;
    private PlantsInformation plantInfo;
    private UIManager uiManager;
    void Awake()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        farming = GetComponent<Farming>();
        plantInfo = GetComponent<PlantsInformation>();
    }

    public override void Upgrade()
    {
        if (DataManager.instance.playerData.money >= plantInfo.upgradePrice)
        {
            DataManager.instance.playerData.money -= plantInfo.upgradePrice;
            ShopManager.instance.GoldTextUpdate();
            plantInfo.upgradePrice += 10;
            
            plantInfo.level += 1;
            farming.harvestCount += 1;
            
            Instantiate(upgradeEffect, gameObject.transform.position, Quaternion.identity);
            uiManager.InputInformation(gameObject);
            print("업그레이드");
        }
    }
}
