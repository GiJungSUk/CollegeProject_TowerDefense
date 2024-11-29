using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDoubleAttack : TowerAttack
{


    private TowerInformation towerInfo;

    [SerializeField]
    private Transform firePos2;

    void Start()
    {
        towerInfo = GetComponent<TowerInformation>();
        attackDamage = towerInfo.attackDamage;
        attackTime = towerInfo.attackTime;
        attackRange = towerInfo.attackRange;
        bulletSpeed = towerInfo.bulletSpeed;
        bulletPrefabs = towerInfo.bulletPrefabs;
        firePos = towerInfo.firePos;
    }

    // Update is called once per frame
    void Update()
    {
        CreateBullets(FindEnemy(), firePos, firePos2);
    }

    public void CreateBullets(GameObject target, Transform firePos1 , Transform firePos2)
    {
        curruntTime += Time.deltaTime;

        if (curruntTime >= attackTime)
        {
            if (target != null)
            {
                GameObject bullet1 = Instantiate(bulletPrefabs, firePos1.position, Quaternion.identity);
                GameObject bullet2 = Instantiate(bulletPrefabs, firePos2.position, Quaternion.identity);

                Bullet bulletScript1 = bullet1.GetComponent<Bullet>();
                Bullet bulletScript2 = bullet2.GetComponent<Bullet>();

                Bullet[] bullets = { bulletScript1, bulletScript2 };

                foreach(Bullet bullet in bullets)
                {
                    bullet.bulletDamage = attackDamage;
                    bullet.bulletSpeed = bulletSpeed;
                    bullet.target = target;
                }
            }

            curruntTime = 0;
        }
    }
}
