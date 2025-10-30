using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int ,Sprite> portraitData;
    public Sprite[] portraitArr;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int ,Sprite>();
        GenerateData();
    }
    void GenerateData()
    {
        talkData.Add(100, new string[] { "살거 아니면 시간 낭비하지마.:0"});//상점
        talkData.Add(200, new string[] { "게임으로 넘어가는 포탈입니다." });//얘는 표시 안됨(게임포탈)
        talkData.Add(300, new string[] { "캐릭터 바꿔볼래?:0", "아님 말고:1" });

        portraitData.Add(100 + 0, portraitArr[0]);
        portraitData.Add(300 + 0, portraitArr[1]);
        portraitData.Add(300 +1,portraitArr[2]);
    }
    public string GetTalk(int id, int talkIndex)
    {
        if(talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex];
        }
    }
    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }

}
