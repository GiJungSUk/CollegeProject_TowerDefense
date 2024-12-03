using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecellObject : MonoBehaviour
{
    private ObjectInformation obj;
    private UIManager uiManager_;
    [SerializeField]
    private GameObject recellEffact;

    private void Awake()
    {
        uiManager_ = GameObject.Find("UIManager").GetComponent<UIManager>();
        obj = gameObject.GetComponent<ObjectInformation>();
    }
    public void Recell()
    {
        if (obj.type == ObjectInformation.Type.Building_B)
        {
            Collider[] hitcollider = Physics.OverlapSphere(gameObject.transform.position, 1f, 1 << 6);
            foreach (Collider collider in hitcollider)
            {
                collider.gameObject.tag = "CanBuild";
            }
        }
        else
        {
            RaycastHit hit;

            Vector3 position = new Vector3(gameObject.transform.position.x, 1 , gameObject.transform.position.z);
            if (Physics.Raycast(position, Vector3.down, out hit, 100f, 1 << 6))
            {
                hit.collider.tag = "CanBuild";       
            }
        }
        DataManager.instance.playerData.money += (int)(obj.price / 2);
        ShopManager.instance.GoldTextUpdate();
        uiManager_.bottomPanel.SetActive(false);
        Instantiate(recellEffact,gameObject.transform.position, Quaternion.identity);   
        Destroy(gameObject);
    }
}
