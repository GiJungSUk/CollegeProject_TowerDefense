using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletDamage;
    public GameObject target;

    void FixedUpdate()
    {
        if (target == null)
            return;
        BulletMove();
    }

    private void BulletMove()
    {
        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.position += (target.transform.position - transform.position ) * bulletSpeed * Time.deltaTime;
    }
}
