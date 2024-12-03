using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTower : UpgradeObject
{

    TowerInformation towerInfo;
    private UIManager uiManager;
    TowerAttack towerAttack;
    private void Awake()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        towerInfo = GetComponent<TowerInformation>();
        towerAttack = GetComponent<TowerAttack>();
    }

    private void Start()
    {
            towerInfo.attackDamage += 4 * (towerInfo.level -1);
            towerInfo.attackRange += 0.6f * (towerInfo.level - 1);
            towerInfo.attackTime -= 0.02f * (towerInfo.level - 1);
    }
    public override void Upgrade()
    {
        if(DataManager.instance.playerData.money >= towerInfo.upgradePrice )
        {
            DataManager.instance.playerData.money -= towerInfo.upgradePrice;
            ShopManager.instance.GoldTextUpdate();

            towerInfo.attackDamage += 4;
            towerInfo.attackRange += 0.6f;
            towerInfo.attackTime -= 0.02f;
            towerInfo.level += 1;

            towerAttack.setBulletStats(towerInfo);
            Instantiate(upgradeEffect, gameObject.transform.position, Quaternion.identity);
            uiManager.InputInformation(gameObject);
            print("업그레이드");
        }
    }
}
