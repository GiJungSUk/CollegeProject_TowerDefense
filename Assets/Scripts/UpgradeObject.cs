using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeObject : MonoBehaviour
{
    [SerializeField]
    GameObject upgradeObject;

    public void Upgrade()
    {
        Destroy(gameObject);
        Instantiate(upgradeObject , gameObject.transform.position , Quaternion.identity);
    }
}
