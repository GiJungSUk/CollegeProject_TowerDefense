using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField]
    GameObject tower;
    [SerializeField]
    Material canBuildMat;
    [SerializeField]
    Material cantBuildMat;

    GameObject tower_;
    Vector3 originalPos;

    bool startBuild = false;
    void Start()
    {
        originalPos = new Vector3(2, -5,-10);
        tower_ = Instantiate(tower , originalPos, Quaternion.identity);
    }

    void Update()
    {
        if(startBuild)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray , out hit))
            {
                if (hit.collider.tag == "CanBuild")
                {
                    tower_.transform.position = hit.collider.transform.position;
                    Renderer renderer = tower_.GetComponent<Renderer>();

                    Material[] materials = renderer.materials;

                    // 각 머테리얼의 색상을 변경
                    foreach (Material mat in materials)
                    {
                        mat.color = Color.green;
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        hit.collider.tag = "CantBuild";
                        Instantiate(tower, hit.collider.transform.position, Quaternion.identity);
                        startBuild = false;
                        tower_.transform.position = originalPos;
                    }
                }
                else if(hit.collider.tag == "CantBuild")
                {
                    tower_.transform.position = hit.collider.transform.position;
                    tower_.GetComponent<Renderer>().material = cantBuildMat;
                }

               

            }
        }
    }

    public void StartBuild()
    {
        startBuild = true;
    }
}
