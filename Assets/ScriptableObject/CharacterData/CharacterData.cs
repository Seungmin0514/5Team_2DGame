using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;


[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Game/CharacterData")]
public class CharacterData : ScriptableObject
{
    public CharacterType characterType;
    public Sprite portrait;
    public int maxHp;
    public float jumpForce;
    public float doubleJumpForce;
    public float speed;
    public RuntimeAnimatorController animatorController;
    public Skills skills;
    public string skillName;
    public float cooldown;


}