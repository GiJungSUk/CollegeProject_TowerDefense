using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Enemy;
    [SerializeField]
    private float spawnTime = 1f;
    private float curruntTime;
    void Start()
    {
        
    }

    void Update()
    {
        curruntTime += Time.deltaTime;
        if(curruntTime > spawnTime)
        {
            Instantiate(Enemy);
            curruntTime = 0f;
        }
        
    }
}
