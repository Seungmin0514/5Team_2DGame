using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemSlot : MonoBehaviour
{
    public Image portraitImage;
    public TMP_Text nameText;
    public TMP_Text priceText;
    public TMP_Text statusText;
    public GameObject lockOverlay;
    public Button openDialogButton;

    SkinConfig cfg;
    ShopManager shop;

    public void Setup(SkinConfig cfg, ShopManager shop)
    {
        this.cfg = cfg; 
        this.shop = shop;
        if (portraitImage) portraitImage.sprite = cfg.portrait;
        if (nameText) nameText.text = cfg.displayName;
        if (priceText) priceText.text = cfg.price.ToString();

        openDialogButton.onClick.RemoveAllListeners();
        openDialogButton.onClick.AddListener(() => shop.OpenPurchaseDialog(cfg));

        //RefreshView();
    }

    public void RefreshView()
    {
        var dm = GameDataManager.Instance;
        bool owned = dm.IsOwned(cfg.skinId);
        bool equipped = owned && (dm.GetEquipped() == cfg.skinId);

        if (portraitImage)
            portraitImage.sprite = owned ? cfg.portrait : (cfg.silhouette ? cfg.silhouette : cfg.portrait);

        if (lockOverlay) lockOverlay.SetActive(!owned);
        if (statusText) statusText.text = equipped ? "Equipped" : (owned ? "Owned" : "Locked");
    }
}