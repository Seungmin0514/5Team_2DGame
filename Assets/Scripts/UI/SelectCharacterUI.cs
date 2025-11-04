using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SelectCharacterUI : MonoBehaviour
{
    public Animator previewAnimator;
    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI hpTxt;
    public TextMeshProUGUI spdTxt;
    public TextMeshProUGUI skillNameTxt;

    private CharacterData currentCharacterData;
    private AnimationClipChanger animationClipChanger;
    private CharacterType selectedType;
    private void Start()
    {
        if (animationClipChanger == null)
            animationClipChanger = FindObjectOfType<AnimationClipChanger>();
    }
    public void SetCharacter(CharacterData data)
    {
        if (data == null || data.animatorController == null)
        {
            Debug.LogWarning("Animator Controller가 비어 있습니다: " + data?.name);
            return;
        }
        currentCharacterData = data;
        switch (data.characterType)
        {
            case CharacterType.One:
                previewAnimator.SetInteger("CharNum", 0);
                nameTxt.text = "Owlet";
                selectedType = CharacterType.One;
                break;
            case CharacterType.Two:
                previewAnimator.SetInteger("CharNum", 1);
                nameTxt.text = "Pink Bunny";
                selectedType = CharacterType.Two;
                break;
            case CharacterType.Three:
                previewAnimator.SetInteger("CharNum", 2);
                nameTxt.text = "Blue Dude";
                selectedType = CharacterType.Three;
                break;
        }
        hpTxt.text = data.maxHp.ToString();
        spdTxt.text = data.speed.ToString();
        skillNameTxt.text = data.skillName;
    }
    public void ConfirmSelection()
    {
        switch (selectedType)
        {
            case CharacterType.One:
                animationClipChanger.SwitchToTypeB();
                break;
            case CharacterType.Two:
                animationClipChanger.SwitchToTypeA();
                break;
            case CharacterType.Three:
                animationClipChanger.SwitchToTypeC();
                break;
            default:
                Debug.LogWarning("선택된 캐릭터가 없습니다.");
                break;
        }
        GameDataManager.Instance.selectedCharacter = currentCharacterData.characterType;
    }
}