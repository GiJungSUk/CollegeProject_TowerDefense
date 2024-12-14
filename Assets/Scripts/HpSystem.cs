using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpSystem : MonoBehaviour
{
    [SerializeField]
    float hp = 100f;
    public float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            if(hp > 0)
            {
                hp = value;
            }
            else
            {
                Die();
            }
        }
    }
    private void Start()
    {
        Hp = hp;
    }

    private void Die()
    {
            Destroy(gameObject);
    }
}
