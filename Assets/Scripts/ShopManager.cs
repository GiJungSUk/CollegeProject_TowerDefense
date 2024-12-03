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
    public TextMeshProUGUI beetText;
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
    public GameObject purchaseOrderTab;

    public Image purchaseImage;
    public TextMeshProUGUI purchaseNameText;
    public TextMeshProUGUI purchasePriceText;
    public Slider purchaseSlider;
    public TextMeshProUGUI maxPurchaseValueText;
    

    // selling part 

    public Button[] sellingButtons;
    public Button sellingButton;
    public GameObject sellingOrderTab;

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
                button.onClick.AddListener(() => UpdateOrderTap(sellingImage,sellingNameText,sellingPriceText));
                button.onClick.AddListener(() => SellingMaxValueChange(sellingSlider, maxSellingValueText));
            }
        }

        if(purchaseButtons != null)
        {
            foreach(Button button in purchaseButtons)
            {
                button.onClick.AddListener(() => UpdateOrderTap(purchaseImage,purchaseNameText,purchasePriceText)); 
                button.onClick.AddListener(() => PurchaseMaxValueChange(purchaseSlider, maxPurchaseValueText));
            }
        }

        if (purchaseButton != null)
            purchaseButton.onClick.AddListener(() => Order(purchaseSlider, purchaseMiddle, -1));

        if (sellingButton != null)
            sellingButton.onClick.AddListener(() => Order(sellingSlider, sellingMiddle, +1));

    }

    public void UpdateOrderTap(Image image , TextMeshProUGUI nameText, TextMeshProUGUI priceText)
    {
        image.sprite = currentimage;
        nameText.text = currentName;
        priceText.text = currentPrice.ToString();
    }

    private void PurchaseMaxValueChange(Slider slider, TextMeshProUGUI maxValueText)
    {
        slider.maxValue = (int)(data.money / currentPrice);
        maxValueText.text = slider.maxValue.ToString();
        if(slider.maxValue > 0)
        {
            purchaseOrderTab.SetActive(true);
        }
    }

    private void SellingMaxValueChange(Slider slider, TextMeshProUGUI maxValueText)
    {
        TextMeshProUGUI text = (TextMeshProUGUI)GetType().GetField(currentName + "Text").GetValue(this);
        maxValueText.text = text.text;
        slider.maxValue = int.Parse(maxValueText.text);
        if(slider.maxValue > 0)
        {
            sellingOrderTab.SetActive(true);
        }
    }

    public void UpdateProduct()
    {
        data = DataManager.instance.playerData;

        tomatoText.text = data.tomato.ToString();
        pumpkinText.text = data.pumpkin.ToString();
        beetText.text = data.beet.ToString();
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

        if(pulma == 1) // 판매일때만 작동하도록 하자
        {
            TextMeshProUGUI productText = (TextMeshProUGUI)GetType().GetField(currentName + "Text").GetValue(this);
            productText.text = (int.Parse(productText.text) - (int)slider.value).ToString(); // 판매할 재료의 텍스트값과 판매할양을 뺀 값으로 바꿈
                                                                                             // << 소지이상으로 판매할수 없도록하는 장치
        }




        int count = (int)slider.value;
        string name = currentName;
        //Action 변수에 실행할 함수를 미리 담아놓는다
        order += ()  => InsertAction(count , pulma , name);

        

        orderList.Add(orderObjects);
        slider.value = 0;
    }

    private void InsertAction(int count, int pulma , string name)
    {
        int value = (int)Type.GetType("PlayerData").GetField(name).GetValue(data);
        Type.GetType("PlayerData").GetField(name).SetValue(data, value + -pulma * count); // 개수는 돈과 반대로 감소하면 증가하고 증가하면 감소함
    }

    public void CancelOrder()
    {
        foreach(GameObject objects  in orderList)
        {
            Destroy(objects);
            total = 0;
            totalText.text = "0";
        }
        order = null;
        UpdateProduct();
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
