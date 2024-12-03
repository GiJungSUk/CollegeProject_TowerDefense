using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Farming : MonoBehaviour
{

    public class seedData
    {
        public string name;
        public float growingTime;
        public GameObject middle_Plants;
        public GameObject plants;
        public int seed;
        
        public seedData(string name , float growingTime, GameObject middle_Plants, GameObject plants , ref int seed)
        {
            this.name = name;
            this.growingTime = growingTime;
            this.middle_Plants = middle_Plants;
            this.plants = plants;
            this.seed = seed;
        }
        
         public bool SeedCanSowed()
        {
            switch (name)
            {
                case "tomato": return SeedCount(ref DataManager.instance.playerData.tomatoSeed);
                case "wheat": return SeedCount(ref DataManager.instance.playerData.wheatSeed);
                case "beat": return SeedCount(ref DataManager.instance.playerData.beetSeed);
                case "carrot": return SeedCount(ref DataManager.instance.playerData.carrotSeed);
                case "pumpkin": return SeedCount(ref DataManager.instance.playerData.pumpkinSeed);
                default : return false;
            }
        }

        private bool SeedCount(ref int seed)
        {
            if (seed > 0)
            {
                seed--;
                return true;
            }
            else
                return false;  
        }
    }

    public enum cropsState
    {
        None,
        Seed,
        Middle,
        Perfact
    }

    [SerializeField]
    private GameObject[] middle_Plants;
    [SerializeField]
    private GameObject[] plants;
    [SerializeField]
    public float[] growingTime;
    [SerializeField]
    private GameObject seed;
    [SerializeField]
    private GameObject wetEffact;
    [SerializeField]
    private GameObject sowEffact;
    [SerializeField]
    private GameObject harvestEffact;

    public GameObject waterIcon;
    public GameObject sowIcon;
    public GameObject harvestIcon;

    private float curruntTime = 0f;
    private float recentGrowingTime = 0f;

    PlantsInformation plantsInformation;
    GameManager gameManager;
    UIManager uiManager;

    private Dictionary<GameManager.SeedState, seedData> seedDictionary;
    private seedData recentSeedData;
    cropsState state;

    public int harvestCount = 1;
    private void Awake()
    {
        plantsInformation = GetComponent<PlantsInformation>();
        state = cropsState.None;
        gameManager = GameManager.Instance;
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        seedDictionary = new Dictionary<GameManager.SeedState, seedData>
        {

            { GameManager.SeedState.tomato, new seedData("tomato" , growingTime[0] ,middle_Plants[0], plants[0], ref DataManager.instance.playerData.tomatoSeed) },
            { GameManager.SeedState.wheat, new seedData("wheat" ,growingTime[1] ,middle_Plants[1], plants[1], ref DataManager.instance.playerData.wheatSeed ) },
            { GameManager.SeedState.beat, new seedData("beat" , growingTime[2] ,middle_Plants[2], plants[2], ref DataManager.instance.playerData.beetSeed)  },
            { GameManager.SeedState.carrot, new seedData("carrot", growingTime[3] ,middle_Plants[3], plants[3], ref DataManager.instance.playerData.carrotSeed)},
            { GameManager.SeedState.pumpkin, new seedData("pumpkin" ,growingTime[4] ,middle_Plants[4], plants[4], ref DataManager.instance.playerData.pumpkinSeed)}
        };
    }
    void Update()
    {
        IconChange();
        Growing();
    }

    private void IconChange()
    {
        if (!plantsInformation.sowed)
        {
            sowIcon.SetActive(true);
            return;
        }
        else if (!plantsInformation.watered)
        {
            waterIcon.SetActive(true);
            return;
        }
        else if(state == cropsState.Perfact)
        {
            harvestIcon.SetActive(true);
            return;
        }
    }
    private void Growing()
    {
        if (plantsInformation.sowed && plantsInformation.watered// 씨앗이 심어져 있고 물을 줬다면
            && recentSeedData != null)
        {
            curruntTime += Time.deltaTime;
            plantsInformation.time = recentGrowingTime - curruntTime;

            if (recentGrowingTime / 2 < curruntTime && state == cropsState.Seed)
            {
                state = cropsState.Middle;
                allCropsDestroy();
                Instantiate(recentSeedData.middle_Plants, gameObject.transform);
            }

            if (recentGrowingTime < curruntTime && state == cropsState.Middle)
            {
                state = cropsState.Perfact;
                allCropsDestroy();
                Instantiate(recentSeedData.plants, gameObject.transform);
                recentGrowingTime = 0f;
            }
        }
    }
    public void Harvest()
    {
        switch (plantsInformation.seedName)
        {
            case "tomato": DataManager.instance.playerData.tomato += harvestCount; break;
            case "wheat": DataManager.instance.playerData.wheat += harvestCount; break;
            case "beat": DataManager.instance.playerData.beet += harvestCount; ; break;
            case "carrot": DataManager.instance.playerData.carrot += harvestCount; ; break;
            case "pumpkin": DataManager.instance.playerData.pumpkin += harvestCount; ; break;
            default: break;
        }

        print(DataManager.instance.playerData.tomato);

        harvestIcon.SetActive(false);
        state = cropsState.None;
        plantsInformation.sowed = false;
        plantsInformation.watered = false;
        plantsInformation.seedName = "";
        curruntTime = 0f;
        plantsInformation.time = 0f;
        recentSeedData = null;
        allCropsDestroy();
        Instantiate(harvestEffact, gameObject.transform.position, Quaternion.identity);

        uiManager.InputInformation(gameObject); // UI 업데이트
    }
    public void Sow()
    {
        if (plantsInformation.sowed == false && state == cropsState.None // 씨앗이 심어져 있지 않으면
            && seedDictionary.TryGetValue(gameManager.seedState, out seedData seedData) // 씨앗의 정보를 가져옴
            && seedData.SeedCanSowed()) // 씨앗이 있는지 확인하고 감소시킴
        {
            recentSeedData = seedData;
            plantsInformation.sowed = true; // 심어져있는 상태로 만들고
            plantsInformation.seedName = seedData.name; // 심은 작물 이름 설정
            recentGrowingTime = seedData.growingTime; // 성장 시간 설정
            Instantiate(seed, gameObject.transform); // 기본 씨앗 오브젝트를 생성
            sowIcon.SetActive(false);
            state = cropsState.Seed;
            Instantiate(sowEffact, gameObject.transform.position, Quaternion.identity);
        }

        uiManager.InputInformation(gameObject); // UI 업데이트
    }
    public void Wartering()
    {
        Instantiate(wetEffact , gameObject.transform.position , Quaternion.identity);
        plantsInformation.watered = true;
        waterIcon.SetActive(false);
        uiManager.InputInformation(gameObject);
    }

    private void allCropsDestroy()
    {
        foreach (Transform child in transform)
        {
            if (child.name != "Canvas")
                GameObject.Destroy(child.gameObject);
        }
    }
    
}
