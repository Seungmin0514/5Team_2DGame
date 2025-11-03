using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelectManager : MonoBehaviour
{
    public GameObject panelRoot;          
    public Transform itemListParent;      
    public GameObject itemSlotPrefab;     
    public Text equippedText;             

    SkinCatalog Catalog => SkinCatalog.Instance;

    void Awake() { if (panelRoot) panelRoot.SetActive(false); }

    public void Open() { panelRoot.SetActive(true); Refresh(); }
    public void Close() { panelRoot.SetActive(false); }

    public void Refresh()
    {
        foreach (Transform c in itemListParent) Destroy(c.gameObject);
        var dm = GameDataManager.Instance;
        if (equippedText) equippedText.text = $"현재: {dm.GetEquipped()}";

        foreach (var cfg in Catalog.skins)
        {
            if (cfg == null) continue;
            if (!dm.IsOwned(cfg.skinId)) continue; // 보유한 것만

            var go = Instantiate(itemSlotPrefab, itemListParent);
            go.GetComponent<SkinSelectItemSlot>().Setup(cfg, this);
        }
    }

    public void Equip(SkinConfig cfg)
    {
        GameDataManager.Instance.Equip(cfg.skinId);
        Refresh();
    }
}
