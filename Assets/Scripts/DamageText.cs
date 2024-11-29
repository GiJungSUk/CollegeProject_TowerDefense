using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    TextMeshProUGUI damageText;
    Color damageColor;
    [SerializeField]
    private float textSpeed = 3f;
    [SerializeField]
    private float alphaSpeed = 3f;

    private float curruntTime = 0f;
    
    void Start()
    {
        damageText = GetComponent<TextMeshProUGUI>();
        damageColor = damageText.color;
    }

    // Update is called once per frame
    void Update()
    {

        curruntTime += Time.deltaTime;

        damageText.transform.Translate(new Vector3(0 , textSpeed * Time.deltaTime , 0));
        damageText.color = new Color(damageColor.r, damageColor.g, damageColor.b, Mathf.Lerp(damageText.color.a , 0f , alphaSpeed * Time.deltaTime));

        if(curruntTime > 2f)
        {
            Destroy(transform.root.gameObject);
        }
    }
}
