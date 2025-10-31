using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectSpriteLoop : MonoBehaviour
{
    public Animator previewAnimator; // Animator가 붙은 Image 오브젝트

    private RuntimeAnimatorController currentController;

    public void SetCharacter(CharacterData data)
    {
        if (data == null || data.animatorController == null)
        {
            Debug.LogWarning("Animator Controller가 비어 있습니다: " + data?.name);
            return;
        }

        // 애니메이터 교체
        previewAnimator.runtimeAnimatorController = data.animatorController;
        currentController = data.animatorController;

        // 첫 번째 상태부터 재생
        var firstState = previewAnimator.GetCurrentAnimatorStateInfo(0);
        previewAnimator.Play(firstState.fullPathHash, 0, 0f);
    }
}