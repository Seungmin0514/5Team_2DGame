using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectSpriteLoop : MonoBehaviour
{
    public Animator previewAnimator;
    

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
                break;
            case CharacterType.Two:
                previewAnimator.SetInteger("CharNum", 1);
                break;
            case CharacterType.Three:
                previewAnimator.SetInteger("CharNum", 2);
                break;
        }
    }
}