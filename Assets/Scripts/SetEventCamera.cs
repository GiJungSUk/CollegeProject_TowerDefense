using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEventCamera : MonoBehaviour
{
    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        // Canvas 컴포넌트를 가져옵니다.
        Canvas canvas = GetComponent<Canvas>();

        mainCamera = Camera.main;
        // Main Camera를 찾아서 이벤트 카메라로 설정합니다.
        canvas.worldCamera = mainCamera;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
