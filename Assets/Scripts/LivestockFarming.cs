using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivestockFarming : MonoBehaviour
{
    AnimalInformation aniInfo;

    public GameObject feedIcon;
    public GameObject harvsetIcon;

    public float productTime;

    private float currutStarveTime = 0f;
    private float currutHarvestTime = 0f;
    UIManager uiManager;

    [SerializeField]
    private GameObject feedEffact;
    [SerializeField]
    private GameObject harvestEffact;

    private void Awake()
    {
        aniInfo = GetComponent<AnimalInformation>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    
    public void IconChange()
    {
        if(aniInfo.canHarvest)
        {
            harvsetIcon.SetActive(true);
            return;
        }
        else if (!aniInfo.feed)
        {
            feedIcon.SetActive(true);
            return;
        }
    }

    public void Harvest()
    {
        GetProduct();
        harvsetIcon.SetActive(false);
        currutHarvestTime = 0f;
        aniInfo.canHarvest = false;
        aniInfo.time = 0f;
        Instantiate(harvestEffact, gameObject.transform.position , Quaternion.identity);
        uiManager.InputInformation(gameObject);
    }

    public void ResourceProduct()
    {

        if (aniInfo.feed)
        {
            currutHarvestTime += Time.deltaTime;
            currutStarveTime += Time.deltaTime;
            aniInfo.time = productTime - currutHarvestTime;

            if (productTime < currutHarvestTime)
            {
                aniInfo.canHarvest = true;
            }

            if(aniInfo.starveTime < currutStarveTime)
            {
                aniInfo.feed = false;
            }
        }
    }

    public virtual void GetProduct()
    {
        
    }

    public void FeedAnimal()
    {
        if (aniInfo != null && DataManager.instance.playerData.feed > 0)
        {
            aniInfo.feed = true;
            feedIcon.SetActive(false);
            currutStarveTime = 0f;
            // 동물 먹이 감소 코드
            DataManager.instance.playerData.feed--;
            uiManager.InputInformation(gameObject);
            Instantiate(feedEffact , gameObject.transform.position , Quaternion.identity);
        }
    }
}
