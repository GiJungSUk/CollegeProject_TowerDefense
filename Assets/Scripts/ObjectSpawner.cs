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
                // ������ ����
                Instantiate(prefab, data.objectPosition[i], Quaternion.identity);
                print("������Ʈ ����!" + prefab);
            }
            else
            {
                Debug.LogError("Prefab not found in Resources folder!");
            }

            ObjectInformation obj = prefab.GetComponent<ObjectInformation>();
            prefab.tag = "Object";

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
            
            obj.level = data.objectLevel[i];

        }

    }
}
