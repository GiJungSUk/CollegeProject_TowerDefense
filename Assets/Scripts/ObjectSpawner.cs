using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    PlayerData data;
    void Awake()
    {
        data = DataManager.instance.playerData;

        for (int i = 0; i < data.objectName.Count; i++)
        {
            GameObject prefab = Resources.Load<GameObject>(data.objectName[i]);

            if (prefab != null)
            {
                // 프리팹 생성
                GameObject object_ = Instantiate(prefab, data.objectPosition[i], Quaternion.Euler(data.objectLotation[i]));
                print("오브젝트 생성!" + prefab);

                ObjectInformation obj = object_.GetComponent<ObjectInformation>();
                object_.tag = "Object";
                obj.level = data.objectLevel[i];

                if (obj.type == ObjectInformation.Type.Building_B)
                {
                    Collider[] hitcollider = Physics.OverlapSphere(data.objectPosition[i], 1f, 1 << 6);
                    foreach (Collider collider in hitcollider)
                    {
                        collider.gameObject.tag = "CantBuild";
                    }
                }
                else
                {
                    RaycastHit hit;

                    Vector3 position = new Vector3(data.objectPosition[i].x, 1, data.objectPosition[i].z);
                    if (Physics.Raycast(position, Vector3.down, out hit, 100f, 1 << 6))
                    {
                        hit.collider.tag = "CantBuild";
                    }
                }
            }
            else
            {
                Debug.LogError("Prefab not found in Resources folder!");
            }

           

           
            
            

        }

    }
}
