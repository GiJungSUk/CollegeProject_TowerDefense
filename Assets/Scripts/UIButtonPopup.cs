using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonPopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject popup;  // 팝업으로 사용할 UI
    private bool isHovering = false;

    // 마우스가 버튼 위로 올라갔을 때 호출
    public void OnPointerEnter(PointerEventData eventData)
    {
        popup.SetActive(true);  // 팝업 활성화
        isHovering = true;
     
    }

    // 마우스가 버튼에서 벗어났을 때 호출
    public void OnPointerExit(PointerEventData eventData)
    {
        popup.SetActive(false);  // 팝업 비활성화
        isHovering = false;
    }

    private void Update()
    {
        if (isHovering) { // 마우스 커서에 팝업 위치 시키기

            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane;  // 카메라와의 거리 설정
            mousePosition = new Vector3 (mousePosition.x -980, mousePosition.y - 50, mousePosition.z); // 상세한 거리 조정
            popup.transform.localPosition = mousePosition;
        }
    }
}
