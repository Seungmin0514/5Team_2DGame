using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("UI References")]
    public Slider hpSlider;              // 체력바 슬라이더
    public RectTransform dividerParent;  // 분할선 부모
    public GameObject dividerPrefab;     // 분할선 프리팹

    private List<GameObject> dividers = new List<GameObject>();
    private GamePlayer player;

    private int maxHP;
    private int currentHP;

    void Start()
    {
        player = FindObjectOfType<GamePlayer>();

        if (player == null)
        {
            Debug.LogWarning("GamePlayer를 찾을 수 없습니다.");
            return;
        }

        maxHP = player.Hp;
        currentHP = player.Hp;

        SetupHPBar(maxHP);
    }

    void Update()
    {
        if (player == null) return;

        if (currentHP != player.Hp)
        {
            currentHP = player.Hp;
            UpdateHP(currentHP);
        }
    }

    private void SetupHPBar(int newMaxHP)
    {
        hpSlider.maxValue = newMaxHP;
        hpSlider.value = newMaxHP;

        // 기존 분할선 제거
        foreach (var div in dividers)
            Destroy(div);
        dividers.Clear();

        // 분할선 새로 생성
        for (int i = 1; i < newMaxHP; i++)
        {
            GameObject divider = Instantiate(dividerPrefab, dividerParent);
            dividers.Add(divider);

            RectTransform rt = divider.GetComponent<RectTransform>();
            float normalizedPos = (float)i / newMaxHP;
            rt.anchorMin = new Vector2(normalizedPos, 0);
            rt.anchorMax = new Vector2(normalizedPos, 1);
            rt.anchoredPosition = Vector2.zero;
        }
    }

    private void UpdateHP(int hp)
    {
        hpSlider.value = hp;

        for (int i = 0; i < dividers.Count; i++)
        {
            if (i >= hp)
                dividers[i].SetActive(false);
            else
                dividers[i].SetActive(true);
        }
    }
}
