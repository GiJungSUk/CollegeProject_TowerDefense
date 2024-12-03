using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeadLine : MonoBehaviour
{
    [SerializeField]
    private float lineRange = 1f;
    [SerializeField]
    private int damage = 5;

    public Slider slider;
    public TextMeshProUGUI hpText;

    private void Awake()
    {
        slider.value = DataManager.instance.playerData.hp;
        hpText.text = DataManager.instance.playerData.hp.ToString() + " / " + "100";
    }
    private void Update()
    {
        Collider[] col = Physics.OverlapSphere(gameObject.transform.position, lineRange, 1 << 7);

        foreach(Collider collider in col)
        {
            DataManager.instance.playerData.hp -= damage;
            slider.value = DataManager.instance.playerData.hp;
            hpText.text = DataManager.instance.playerData.hp.ToString() + " / " + "100" ; 
            Destroy(collider.gameObject);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineRange);
    }

}
