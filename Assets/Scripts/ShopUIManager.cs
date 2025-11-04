using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopUIManager : MonoBehaviour
{
    public Image characterAPanel;
    public Image characterBPanel;
    public Image entirePanel;

    public Image characterAImg;
    public Image characterBImg;

    private GameActoinManager gameActoinManager;
    public GameObject selectUI;

    void Awake()
    {
        gameActoinManager = FindAnyObjectByType<GameActoinManager>();
        if(gameActoinManager == null)
        {
            Debug.LogError("GameActionManager를 찾을 수 없습니다.");
        }
    }
    public void UpdatePurchaseStatusUI()
    {
        if(GameDataManager.Instance == null)
        {
            Debug.LogError("GameDataManager.Instance 초기화 안됨");
            return;
        }
        if (GameDataManager.Instance.isPurchasedA)
        {
            characterAImg.color = new Color(1f, 1f, 1f,1f);
        }
        else
        {
            characterAImg.color = new Color(63f / 255f, 63f / 255f, 63f / 255f,1f);
        }
        if (GameDataManager.Instance.isPurchasedB)
        {
            characterBImg.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            characterBImg.color = new Color(63f / 255f, 63f / 255f, 63f / 255f, 1f);
        }
    }
    public void ShowCharacterAPanel()
    {
        characterAPanel.gameObject.SetActive(true);
        UpdatePurchaseStatusUI();
    }
    public void ShowCharacterBPanel()
    {
        characterBPanel.gameObject.SetActive(true);
    }
    public void CloseEntirePanel()
    {
        characterAPanel.gameObject.SetActive(false);
        characterBPanel.gameObject.SetActive(false);
        entirePanel.gameObject.SetActive(false);
        if(gameActoinManager != null)
        {
            gameActoinManager.isAction = false;
        }
    }
    public void CloseCharacterAPanel()
    {
        characterAPanel.gameObject.SetActive(false);
    }
    public void CloseCharacterBPanel()
    {
        characterBPanel.gameObject.SetActive(false);
    }

    public void PurchaseA()
    {
        if (!GameDataManager.Instance.isPurchasedA && GameDataManager.Instance.GetCoins() >= 200)
        {
            Debug.Log($"CharacterA 구매 남은 코인 : {GameDataManager.Instance.GetCoins()}");
            GameDataManager.Instance.MinusCoins(200);
            GameDataManager.Instance.isPurchasedA = true;
            UpdatePurchaseStatusUI();
        }
        else if(GameDataManager.Instance.isPurchasedA)
        {
            Debug.Log("A 이미 구매함");
        }else if (GameDataManager.Instance.GetCoins() < 200)
        {
            Debug.Log($"A 살 코인 부족 남은 코인 : {GameDataManager.Instance.GetCoins()}");
        }
    }
    public void PurchaseB()
    {
        if (!GameDataManager.Instance.isPurchasedB && GameDataManager.Instance.GetCoins() >= 100)
        {
            Debug.Log($"CharacterB 구매 남은 코인 : {GameDataManager.Instance.GetCoins()}");
            GameDataManager.Instance.MinusCoins(100);
            GameDataManager.Instance.isPurchasedB = true;
            UpdatePurchaseStatusUI();
        }
        else if (GameDataManager.Instance.isPurchasedB)
        {
            Debug.Log("B 이미 구매함");
        }
        else if (GameDataManager.Instance.GetCoins() < 100)
        {
            Debug.Log($"B 살 코인 부족 남은 코인 : {GameDataManager.Instance.GetCoins()}");
        }
    }

    public void Setcoins()
    {
        GameDataManager.Instance.SetCoins(200);
    }

    public void CloseSelectUI()
    {
        selectUI.SetActive(false);
        if (gameActoinManager != null)
        {
            gameActoinManager.isAction = false;
        }
    } 
}
