using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "QuestDB", menuName = "Game/QuestDB")]
public class QuestDB : ScriptableObject
{
    public List<QuestDef> quests = new List<QuestDef>();

    public QuestDef Get(string id)
    {
        return quests.Find(q => q !=null && q.questId == id);
    }
}
