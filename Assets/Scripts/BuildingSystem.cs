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
            else
            {
                building_Objcet.Add(null);
            }
               
        }

        building_Objcet[0].GetComponent<TowerAttack>().enabled = false;
    }
    void Update()
    {
       BuildSystem();
       RotationChange();
    }

    private void BuildSystem()
    {
        if (startBuild)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit , 100f ,1 << 6))
            {
                bool canBuild;
                Vector3 buildPos = new Vector3(hit.collider.transform.position.x, 0.1f, hit.collider.transform.position.z); //건설할 위치
                Collider[] hitcollider = Physics.OverlapSphere(hit.collider.transform.position, 1f, 1 << 6);

                canBuild = JudgeBuild(hit , hitcollider); // 건설 가능여부 확인 

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
                        GameObject building_Instant = Instantiate(building[selectedButtonID], buildPos , building_Objcet[selectedButtonID].transform.rotation);
                        building_Instant.tag = "Object"; // 태그를 오브젝트로 바꾸는 이유는 건설 미완료 상태에서 건설 시 바텀 팝업을 뜨우지 않기 위해서이다.
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

        if (hit.collider.name.Contains("tile") && (selectedButtonID != 0)) // 타워설치타일인데 타워가 아닐경우 설치 불가
            return returnFlag;
        
        if(!hit.collider.name.Contains("tile") && (selectedButtonID == 0)) // 타워설치치타일이 아닌데 타워일 경우 설치 불가
            return returnFlag;

        if (selectedButtonID >= 10) // 10 이상이면 대형 건물
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

    private void RotationChange()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            print("회전");
            building_Objcet[selectedButtonID].transform.eulerAngles = new Vector3(0, building_Objcet[selectedButtonID].transform.eulerAngles.y + 90, 0); 
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
