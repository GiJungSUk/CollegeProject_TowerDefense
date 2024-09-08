using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransfrom;
    [SerializeField]
    private float cameraSpeed = 10f;
    [SerializeField]
    private float WheelSpeed = 10f;
    Camera mainCamera;

   void Start()
    {
        mainCamera = Camera.main;
        mainCamera.transform.position = cameraTransfrom.position;
    }

    
    void Update()
    {
        CameraMove_XZ();
        CameraMove_Y();
        LimitCameraMove();
    }

    private void CameraMove_XZ() 
    {
        float dir_z = Input.GetAxis("Horizontal");
        float dir_x = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(dir_x, 0, -dir_z);
        dir.Normalize();

        mainCamera.transform.position += dir * cameraSpeed * Time.deltaTime;
    }

    private void CameraMove_Y()
    {
        float wheel = Input.GetAxis("Mouse ScrollWheel"); // ¿ß∑Œ »Ÿ 0.1 æ∆∑°∑Œ »Ÿ -0.1

        Vector3 dir_y = new Vector3(0, -wheel, 0);

        mainCamera.transform.position += dir_y * WheelSpeed * Time.deltaTime;
    }

    private void LimitCameraMove()
    {
        mainCamera.transform.position = new Vector3(Mathf.Clamp(mainCamera.transform.position.x, -15, 7)
           , Mathf.Clamp(mainCamera.transform.position.y, 2, 15)
           , Mathf.Clamp(mainCamera.transform.position.z, -15, 0));
    }
}
