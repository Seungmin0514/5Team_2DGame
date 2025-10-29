using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [Header("Quest Data")]
    public string questDesc = "Collect 50 coins";
    public int current = 10;
    public int target = 50;
    public int rewardCoins = 100;
    public bool rewardClaimed = false;

    [Header("UI Refs")]
    public Text questDescText;
    public Text progressText;
    public Text rewardText;
    public Button claimButton;

    void Awake()
    {
        instance = this;    
    }

    public void RefreshQuestUI()
    {
        questDescText.text = questDesc;
        progressText.text = current + " / " + target;
        rewardText.text = "Reward: " + rewardCoins + " Cois";

        bool canClaim = (!rewardClaimed && current >= target);
        claimButton.interactable = canClaim;
    }

    public void ClaimReward()
    {
        bool canClaim = (!rewardClaimed && current >= target);
        if (canClaim)
        {
            GameDataManager.Instance.coins += rewardCoins;
            rewardClaimed = true;
            RefreshQuestUI();
        }
    }
}
