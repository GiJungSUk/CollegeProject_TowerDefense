using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    //내가 가지고있는 자산 text
    public TextMeshProUGUI tomatoText;
    public TextMeshProUGUI pumpkinText;
    public TextMeshProUGUI beatText;
    public TextMeshProUGUI wheatText;
    public TextMeshProUGUI carrotText;
    public TextMeshProUGUI woolText;
    public TextMeshProUGUI milkText;
    public TextMeshProUGUI eggText;


    [HideInInspector]
    public Sprite currentimage;
    [HideInInspector]
    public string currentName;
    [HideInInspector]
    public int currentPrice;

    // purchase part
    public Button[] purchaseButtons;
    public Button purchaseButton;

    public Image purchaseImage;
    public TextMeshProUGUI purchaseNameText;
    public TextMeshProUGUI purchasePriceText;
    public Slider purchaseSlider;
    public TextMeshProUGUI maxPurchaseValueText;
    

    // selling part 

    public Button[] sellingButtons;
    public Button sellingButton;

    public Image sellingImage;
    public TextMeshProUGUI sellingNameText;
    public TextMeshProUGUI sellingPriceText;
    public Slider sellingSlider;
    public TextMeshProUGUI maxSellingValueText;

    // 상점 가운데 Ui
    public GameObject purchaseMiddle;
    public GameObject sellingMiddle;
    public GameObject shop;
    public Transform middleArea;
    public TextMeshProUGUI totalText;

    public TextMeshProUGUI topGoldText;
    int total;
    List<GameObject> orderList;
    Action order;

    PlayerData data;
    public static ShopManager instance;


    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        orderList = new List<GameObject>();
        data = DataManager.instance.playerData;

        if (sellingButtons != null)
        {
            foreach (Button button in sellingButtons)
            {
                button.onClick.AddListener(() => UpdateOrderTap(sellingImage,sellingNameText,sellingPriceText,sellingSlider,maxSellingValueText));
            }
        }

        if(purchaseButtons != null)
        {
            foreach(Button button in purchaseButtons)
            {
                button.onClick.AddListener(() => UpdateOrderTap(purchaseImage,purchaseNameText,purchasePriceText,purchaseSlider,maxPurchaseValueText));
            }
        }

        if (purchaseButton != null)
            purchaseButton.onClick.AddListener(() => Order(purchaseSlider, purchaseMiddle, -1));

        if (sellingButton != null)
            sellingButton.onClick.AddListener(() => Order(sellingSlider, sellingMiddle, +1));

    }

    public void UpdateOrderTap(Image image , TextMeshProUGUI nameText, TextMeshProUGUI priceText , Slider slider , TextMeshProUGUI maxValueText)
    {
        image.sprite = currentimage;
        nameText.text = currentName;
        priceText.text = currentPrice.ToString();

        //slider.maxValue = (int)(data.money / curruntPrice);
        //maxValueText.text = slider.maxValue.ToString();
    }

    public void UpdateProduct()
    {
        data = DataManager.instance.playerData;

        tomatoText.text = data.tomato.ToString();
        pumpkinText.text = data.pumpkin.ToString();
        beatText.text = data.beet.ToString();
        wheatText.text = data.wheat.ToString();
        carrotText.text = data.carrot.ToString();
        woolText.text = data.wool.ToString();
        milkText.text = data.milk.ToString();
        eggText.text = data.egg.ToString();
    }

    public void Order(Slider slider , GameObject orderObject , int pulma)
    {
        total = total +  pulma*(int)(slider.value * currentPrice);
        totalText.text = total.ToString();

        GameObject orderObjects = Instantiate(orderObject, middleArea);
        TextMeshProUGUI[] texts = orderObjects.GetComponentsInChildren<TextMeshProUGUI>();
        Image[] images = orderObjects.GetComponentsInChildren<Image>();

        foreach (Image image in images)
        {
            if (image.name == "ProductImage")
                image.sprite = currentimage;
        }

        foreach (TextMeshProUGUI text in texts)
        {
            if (text.name == "PriceText")
            {
                text.text = (slider.value * currentPrice).ToString();
            }
            if (text.name == "ProductCount")
            {
                text.text = slider.value.ToString();
            }
        }

        //Action 변수에 실행할 함수를 미리 담아놓는다
        order += () =>
        {
            int value = (int)Type.GetType("PlayerData").GetField(currentName).GetValue(data);
            Type.GetType("PlayerData").GetField(currentName).SetValue(data, (int)(value + pulma * slider.value));
        };

        Debug.Log((int)Type.GetType("PlayerData").GetField(currentName).GetValue(data));

        orderList.Add(orderObjects);
        slider.value = 0;
    }

    public void CancelOrder()
    {
        foreach(GameObject objects  in orderList)
        {
            Destroy(objects);
            total = 0;
            totalText.text = "0";
        }
    }

    public void CheckObject()
    {
    }

    public void ConfirmOrder()
    {
        if(data.money + total >= 0)
        {
            data.money += total;
            order();
            order = null;
            CancelOrder();
            print("주문완료");
        }
        GoldTextUpdate();
    }

    public void GoldTextUpdate()
    {
        topGoldText.text = DataManager.instance.playerData.money.ToString();
    }
}
