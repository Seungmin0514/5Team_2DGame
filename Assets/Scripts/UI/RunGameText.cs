using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RunGameText : MonoBehaviour
{
    public TextMeshProUGUI goldTxt;
    private int gold;
    void Start()
    {
        gold = (int)RunGameManager.Instance.gold;
    }

    void Update()
    {
        goldTxt.text = gold.ToString();
    }
}
