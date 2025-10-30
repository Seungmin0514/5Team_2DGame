using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("DB & 활성 퀘스트")]
    public QuestDB questDB;
    public List<string> activeQuestIds = new List<string>(); // 오늘 표시할 퀘스트 id들

    [Header("UI")]
    public Transform questListParent;     // VerticalLayoutGroup
    public GameObject questItemPrefab;    // QuestItemSlot 프리팹

    // 진행도 저장 (questId -> current)
    Dictionary<string, int> progress = new Dictionary<string, int>();
    // 보상 수령 여부
    HashSet<string> claimed = new HashSet<string>();

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        GameEvents.OnCoinsGained += OnCoinsGained;
        GameEvents.OnItemBought += OnItemBought;
    }

    void OnDisable()
    {
        GameEvents.OnCoinsGained -= OnCoinsGained;
        GameEvents.OnItemBought -= OnItemBought;
    }

    public int GetProgress(string questId)
    {
        return progress.TryGetValue(questId, out var v) ? v : 0;
    }

    public void AddProgress(string questId, int delta)
    {
        if (!progress.ContainsKey(questId)) progress[questId] = 0;
        progress[questId] = Mathf.Max(0, progress[questId] + delta);
        RefreshQuestUI(); //UI 즉시반영
    }

    public bool IsClaimed(string questId) => claimed.Contains(questId);

    public void ClaimReward(string questId)
    {
        var def = questDB.Get(questId);
        if (def == null) return;

        int cur = GetProgress(questId);
        if (cur < def.target || IsClaimed(questId)) return;

        GameDataManager.Instance.coins += def.rewardCoins;
        claimed.Add(questId);

        RefreshQuestUI();
        ShopManager.Instance?.RefreshShopUI(); //코인 증가반영
    }

    public void RefreshQuestUI()
    {
        foreach (Transform child in questListParent) Destroy(child.gameObject);

        foreach (var id in activeQuestIds)
        {
            var def = questDB.Get(id);
            if (def == null) continue;

            var go = Instantiate(questItemPrefab, questListParent);
            var slot = go.GetComponent<QuestItemSlot>();
            slot.Setup(def, this);
        }
    }

    // 이벤트로부터 진행도 누적
    void OnCoinsGained(int amount)
    {
        foreach (var id in activeQuestIds)
        {
            var def = questDB.Get(id);
            if (def == null) continue;

            if (def.type == QuestType.CollectCoins)
            {
                AddProgress(id, amount);
            }
        }
    }

    void OnItemBought(string itemId)
    {
        foreach (var id in activeQuestIds)
        {
            var def = questDB.Get(id);
            if (def == null) continue;

            if (def.type == QuestType.BuyItems)
            {
                AddProgress(id, 1);
            }
        }
    }
}
