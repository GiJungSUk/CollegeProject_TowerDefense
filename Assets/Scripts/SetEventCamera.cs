using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEventCamera : MonoBehaviour
{
    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        // Canvas ������Ʈ�� �����ɴϴ�.
        Canvas canvas = GetComponent<Canvas>();

        mainCamera = Camera.main;
        // Main Camera�� ã�Ƽ� �̺�Ʈ ī�޶�� �����մϴ�.
        canvas.worldCamera = mainCamera;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
