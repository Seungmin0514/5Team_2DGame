using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageUIManager : MonoBehaviour
{
    public static VillageUIManager Instance;

    public GameObject shopPanel;
    public GameObject inventoryPanel;
    public GameObject questPanel;

    //public PlayerVillageController playerController;

    void Awake()
    {
        Instance = this;
    }

    void LockPlayer(bool locked)
    {
        //if (playerController != null)
        //    playerController.enabled = !locked;
    }

    //public void OpenShop()
    //{
    //    shopPanel.SetActive(true);
    //    LockPlayer(true);

    //    if (ShopManager.Instance != null)
    //        ShopManager.Instance.RefreshShopUI();
    //}

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        LockPlayer(false);
    }

    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        LockPlayer(true);

        if (InventoryUI.Instance != null)
            InventoryUI.Instance.RefreshInventory();
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        LockPlayer(false);
    }

    //public void OpenQuest()
    //{
    //    questPanel.SetActive(true);
    //    LockPlayer(true);

    //    if (QuestManager.Instance != null)
    //        QuestManager.Instance.RefreshQuestUI();
    //}

    public void CloseQuest()
    {
        questPanel.SetActive(false);
        LockPlayer(false);
    }
}
