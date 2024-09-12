using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public static int selectedButtonID;   // 선택된 버튼의 고유 번호를 저장할 변수 0~9 소형건물 10 ~ 대형건물
    [SerializeField]
    GameObject[] building; // 프리펩
    List<GameObject> building_Objcet = new List<GameObject>(); //실제로 생성된 프리펩들

    Vector3 originalPos;

    bool startBuild = false;
    void Start()
    {
        originalPos = new Vector3(2, -5, -10);

        for (int i = 0; i < building.Length; i++)
        {
            if (building[i] != null)
                building_Objcet.Add(Instantiate(building[i], originalPos, Quaternion.identity));
        }
    }
    void Update()
    {
       BuildSystem();
    }
    private void BuildSystem()
    {
        if (startBuild)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                bool canBuild;
                Vector3 buildPos = new Vector3(hit.collider.transform.position.x, 0.1f, hit.collider.transform.position.z); //건설할 위치
                Collider[] hitcollider = Physics.OverlapSphere(hit.collider.transform.position, 1f, 1 << 6);

                canBuild = JudgeBuild(hit , hitcollider);

                if (canBuild) // 건설 가능일 때
                {
                    ChangeMat(building_Objcet[selectedButtonID], Color.green);
                    building_Objcet[selectedButtonID].transform.position = buildPos;

                    if (Input.GetMouseButtonDown(0)) // 클릭 했을 때 그 자리에 건설
                    {
                        if(selectedButtonID < 10) // 소형 건물이면 그 자리만 건설불가로 만들고
                            hit.collider.tag = "CantBuild";
                        else // 대형 건물이면 3x3 지형을 건설 불가로 만든다
                        {
                            foreach (Collider collider in hitcollider)
                            {
                                collider.tag = "CantBuild";
                            }
                        }
                        Instantiate(building[selectedButtonID], buildPos , Quaternion.identity);
                        startBuild = false;
                        building_Objcet[selectedButtonID].transform.position = originalPos; 
                    }
                }

                else if (!canBuild) // 건설 불가능할 때
                {
                    ChangeMat(building_Objcet[selectedButtonID], Color.red);
                    building_Objcet[selectedButtonID].transform.position = buildPos;
                }
            }
        }
    }

    private bool JudgeBuild(RaycastHit hit , Collider[] hitcollider)
    {
        bool returnFlag = false;

        if(selectedButtonID >= 10) // 10 이상이면 대형 건물
        {
            foreach (Collider collider in hitcollider)
            {
                if (collider.gameObject.tag == "CantBuild")
                    return returnFlag;
            }
            returnFlag = true;
            return returnFlag;
        }
        else // 10 이하면 소형 건물
        {
            if (hit.collider.tag == "CanBuild")
            {
                returnFlag = true;
                return returnFlag;
            }
            else
                return returnFlag;
        }
       
    }

    private void ChangeMat(GameObject objcet, Color color)
    {
        Renderer[] renderer = objcet.GetComponentsInChildren<Renderer>();

        foreach(Renderer rend  in renderer)
        {
            Material[] materials = rend.materials;

            // 각 머테리얼의 색상을 변경
            foreach (Material mat in materials)
            {
                mat.color = color;
            }
        }
       
    }

    public void StartBuild()
    {
        for (int i = 0; i < building.Length; i++) // 만약 타워1을 하다가 2를 누르면 그 이전꺼를 다시 원래대로 돌림
            if (building[i] != null)
                building_Objcet[i].transform.position = originalPos;
    
        startBuild = true;
    }

    
}
