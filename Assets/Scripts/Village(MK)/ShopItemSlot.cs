using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemSlot : MonoBehaviour
{
    [Header("Refs")]
    public Image icon;
    public TMP_Text nameText;
    public TMP_Text priceText;
    public Button buyButton;

    [Header("Style")]
    public Color priceNormal = Color.white;
    public Color priceInsufficient = Color.red;

    ItemDef def;
    ShopManager shop;


    public void Setup(ItemDef def, ShopManager shop)
    {
        this.def = def;
        this.shop = shop;

        if (icon && def.icon) icon.sprite = def.icon;
        if (nameText) nameText.text = def.displayName;
        if (priceText) priceText.text = def.basePrice.ToString();

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => shop.PurChase(def));
    }

    public void UpdateInteractable()
    {
        int coins = GameDataManager.Instance.coins;
        bool canBuy = def != null && coins >= def.basePrice;

        if (buyButton) buyButton.interactable = canBuy;
        if (priceText) priceText.color = canBuy ? priceNormal : priceInsufficient;
    }
}
