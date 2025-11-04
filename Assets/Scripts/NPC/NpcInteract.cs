using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class NpcInteract : MonoBehaviour
{
    public enum Role 
    {
        Shop,
        SkinSelect
    }  // NPC 역할 구분

    [Header("역할 설정")]
    public Role role = Role.Shop;          // 이 NPC가 상점인지 선택창인지 지정

    [Header("UI 연결")]
    public GameObject promptUI;            // E: 상호작용 표시용 오브젝트

    [Header("메뉴 연결")]
    public ShopManager shopManager;        // role == Shop일 때 연결
    public SkinSelectManager skinSelectManager; // role == SkinSelect일 때 연결

    bool isNear = false;

    void Reset()
    {
        // 콜라이더 자동 트리거화
        var col = GetComponent<Collider2D>();
        if (col) col.isTrigger = true;
    }

    void Update()
    {
        if (!isNear) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (promptUI) promptUI.SetActive(false);
            OpenMenu();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNear = true;
            if (promptUI) promptUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNear = false;
            if (promptUI) promptUI.SetActive(false);
        }
    }

    void OpenMenu()
    {
        switch (role)
        {
            case Role.Shop:
                if (shopManager) 
                    shopManager.OpenShop();
                else Debug.LogWarning("[NpcInteract] shopManager not assigned");
                break;

            case Role.SkinSelect:
                if (skinSelectManager) 
                    skinSelectManager.Open();
                break;
        }
    }
}