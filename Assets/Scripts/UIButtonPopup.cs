using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonPopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject popup;  // �˾����� ����� UI
    private bool isHovering = false;

    // ���콺�� ��ư ���� �ö��� �� ȣ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        popup.SetActive(true);  // �˾� Ȱ��ȭ
        isHovering = true;
     
    }

    // ���콺�� ��ư���� ����� �� ȣ��
    public void OnPointerExit(PointerEventData eventData)
    {
        popup.SetActive(false);  // �˾� ��Ȱ��ȭ
        isHovering = false;
    }

    private void Update()
    {
        if (isHovering) { // ���콺 Ŀ���� �˾� ��ġ ��Ű��

            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane;  // ī�޶���� �Ÿ� ����
            mousePosition = new Vector3 (mousePosition.x -980, mousePosition.y - 50, mousePosition.z); // ���� �Ÿ� ����
            popup.transform.localPosition = mousePosition;
        }
    }
}
