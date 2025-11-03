using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Xml.Serialization;

public class PurchaseDialog : MonoBehaviour
{
    [Header("Root")]
    public GameObject panelRoot;

    [Header("UI")]
    public Image portrait;
    public TMP_Text titleText;
    public TMP_Text priceText;
    public TMP_Text warningText;  //"코인이 부족합니다"

    [Header("Button")]
    public Button buyButton;
    public Button cancelButton;

    Action<SkinConfig> onConfirm;
    SkinConfig current;

    void Awake()
    {
        Hide();    
    }

    public void Show(SkinConfig cfg, Action<SkinConfig> confirmCallback)
    {
        current = cfg;
        onConfirm = confirmCallback;

        if(portrait) portrait.sprite = cfg.portrait;
        if (titleText) titleText.text = cfg.displayName;
        if (priceText) priceText.text = cfg.price.ToString();
        if (warningText) warningText.gameObject.SetActive(false);


        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => onConfirm?.Invoke(current));


        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(Hide);


        if (panelRoot) panelRoot.SetActive(true);
    }


    public void ShowInsufficient()
    {
        if (!warningText) return;
        warningText.text = "코인이 부족합니다!";
        warningText.gameObject.SetActive(true);
    }


    public void Hide()
    {
        if (panelRoot) panelRoot.SetActive(false);
    }
}

