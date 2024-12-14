using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeObject : MonoBehaviour
{
    [SerializeField]
    public GameObject upgradeObject;
    [SerializeField]
    public GameObject upgradeEffect;
    [HideInInspector]
    public ObjectInformation obj;

    public UIManager uiManager_;

    private void Start()
    {
        uiManager_ = GameObject.Find("UIManager").GetComponent<UIManager>();
        obj = GetComponent<ObjectInformation>();
    }
    public void Evolve()
    {
        print(obj);
        if(upgradeObject != null && obj.level == 5)
        {
            Destroy(gameObject);
            GameObject upgradeObject_ = Instantiate(upgradeObject, gameObject.transform.position, Quaternion.identity);
            Instantiate(upgradeEffect, gameObject.transform.position, Quaternion.identity);
            Debug.Log(uiManager_);
            uiManager_.InputInformation(upgradeObject_);
        }
        else
        {
            Upgrade();
        }
    }
    
    public virtual void Upgrade()
    {
    }
}
