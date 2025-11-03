using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinApplier : MonoBehaviour
{
    public SkinCatalog catalog;
    public Animator targetAnimator;         //에니메이터 기반이면 설정
    public SpriteRenderer targetSprite;     //스프라이트 기반이면 설정

    void OnEnable()
    {
        ApplyNow();
        GameSignals.OnSkinEquipped += OnSkinEquipped;
    }


    void OnDisable()
    {
        GameSignals.OnSkinEquipped -= OnSkinEquipped;
    }


    void OnSkinEquipped(string _) => ApplyNow();


    public void ApplyNow()
    {
        if (catalog == null)
        {
            if(SkinCatalog.Instance == null)
            {
                Debug.Log("SkinCatalog 인스턴스 null");
                return;
            }
            catalog = SkinCatalog.Instance;
        }
            
        if(GameDataManager.Instance == null)
        {
            Debug.LogError("GameDataManager인스턴스 null");
            return;
        }
        string id = GameDataManager.Instance.GetEquipped();
        var cfg = catalog ? catalog.Get(id) : null;
        if (cfg == null) return;


        if (targetAnimator)
        {
            if (cfg.animatorOverride)
                targetAnimator.runtimeAnimatorController = cfg.animatorOverride;
            else if (cfg.animatorController)
                targetAnimator.runtimeAnimatorController = cfg.animatorController;
        }
        if (targetSprite && cfg.singleSprite)
            targetSprite.sprite = cfg.singleSprite;
    }
}

