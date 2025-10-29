using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour
{
    public Text nameText;
    public Text priceText;
    public Button buyButton;

    ShopItemData dataRef;
    ShopManager shopRef;

    public void Setup(ShopItemData data, ShopManager shop)
    {
        dataRef = data;
        shopRef = shop;

        nameText.text = data.itemName;
        priceText.text = data.price.ToString();

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => { shopRef.PurChase(dataRef); });
    }
}
