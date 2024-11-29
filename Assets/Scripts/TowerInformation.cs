using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInformation : ObjectInformation
{
    public float attackDamage = 5f;
    public float attackTime = 1f;
    public float bulletSpeed = 3f;
    public GameObject bulletPrefabs;
    public Transform firePos;
    public  float attackRange = 2f;
}
