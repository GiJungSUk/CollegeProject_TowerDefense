using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonID : MonoBehaviour
{
    [SerializeField]
    int buttonID;  // 버튼 고유 번호

    private Button button;

    void Start()
    {
        // Button 컴포넌트를 가져옴
        button = GetComponent<Button>();

        // 클릭 이벤트에 메소드 등록
        button.onClick.AddListener(() => OnButtonClick(buttonID));
    }

    // 버튼 클릭 시 호출되는 함수
    void OnButtonClick(int id)
    {
        // 클릭된 버튼의 ID를 전역 변수에 저장
        BuildingSystem.selectedButtonID = id;
    }
}
