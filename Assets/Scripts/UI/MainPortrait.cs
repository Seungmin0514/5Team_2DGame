using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPortrait : MonoBehaviour
{
    public Image portraitImage;
    //GameCharacterManager characterManager;
    public List<CharacterData> characterData;

    void Start()
    {
        //characterManager = GameCharacterManager.characterManager;
        UpdatePortrait();
    }

    public void UpdatePortrait()
    {
        var type = GameCharacterManager.characterManager.characterType;
        Debug.Log("캐릭터 타입: " + type);
        switch (type)
        {
            case CharacterType.One:
                portraitImage.sprite = characterData[0].portrait;
                break;
            case CharacterType.Two:
                portraitImage.sprite = characterData[1].portrait;
                break;
            case CharacterType.Three:
                portraitImage.sprite = characterData[2].portrait;
                break;
        }
    }
}
