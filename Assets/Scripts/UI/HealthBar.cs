using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider hpSlider; //체력바 bg
    public RectTransform dividerParent; //분할선 부모
    public GameObject divider; //분할선 프리팹

    public List<CharacterData> characterData;
    public int maxHP = 5;
    public int currentHP = 5;

    void Start()
    {
        //뭘 만들어야할까...
        //
    }

}
