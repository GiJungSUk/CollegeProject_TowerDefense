using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public static int selectedButtonID;   // ���õ� ��ư�� ���� ��ȣ�� ������ ����
    [SerializeField]
    GameObject[] building; // ������
    List<GameObject> building_Objcet = new List<GameObject>(); //������ ������ �������

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
                Vector3 buildPos = new Vector3(hit.collider.transform.position.x, 0.1f, hit.collider.transform.position.z); //�Ǽ��� ��ġ

                if (hit.collider.tag == "CanBuild") // �Ǽ� ������ ��
                {
                    ChangeMat(building_Objcet[selectedButtonID], Color.green);
                    building_Objcet[selectedButtonID].transform.position = buildPos;

                    if (Input.GetMouseButtonDown(0)) // Ŭ�� ���� �� �� �ڸ��� �Ǽ�
                    {
                        hit.collider.tag = "CantBuild";
                        Instantiate(building[selectedButtonID], buildPos , Quaternion.identity);
                        startBuild = false;
                        building_Objcet[selectedButtonID].transform.position = originalPos;
                    }
                }

                else if (hit.collider.tag == "CantBuild") // �Ǽ� �Ұ����� ��
                {
                    ChangeMat(building_Objcet[selectedButtonID], Color.red);
                    building_Objcet[selectedButtonID].transform.position = buildPos;
                }
            }
        }
    }

    private void ChangeMat(GameObject objcet, Color color)
    {
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

    public void StartBuild()
    {
        for (int i = 0; i < building.Length; i++) // ���� Ÿ��1�� �ϴٰ� 2�� ������ �� �������� �ٽ� ������� ����
            if (building[i] != null)
                building_Objcet[i].transform.position = originalPos;
    
        startBuild = true;
    }

    
}
