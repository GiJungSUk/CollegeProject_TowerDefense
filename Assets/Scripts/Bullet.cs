using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public float bulletSpeed;
    [HideInInspector]
    public float bulletDamage;
    [HideInInspector]
    public GameObject target;

    public GameObject hitEffact;
    public GameObject damageFont;
    public Color textColor;



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
                Instantiate(hitEffact , gameObject.transform.position , Quaternion.identity);
                GameObject damageFontObj = Instantiate(damageFont, gameObject.transform.position, Quaternion.identity);
                damageFontObj.GetComponentInChildren<TextMeshProUGUI>().text = bulletDamage.ToString();
                Destroy(gameObject);
            }
        }
    }
}
