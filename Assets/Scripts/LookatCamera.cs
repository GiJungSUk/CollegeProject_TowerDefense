using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LookatCamera : MonoBehaviour
{
    Vector3 dir;

    void Start()
    {
        Vector3 startPoint = new Vector3(5, 0, -10);
        Vector3 endPoint = new Vector3(0, 10, -10);
        dir = (endPoint - startPoint).normalized;

    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(dir);
    }
}
