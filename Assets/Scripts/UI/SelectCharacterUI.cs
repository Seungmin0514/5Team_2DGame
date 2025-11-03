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

                break;
            case CharacterType.Two:
                previewAnimator.SetInteger("CharNum", 1);
                nameTxt.text = "Pink Bunny";
                break;
            case CharacterType.Three:
                previewAnimator.SetInteger("CharNum", 2);
                nameTxt.text = "Blue Dude";
                break;
        }
        hpTxt.text = data.maxHp.ToString();
        spdTxt.text = data.speed.ToString();
        skillNameTxt.text = data.skillName;
    }
    public void ConfirmSelection()
    {
        if (currentCharacterData == null)
        {
            Debug.LogWarning("선택된 캐릭터가 없습니다!");
            return;
        }
        GameDataManager.Instance.selectedCharacter = currentCharacterData.characterType;
    }
}