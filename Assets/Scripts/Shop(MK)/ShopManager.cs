using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [Header("Catalog & sale")]
    public SkinCatalog Catalog;
    public List<string> saleSkinIds = new();

    [Header("UI Roots")]
    public GameObject panelRoot;
    public Transform itemListParent;
    public GameObject itemSlotPrefab;
    public TMP_Text coinsText;

    [Header("Dialog")]
    public PurchaseDialog purchaseDialog;

    Action<int> _onCoinsChanged;
    Action<string> _onSkinBought;
    Action<string> _onSkinEquipped;

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        _onCoinsChanged = _ => RefreshUI();
        _onSkinBought = _ => RefreshUI();
        _onSkinEquipped = _ => RefreshUI();

        GameSignals.OnCoinsChanged += _onCoinsChanged;
        GameSignals.OnSkinBought += _onSkinBought;
        GameSignals.OnSkinEquipped += _onSkinEquipped;

        // 패널이 켜져있다면 내용 채우기
        if (panelRoot && panelRoot.activeInHierarchy) RefreshUI();
    }


    void OnDisable()
    {
        GameSignals.OnCoinsChanged -= _onCoinsChanged;
        GameSignals.OnSkinBought -= _onSkinBought;
        GameSignals.OnSkinEquipped -= _onSkinEquipped;
    }


    // 열기/닫기/토글
    public void OpenShop() { if (panelRoot) { panelRoot.SetActive(true); RefreshUI(); } }
    public void CloseShop() { if (panelRoot) { panelRoot.SetActive(false); } }
    public void ToggleShop() { if (!panelRoot) return; panelRoot.SetActive(!panelRoot.activeSelf); if (panelRoot.activeSelf) RefreshUI(); }


    public void RefreshUI()
    {
        if (coinsText) coinsText.text = "Coins: " + GameDataManager.Instance.GetCoins();


        foreach (Transform c in itemListParent) Destroy(c.gameObject);


        foreach (var id in saleSkinIds)
        {
            var cfg = Catalog?.Get(id);
            if (cfg == null) continue;


            var go = Instantiate(itemSlotPrefab, itemListParent);
            var slot = go.GetComponent<ShopItemSlot>();
            slot.Setup(cfg, this);
            slot.RefreshView();
        }
    }


    // 슬롯 클릭 → 모달 열기
    public void OpenPurchaseDialog(SkinConfig cfg)
    {
        if (cfg == null || purchaseDialog == null) return;
        purchaseDialog.Show(cfg, TryBuyAndEquip);
    }


    // 모달 구매 버튼 → 시도
    void TryBuyAndEquip(SkinConfig cfg)
    {
        var dm = GameDataManager.Instance;


        if (dm.IsOwned(cfg.skinId))
        {
            dm.Equip(cfg.skinId); // 이미 보유 장착만
            return;
        }


        if (!dm.TrySpendCoins(cfg.price))
        {
            purchaseDialog.ShowInsufficient(); // 경고 노출
            return;
        }


        dm.AddOwned(cfg.skinId); // 해금
        dm.Equip(cfg.skinId); // 바로 장착(원하면 제거 가능)
    }

}