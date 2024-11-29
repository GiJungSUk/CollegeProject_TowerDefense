using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueChanged : MonoBehaviour
{
    public Slider mySlider;
    public TextMeshProUGUI priceText;  

    private void Start()
    {
        mySlider = GetComponent<Slider>();

        if(mySlider != null)
        {
            mySlider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    void OnSliderValueChanged(float value)
    {
        priceText.text = (mySlider.value * ShopManager.instance.currentPrice).ToString();
    }
}
