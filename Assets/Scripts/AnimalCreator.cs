using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class AnimalCreator : MonoBehaviour
{
    public static int selectedButtonID;
    [SerializeField]
    private GameObject[] animalDummy;
    [SerializeField]
    private GameObject[] animalPrefabs;

    private bool startCreate= false;


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
