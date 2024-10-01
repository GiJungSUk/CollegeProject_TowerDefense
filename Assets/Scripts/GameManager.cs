using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject bottomPanel;
    public Image photo;
    public TextMeshProUGUI name;
    public TextMeshProUGUI level;
    
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            FindObject();
        }
        
    }

    private void FindObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray , out hit))
        {
            if(hit.collider != null && hit.collider.gameObject.tag == "Object")
            {
                bottomPanel.gameObject.SetActive(true);
                name.text = hit.collider.gameObject.name;
            }
        }
    }
}
