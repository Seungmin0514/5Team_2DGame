using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationClipChanger : MonoBehaviour
{
    private static class OriginalClipNames
    {
        public const string IDLE = "Player_Idle";
        public const string WALK = "PlayerWalk";
        public const string JUMP = "Player_Jump";
    }
    [System.Serializable]
    public struct PlayerClipSet
    {
        public AnimationClip idleClip;
        public AnimationClip walkClip;
        public AnimationClip jumpClip;
    }

    public PlayerClipSet CharacterTypeA;
    public PlayerClipSet CharacterTypeB;
    public PlayerClipSet CharacterTypeC;


    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;

    //void Start()
    //{
    //    switch (GameDataManager.Instance.selectedCharacter)
    //    {
    //        case CharacterType.One:
    //            SwitchToTypeB();
    //            break;
    //        case CharacterType.Two:
    //            SwitchToTypeA();
    //            break;
    //        case CharacterType.Three:
    //            SwitchToTypeC();
    //            break;
    //        default:
    //            SwitchToTypeA();
    //            break;
    //    }
    //}
    //void Awake()
    //{
    //    animator = GetComponent<Animator>();
    //    animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
    //    animator.runtimeAnimatorController = animatorOverrideController;
    //}

    //public void ChangeClip(string originalClipName, AnimationClip overrideClip)
    //{
    //    animatorOverrideController[originalClipName] = overrideClip;
    //    Debug.Log($"애니메이션 클립 '{originalClipName}'이 새클립 '{overrideClip}'으로 변경");
    //}
    //// Start is called before the first frame update
    //public void SwitchToSet(PlayerClipSet clipSet)
    //{
    //    ChangeClip(OriginalClipNames.IDLE, clipSet.idleClip);
    //    ChangeClip(OriginalClipNames.JUMP, clipSet.jumpClip);
    //    ChangeClip(OriginalClipNames.WALK, clipSet.walkClip);
    //}

    //public void SwitchToTypeA()
    //{
    //    GameDataManager.Instance.selectedCharacter = CharacterType.Two;
    //    SwitchToSet(CharacterTypeA);
    //}
    //public void SwitchToTypeB()
    //{
    //    GameDataManager.Instance.selectedCharacter = CharacterType.One;
    //    SwitchToSet(CharacterTypeB);
    //}
    //public void SwitchToTypeC()
    //{
    //    GameDataManager.Instance.selectedCharacter = CharacterType.Three;
    //    SwitchToSet(CharacterTypeC);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) SwitchToTypeA();
    //    if (Input.GetKeyDown(KeyCode.Alpha2)||Input.GetKeyDown(KeyCode.Keypad2)) SwitchToTypeB();
    //    if( Input.GetKeyDown(KeyCode.Alpha3)||Input.GetKeyDown(KeyCode.Keypad3)) SwitchToTypeC();
    //}
}
