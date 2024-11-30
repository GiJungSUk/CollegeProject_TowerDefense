using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject bottomPanel;
    public TextMeshProUGUI upgradePrice;
    public TextMeshProUGUI recellPrice;
    public Image photo;
    public TextMeshProUGUI names;
    public TextMeshProUGUI level;

    public GameObject animalPanel;
    public TextMeshProUGUI remainAnimalTimeText;
    public TextMeshProUGUI feedText;

    public GameObject towerPanel;
    public TextMeshProUGUI AttackDamageText;
    public TextMeshProUGUI AttackSpeedText;
    public TextMeshProUGUI AttackRangeText;

    public GameObject plantPanel;
    public TextMeshProUGUI seedNmaeText;
    public TextMeshProUGUI remainCropsTimeText;


    public TextMeshProUGUI topGoldText;
    public TextMeshProUGUI topStageText;
    public TextMeshProUGUI topNameText;


    GameObject recentObject;

    private void Awake()
    {
        topGoldText.text = DataManager.instance.playerData.money.ToString();
        topNameText.text = DataManager.instance.playerData.name;
        topStageText.text = DataManager.instance.playerData.stage.ToString();

    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            FindObject();
        }
    }
    public void InputInformation(GameObject gameObject)
    {
        ObjectInformation info = gameObject.GetComponent<ObjectInformation>(); // 대상 정보 가져오기

        names.text = info.name;
        level.text = info.level.ToString();
        upgradePrice.text = info.upgradePrice.ToString();
        recellPrice.text = (info.price / 2).ToString();
        checkObjectInfo(info.type, gameObject);
        recentObject = info.gameObject;
    }

    public void UpgradeBtnEvent()
    {
        recentObject.GetComponent<UpgradeObject>().Upgrade();

    }

    private void FindObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, (1 << 5)!))
        {
            if (hit.collider != null && hit.collider.gameObject.tag == "Object")
            {
                bottomPanel.gameObject.SetActive(true);
                InputInformation(hit.collider.gameObject);
            }
        }
    }
    private void checkObjectInfo(ObjectInformation.Type type, GameObject gameObject)
    {
        allPanelSet(false);

        switch (type)
        {
            case ObjectInformation.Type.Animal:
                animalPanel.SetActive(true);
                AnimalInformation aniInfo = gameObject.GetComponent<AnimalInformation>();
                if (aniInfo.feed) feedText.text = "O"; else feedText.text = "X";
                StopAllCoroutines();
                StartCoroutine(UpdateRemainTime(remainAnimalTimeText, aniInfo));

                break;
            case ObjectInformation.Type.Tower:
                towerPanel.SetActive(true);
                TowerInformation towerInfo = gameObject.GetComponent<TowerInformation>();
                AttackDamageText.text = towerInfo.attackDamage.ToString();
                AttackSpeedText.text = towerInfo.attackTime.ToString();
                AttackRangeText.text = towerInfo.attackRange.ToString();

                break;
            case ObjectInformation.Type.Plant:
                plantPanel.SetActive(true);
                PlantsInformation plantInfo = gameObject.GetComponent<PlantsInformation>();
                seedNmaeText.text = plantInfo.seedName;
                StopAllCoroutines();
                StartCoroutine(UpdateRemainTime(remainCropsTimeText, plantInfo));
                break;
            case ObjectInformation.Type.Building: break;
        }
    }

    private IEnumerator UpdateRemainTime(TextMeshProUGUI text, ObjectInformation obj)
    {
        if (obj.time < 0)
            text.text = "0";

        while (obj.time >= 0)
        {
            text.text = obj.time.ToString("0");
            yield return new WaitForSeconds(1f); // 1초 간격으로 업데이트
        }
    }

    private void allPanelSet(bool flag)
    {
        animalPanel.SetActive(flag);
        towerPanel.SetActive(flag);
        plantPanel.SetActive(flag);
    }



}