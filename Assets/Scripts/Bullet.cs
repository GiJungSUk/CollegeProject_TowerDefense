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
        {
            Destroy(gameObject);
            return;
        }
            
        BulletMove();
        BulletDamaged();
    }
    private void Update()
    {
        
    }

    private void BulletMove()
    {
        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.position += (target.transform.position - transform.position ) * bulletSpeed * Time.deltaTime;
    }

    private void BulletDamaged()
    {
        Collider[] col = Physics.OverlapSphere(gameObject.transform.position, 0.2f, 1 << 7);

        foreach (Collider enemy in col)
        {
            if (enemy != null)
            {
                enemy.GetComponent<HpSystem>().Hp -= bulletDamage;
                Destroy(gameObject);
            }
        }
    }
}
