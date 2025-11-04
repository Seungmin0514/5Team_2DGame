using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunGameText : MonoBehaviour
{
    public TextMeshProUGUI goldTxt;
    private int gold;
    void Update()
    {
        int currentGold = (int)RunGameManager.Instance.gold;

        // 값이 바뀌었을 때만 UI 갱신
        if (currentGold != gold)
        {
            gold = currentGold;
            goldTxt.text = gold.ToString();
        }
    }
}
