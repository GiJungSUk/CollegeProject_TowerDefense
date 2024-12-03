using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public static int selectedButtonID;   // ���õ� ��ư�� ���� ��ȣ�� ������ ���� 0~9 �����ǹ� 0~5 ���� 5~10 10~ �����ǹ�
    [SerializeField]
    GameObject[] building; // ������
    [SerializeField]
    GameObject[] dummyObject;
    [SerializeField]
    GameObject buildEffact_s;
    [SerializeField]
    GameObject buildEffact_b;


    List<GameObject> building_Objcet = new List<GameObject>();

    Vector3 originalPos;

    bool startBuild = false;
    void Start()
    {
        originalPos = new Vector3(2, -10, -10);

        for (int i = 0; i < dummyObject.Length; i++)
        {
            if (building[i] != null)
                building_Objcet.Add(Instantiate(dummyObject[i], originalPos, dummyObject[i].transform.rotation));
            else
            {
                building_Objcet.Add(null);
            }
        }
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
                Vector3 buildPos = new Vector3(hit.collider.transform.position.x, 0.1f, hit.collider.transform.position.z); //�Ǽ��� ��ġ
                Collider[] hitcollider = Physics.OverlapSphere(hit.collider.transform.position, 1f, 1 << 6);

                canBuild = JudgeBuild(hit , hitcollider); // �Ǽ� ���ɿ��� Ȯ�� 

                if (canBuild) // �Ǽ� ������ ��
                {
                    ChangeMat(building_Objcet[selectedButtonID], Color.green);
                    building_Objcet[selectedButtonID].transform.position = buildPos;

                    if (Input.GetMouseButtonDown(0)) // Ŭ�� ���� �� �� �ڸ��� �Ǽ�
                    {
                        
                        if (DataManager.instance.playerData.money >=
                            building[selectedButtonID].GetComponent<ObjectInformation>().price) // ������ ����� ��
                        {
                            if (selectedButtonID < 3)
                            {
                                hit.collider.tag = "CantBuild";
                                Instantiate(buildEffact_s, buildPos, Quaternion.identity);
                            } // ���� �ǹ��̸� �� �ڸ��� �Ǽ��Ұ��� �����
                            else if (selectedButtonID >= 3 && selectedButtonID < 10) // ������ ��� �׳� ���ΰ� , fence�� ���� �ٲ��� ����
                            {

                            }
                            else // ���� �ǹ��̸� 3x3 ������ �Ǽ� �Ұ��� �����
                            {
                                foreach (Collider collider in hitcollider)
                                {
                                    collider.tag = "CantBuild";
                                    Instantiate(buildEffact_b, buildPos, Quaternion.identity);
                                }
                            }

                            DataManager.instance.playerData.money -= building[selectedButtonID].GetComponent<ObjectInformation>().price; // ������ �����ϰ�
                            ShopManager.instance.GoldTextUpdate(); // �ؽ�Ʈ�� ������Ʈ��
                            GameObject building_Instant = Instantiate(building[selectedButtonID], buildPos, building_Objcet[selectedButtonID].transform.rotation);
                            building_Instant.tag = "Object"; // �±׸� ������Ʈ�� �ٲٴ� ������ �Ǽ� �̿Ϸ� ���¿��� �Ǽ� �� ���� �˾��� �߿��� �ʱ� ���ؼ��̴�.
                            startBuild = false;
                            building_Objcet[selectedButtonID].transform.position = originalPos;
                            
                        }
                        else{ //  �������� �ٽ� ������� �ǵ���
                            for (int i = 0; i < building.Length; i++)
                                if (building[i] != null)
                                    building_Objcet[i].transform.position = originalPos;
                            startBuild = false;

                        }
                        
                    }
                }

                else if (!canBuild) // �Ǽ� �Ұ����� ��
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

        if (hit.collider.name.Contains("tile") && (selectedButtonID != 0)) // Ÿ����ġŸ���ε� Ÿ���� �ƴҰ�� ��ġ �Ұ�
            return returnFlag;
        
        if(!hit.collider.name.Contains("tile") && (selectedButtonID == 0)) // Ÿ����ġġŸ���� �ƴѵ� Ÿ���� ��� ��ġ �Ұ�
            return returnFlag;

        if (selectedButtonID >= 10) // 10 �̻��̸� ���� �ǹ�
        {
            foreach (Collider collider in hitcollider)
            {
                if (collider.gameObject.tag == "CantBuild")
                    return returnFlag;
            }
            returnFlag = true;
            return returnFlag;
        }
        else // 10 ���ϸ� ���� �ǹ�
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
        if(selectedButtonID >= 5 && selectedButtonID < 10) // ������ ��� ���׸��� ���� ����
        {
            return;
        }
        Renderer[] renderer = objcet.GetComponentsInChildren<Renderer>();

        foreach(Renderer rend  in renderer)
        {
            Material[] materials = rend.materials;

            // �� ���׸����� ������ ����
            foreach (Material mat in materials)
            {
                mat.color = color;
            }
        }
       
    }

    private void RotationChange()
    {
        if (Input.GetKeyDown(KeyCode.R) && selectedButtonID != 1) // �� ȸ�� �Ƚ�Ŵ
        {
            building_Objcet[selectedButtonID].transform.eulerAngles = new Vector3(0, building_Objcet[selectedButtonID].transform.eulerAngles.y + 90, 0); 
        }
    }

    public void StartBuild()
    {
        for (int i = 0; i < building.Length; i++) // ���� Ÿ��1�� �ϴٰ� 2�� ������ �� �������� �ٽ� ������� ����
            if (building[i] != null)
                building_Objcet[i].transform.position = originalPos;
        startBuild = true;
    }
}
