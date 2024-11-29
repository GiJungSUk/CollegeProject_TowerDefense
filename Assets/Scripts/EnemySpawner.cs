using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float waveTime = 60f;
    [SerializeField]
    private float spawnTime = 1f;
    [SerializeField]
    private float stageTime = 60f;
    [SerializeField]
    private GameObject Enemy;

    private float curruntCreateTime;
    private float curruntWaveTime;
    private float curruntStageTime;
    private float baseSpawnTime;

    private bool stageFlag = false;

    public GameObject stageStartBtn;
    public TextMeshProUGUI waveTimeText;
    public TextMeshProUGUI stageText;

    public GameObject shopBtn;
    void Start()
    {
        stageText.text = DataManager.instance.playerData.stage.ToString();
        baseSpawnTime = spawnTime;
    }

    void Update()
    {
        if(stageFlag) // 스테이지 시작
        {
            curruntCreateTime += Time.deltaTime;
            curruntWaveTime += Time.deltaTime;

            waveTimeText.text = (waveTime - curruntWaveTime).ToString("0");

            if (curruntCreateTime > spawnTime)
            {
                Instantiate(Enemy);
                curruntCreateTime = 0f;
            }
        }
        else // 휴식 시간
        {
            curruntStageTime += Time.deltaTime;
            waveTimeText.text = (stageTime - curruntStageTime).ToString("0");

            if((stageTime - curruntStageTime) < 0) // 스테이지 휴식시간이 끝났다면
            {
                StageStart();
            }

        }
        
        if(waveTime - curruntWaveTime < 0) // 스테이지가 끝났다면
        {
            shopBtn.SetActive(true);
            stageFlag = false;
            DataManager.instance.playerData.stage++;
            stageText.text = DataManager.instance.playerData.stage.ToString();
            waveTimeText.text = "0";
            curruntWaveTime = 0f;
            stageStartBtn.SetActive(true);
        }
    }

    public void StageStart()
    {
        curruntStageTime = 0;
        spawnTime = Mathf.Max(0.4f , baseSpawnTime - (DataManager.instance.playerData.stage * 0.1f));
        stageFlag = true;
        waveTimeText.text = waveTime.ToString();
        stageStartBtn.SetActive(false);
        Enemy.GetComponent<HpSystem>().Hp += DataManager.instance.playerData.stage * 5f;
    }
}
