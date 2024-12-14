using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePlant : UpgradeObject
{
    private Farming farming;
    private PlantsInformation plantInfo;
    private UIManager uiManager;
    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        farming = GetComponent<Farming>();
        plantInfo = GetComponent<PlantsInformation>();
        farming.harvestCount = plantInfo.level;
    }

    public override void Upgrade()
    {
        if (DataManager.instance.playerData.money >= plantInfo.upgradePrice)
        {
            DataManager.instance.playerData.money -= plantInfo.upgradePrice;
            ShopManager.instance.GoldTextUpdate();
            plantInfo.upgradePrice += 10;
            
            plantInfo.level += 1;
            farming.harvestCount = plantInfo.level;
            
            Instantiate(upgradeEffect, gameObject.transform.position, Quaternion.identity);
            uiManager.InputInformation(gameObject);
            print("업그레이드");
        }
    }
}
