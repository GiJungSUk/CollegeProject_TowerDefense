using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonID : MonoBehaviour
{
    [SerializeField]
    int buttonID;  // ��ư ���� ��ȣ

    private Button button;

    void Start()
    {
        // Button ������Ʈ�� ������
        button = GetComponent<Button>();

        // Ŭ�� �̺�Ʈ�� �޼ҵ� ���
        button.onClick.AddListener(() => OnButtonClick(buttonID));
    }

    // ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    void OnButtonClick(int id)
    {
        // Ŭ���� ��ư�� ID�� ���� ������ ����
        BuildingSystem.selectedButtonID = id;
    }
}
