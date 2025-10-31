using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerAnimationControl : MonoBehaviour
{

    [SerializeField]private Animator PlayerAnimator;
    [SerializeField]private Animator PlayerEffectAnimator;


    private void Awake()
    {
         
    }
    public void InitAnimator(RuntimeAnimatorController runtimeAnimatorController) 
    {
        PlayerAnimator.runtimeAnimatorController = runtimeAnimatorController;
    }
    public void JumpAnimation()
    {
        PlayerAnimator.SetBool("IsJump", true);
    }
    public void EndJumpAnimation()
    {
        PlayerAnimator.SetBool("IsJump", false);
    }
    public void DoubleJumpEffect()
    {
        PlayerEffectAnimator.SetTrigger("IsDoubleJump");
    }
    public void DamagedAnimation()
    {
        PlayerAnimator.SetTrigger("Damaged");
    }
    public void DieAnimation()
    {
        PlayerAnimator.SetBool("Die",true);
    }
    public void UseSkillAnimation()
    {
        PlayerAnimator.SetTrigger("UseSkill");
    }
    public void UseSlideAnimation()
    {
        PlayerAnimator.SetBool("IsSlide", true);
    }
    public void EndSlideAnimation()
    {
        PlayerAnimator.SetBool("IsSlide",false  );
    }
}
