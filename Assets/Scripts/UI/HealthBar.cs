using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider hpSlider; //체력바 bg
    public RectTransform dividerParent; //분할선 부모
    public GameObject dividerPrefab; //분할선 프리팹
    private List<GameObject> dividers = new List<GameObject>();

    public List<CharacterData> characterData;
    public int maxHP = 5;
    public int currentHP = 5;
    private void Awake()
    {
        
    }
    void Start()
    {
        SetupHPBar(maxHP);
    }

    public void SetupHPBar(int newMaxHP)
    {
        maxHP = newMaxHP;
        currentHP = newMaxHP;
        hpSlider.maxValue = maxHP;
        hpSlider.value = currentHP;

        // 기존 분할선 제거
        foreach (var div in dividers)
            Destroy(div);
        dividers.Clear();

        // 분할선 생성
        for (int i = 1; i < maxHP; i++)
        {
            GameObject divider = Instantiate(dividerPrefab, dividerParent);
            dividers.Add(divider);

            // 각 분할선의 위치 계산 (Fill 영역을 0~1로 봤을 때)
            RectTransform rt = divider.GetComponent<RectTransform>();
            float normalizedPos = (float)i / maxHP;
            rt.anchorMin = new Vector2(normalizedPos, 0);
            rt.anchorMax = new Vector2(normalizedPos, 1);
            rt.anchoredPosition = Vector2.zero;
        }
    }
    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Max(0, currentHP - damage);
        UpdateHP(currentHP);
    }

    public void Heal(int amount)
    {
        currentHP = Mathf.Min(maxHP, currentHP + amount);
        UpdateHP(currentHP);
    }

    private void UpdateHP(int hp)
    {
        hpSlider.value = hp;
    }
}
