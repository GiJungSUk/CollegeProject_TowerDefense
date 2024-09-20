using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationChanger : MonoBehaviour
{
    [SerializeField]
    private int rotation = 90;
    [SerializeField]
    private float hitArea = 0.2f;
    void Update()
    {
        Collider[] hit = Physics.OverlapSphere(gameObject.transform.position, hitArea, 1 << 7);

        foreach(Collider col in hit)
        {
            col.transform.eulerAngles = new Vector3(0, rotation , 0);
            print(hit);
        }

    }
}
