using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject bottomPanel;
    public TextMeshProUGUI upgradePrice;
    public TextMeshProUGUI recellPrice;
    public Image photo;
    public TextMeshProUGUI name;
    public TextMeshProUGUI level;

    GameObject recentObject;
    
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            FindObject();
        }
    }

    private void FindObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray , out hit))
        {
            if(hit.collider != null && hit.collider.gameObject.tag == "Object")
            {
                bottomPanel.gameObject.SetActive(true);

                ObjectInformation info = hit.collider.GetComponent<ObjectInformation>(); // 대상 정보 가져오기
                recentObject = hit.collider.gameObject;

                name.text = info.name;
                level.text = info.level.ToString();
                upgradePrice.text = info.upgradePrice.ToString();
                recellPrice.text = (info.price / 2).ToString();
                recentObject = info.gameObject;
            }
        }
    }

    public void UpgradeBtnEvent()
    {
        recentObject.GetComponent<UpgradeObject>().Upgrade();
    }

}
