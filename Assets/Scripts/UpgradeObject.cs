using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeObject : MonoBehaviour
{
    [SerializeField]
    GameObject upgradeObject;
    [SerializeField]
    GameObject upgradeEffect;

    ObjectInformation obj;
    UIManager uiManager;

    private void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        obj = GetComponent<ObjectInformation>();
    }
    public void Evolve()
    {
        if(upgradeObject != null && obj.level == 10)
        {
            Destroy(gameObject);
            GameObject upgradeObject_ = Instantiate(upgradeObject, gameObject.transform.position, Quaternion.identity);
            Instantiate(upgradeEffect, gameObject.transform.position, Quaternion.identity);
            obj.level = 1;
            uiManager.InputInformation(upgradeObject_);
        }    
    }

    public virtual void Upgrade()
    {
    }
}
