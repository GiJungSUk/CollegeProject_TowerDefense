using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [HideInInspector]
    public float attackDamage;
    [HideInInspector]
    public float attackTime;
    [HideInInspector]
    public float bulletSpeed;
    [HideInInspector]
    public Transform firePos;
    [HideInInspector]
    public float attackRange;
    public GameObject bulletPrefabs;
    [HideInInspector]
    public float curruntTime = 0f;
    public Color gizmoColor = Color.red;

    [SerializeField]
    bool lookAtFlag = false;

    private TowerInformation towerInfo_;
    void Start()
    {
        towerInfo_ = GetComponent<TowerInformation>();
        setBulletStats(towerInfo_);
    }

    void Update()
    {
        CreateBullet(FindEnemy() , firePos);
    }


    public void setBulletStats(TowerInformation towerInfo)
    {
        attackDamage = towerInfo.attackDamage;
        attackTime = towerInfo.attackTime;
        attackRange = towerInfo.attackRange;
        bulletSpeed = towerInfo.bulletSpeed;
        bulletPrefabs = towerInfo.bulletPrefabs;
        firePos = towerInfo.firePos;
    }

    public GameObject FindEnemy()
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

                    if(lookAtFlag)
                    transform.LookAt(shortestEnemy.transform);
                    
                }
            }
        }
        return shortestEnemy;
    }

    public void CreateBullet(GameObject target , Transform firePos)
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

    public void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
