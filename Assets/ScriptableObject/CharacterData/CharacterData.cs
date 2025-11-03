using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Game/CharacterData")]
public class CharacterData : ScriptableObject
{
    public CharacterType characterType;
    public Sprite portrait;
    public int maxHp;
    public float jumpForce;
    public float doubleJumpForce;
    public float speed;
    public float gravity;
    public RuntimeAnimatorController animatorController;
    public Skills skills;
    public string skillName;
    public float cooldown;
    public Sprite skillIcon;
    public Sprite skillIconDisabled;

}