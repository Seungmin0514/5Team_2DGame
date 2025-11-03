using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinSelectItemSlot : MonoBehaviour
{
    public Image portraitImage;
    public TMP_Text nameText;
    public TMP_Text statusText;
    public Button equipButton;

    SkinConfig cfg;
    SkinSelectManager owner;

    public void Setup(SkinConfig cfg, SkinSelectManager owner)
    {
        this.cfg = cfg; this.owner = owner;
        if (portraitImage && cfg.portrait) portraitImage.sprite = cfg.portrait;
        if (nameText) nameText.text = cfg.displayName;

        equipButton.onClick.RemoveAllListeners();
        equipButton.onClick.AddListener(() => owner.Equip(cfg));

        Refresh();
    }

    public void Refresh()
    {
        bool eq = (GameDataManager.Instance.GetEquipped() == cfg.skinId);
        if (statusText) statusText.text = eq ? "Equipped" : "";
        if (equipButton) equipButton.interactable = !eq;
    }
}
