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

    public void SetCharacter(CharacterData data)
    {
        if (data == null || data.animatorController == null)
        {
            Debug.LogWarning("Animator Controller가 비어 있습니다: " + data?.name);
            return;
        }
        var characterType = data.characterType;
        switch (characterType)
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
}