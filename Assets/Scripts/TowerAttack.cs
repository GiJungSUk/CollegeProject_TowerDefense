using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{

    [SerializeField]
    private float attackDamage = 5f;
    [SerializeField]
    private float attackTime = 1f;
    [SerializeField]
    private float bulletSpeed = 3f;
    [SerializeField]
    private GameObject bulletPrefabs;
    [SerializeField]
    private Transform firePos;
    [SerializeField]
    private float attackRange = 2f;

    float curruntTime = 0f;
    public Color gizmoColor = Color.red;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CreateBullet(FindEnemy());
    }

    private GameObject FindEnemy()
    {
        Collider[] col = Physics.OverlapSphere(gameObject.transform.position, attackRange , 1 << 7);
        GameObject shortestEnemy = null;
        float shortestDistance = attackRange;

        foreach (Collider enemy in col)
        {
            if (enemy != null)
            {
                float distance = Vector3.Distance(gameObject.transform.position, enemy.transform.position);
                if (distance <= shortestDistance)
                {
                    shortestDistance = distance;
                    shortestEnemy = enemy.gameObject;
                    
                }
            }
        }
        return shortestEnemy;
    }

    private void CreateBullet(GameObject target)
    {
        curruntTime += Time.deltaTime;
       
        if(curruntTime >=  attackTime)
        {
            if (target != null)
            {
                GameObject bullet = Instantiate(bulletPrefabs, firePos.position , Quaternion.identity);
                Bullet bulletScript = bullet.GetComponent<Bullet>();

                bulletScript.bulletDamage = attackDamage;
                bulletScript.bulletSpeed = bulletSpeed;
                bulletScript.target = target;
            }

            curruntTime = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
