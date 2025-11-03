using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    [Header("UI References")]
    public Image skillIcon;
    public Image cooldownOverlay;
    public List<CharacterData> characterData;

    private float coolTime;
    private float maxCoolTime;
    private Coroutine cooltimeRoutine;

    private void Start()
    {
        if (GameDataManager.Instance == null)
        {
            Debug.LogWarning("GameDataManager 인스턴스가 없습니다.");
            return;
        }
        var type = GameDataManager.Instance.selectedCharacter;
        Debug.Log("캐릭터 타입: " + type);
        switch (type)
        {
            case CharacterType.One:
                skillIcon.sprite = characterData[0].skillIcon;
                cooldownOverlay.sprite = characterData[0].skillIconDisabled;
                break;
            case CharacterType.Two:
                skillIcon.sprite = characterData[1].skillIcon;
                cooldownOverlay.sprite = characterData[1].skillIconDisabled;
                break;
            case CharacterType.Three:
                skillIcon.sprite = characterData[2].skillIcon;
                cooldownOverlay.sprite = characterData[2].skillIconDisabled;
                break;
            default:
                Debug.LogWarning("알 수 없는 캐릭터 타입");
                break;
        }
    }
    public void StartCooltime(float duration)
    {
        if (cooltimeRoutine != null)
            StopCoroutine(cooltimeRoutine);

        maxCoolTime = duration;
        coolTime = duration;
        cooldownOverlay.fillAmount = 1f;

        cooltimeRoutine = StartCoroutine(CooltimeRoutine());
    }

    private IEnumerator CooltimeRoutine()
    {
        while (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
            cooldownOverlay.fillAmount = coolTime / maxCoolTime;
            yield return null;
        }

        coolTime = 0;
        cooldownOverlay.fillAmount = 0f;
        cooltimeRoutine = null;
    }
}
