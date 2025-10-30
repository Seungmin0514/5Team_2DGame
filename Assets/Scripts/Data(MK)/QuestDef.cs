using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    CollectCoins, 
    BuyItems
}
[CreateAssetMenu(fileName = "QuestDef", menuName = "Game/Quest")]

public class QuestDef : ScriptableObject
{
    public string questId;
    public string title;
    public QuestType type;
    public int target;
    public int rewardCoins;
    [TextArea] public string description; 
}
