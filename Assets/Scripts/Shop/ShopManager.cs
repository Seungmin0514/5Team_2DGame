using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class ShopManager : MonoBehaviour
{
    public GameObject panelRoot;          
    public SkinCatalog catalog;
    public Transform itemListParent;      
    public GameObject itemSlotPrefab;     
    public TMP_Text coinsText;
    public PurchaseDialog purchaseDialog;

    [Header("Shop Listing")]
    public List<string> saleSkinIds = new();

    void Awake() 
    {
        if (panelRoot) panelRoot.SetActive(false);
    }

    void OnEnable()
    {
        GameSignals.OnCoinsChanged += _ => RefreshUI();
        GameSignals.OnSkinBought += _ => RefreshUI();
        GameSignals.OnSkinEquipped += _ => RefreshUI();
    }
    void OnDisable()
    {
        GameSignals.OnCoinsChanged -= _ => RefreshUI();
        GameSignals.OnSkinBought -= _ => RefreshUI();
        GameSignals.OnSkinEquipped -= _ => RefreshUI();
    }

    public void OpenShop()
    {
        panelRoot.SetActive(true);
        RefreshUI();
    }
    public void CloseShop() => panelRoot.SetActive(false);
    public void ToggleShop() { if (panelRoot.activeSelf) CloseShop(); else OpenShop(); }

    public void RefreshUI()
    {
        if (coinsText) coinsText.text = "Coins: " + GameDataManager.Instance.GetCoins();
        foreach (Transform c in itemListParent) Destroy(c.gameObject);
        foreach (var id in saleSkinIds)
        {
            var cfg = catalog?.Get(id);
            if (cfg == null) continue;
            var go = Instantiate(itemSlotPrefab, itemListParent);
            go.GetComponent<ShopItemSlot>().Setup(cfg, this);
        }
    }

    public void OpenPurchaseDialog(SkinConfig cfg)
    {
        if (cfg == null || !purchaseDialog) return;
        purchaseDialog.Show(cfg, TryBuyAndEquip);
    }

    void TryBuyAndEquip(SkinConfig cfg)
    {
        var dm = GameDataManager.Instance;

        if (dm.IsOwned(cfg.skinId)) { dm.Equip(cfg.skinId); purchaseDialog.Hide(); return; }

        if (!dm.TrySpendCoins(cfg.price)) { purchaseDialog.ShowInsufficient(); return; }

        dm.AddOwned(cfg.skinId);
        dm.Equip(cfg.skinId);
        purchaseDialog.Hide();
    }
}