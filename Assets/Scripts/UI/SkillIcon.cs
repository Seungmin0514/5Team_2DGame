using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SkillIcon : MonoBehaviour
{
    [Header("UI References")]
    public Image skillIcon;
    public Image cooldownOverlay;

    private float coolTime;
    private float maxCoolTime;
    private Coroutine cooltimeRoutine;

    private void Start()
    {
        var type = GameDataManager.Instance.selectedCharacter;
        Debug.Log("캐릭터 타입: " + type);
        switch (type)
        {
            case CharacterType.One:
                //skillIcon.sprite = ;
                //cooldownOverlay.sprite = ;
                break;
            case CharacterType.Two:
                //skillIcon.sprite = ;
                //cooldownOverlay.sprite = ;
                break;
            case CharacterType.Three:
                //skillIcon.sprite = ;
                //cooldownOverlay.sprite = ;
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
