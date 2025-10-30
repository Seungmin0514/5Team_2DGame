using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestItemSlot : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text descText;
    public TMP_Text progressText;
    public TMP_Text rewardText;
    public Button claimButton;

    QuestDef def;
    QuestManager mgr;

    public void Setup(QuestDef def, QuestManager mgr)
    {
        this.def = def;
        this.mgr = mgr;
        Refresh();
        claimButton.onClick.RemoveAllListeners();
        claimButton.onClick.AddListener(() => {
            mgr.ClaimReward(def.questId);
        });
    }

    void Refresh()
    {
        int cur = mgr.GetProgress(def.questId);
        bool claimed = mgr.IsClaimed(def.questId);
        bool canClaim = !claimed && cur >= def.target;

        if (titleText) titleText.text = def.title;
        if (descText) descText.text = def.description;
        if (progressText) progressText.text = $"{cur} / {def.target}";
        if (rewardText) rewardText.text = $"Reward: {def.rewardCoins} Coins";
        if (claimButton) claimButton.interactable = canClaim;
    }
}
