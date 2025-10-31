using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;


public enum CharacterType
{
    One,
    Two,
    Three,
}

public class GameCharacterManager : MonoBehaviour
{
    public static GameCharacterManager characterManager;
    public CharacterType characterType { get; private set; }
    [SerializeField] private List<CharacterData> characterData;
    [SerializeField] private GamePlayer player;

    private void Awake()
    {
        if (characterManager != null)
        {
            Destroy(this);
            return;
        }
        characterManager = this;

    }
    private void Start()
    {

        SetCharacter(CharacterType.One);
    }

    public void SetCharacter(CharacterType characterType)
    {
        foreach (CharacterData data in characterData)
        {
            if (data.characterType == characterType)
            {
                player.PlayerInit(data);
                
            }
        }
    }
}
