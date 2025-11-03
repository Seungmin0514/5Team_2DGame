using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPortrait : MonoBehaviour
{
    public Image portraitImage;
    public List<CharacterData> characterData;

    void Start()
    {
        UpdatePortrait();
    }

    public void UpdatePortrait()
    {
        if (GameDataManager.Instance == null)
        {
            Debug.LogWarning("GameDataManager 인스턴스가 없습니다.");
            return;
        }
        if (portraitImage == null)
        {
            Debug.LogWarning("portraitImage가 연결되어 있지 않습니다.");
            return;
        }
        var type = GameDataManager.Instance.selectedCharacter;
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
            default:
                Debug.LogWarning("알 수 없는 캐릭터 타입");
                break;
        }
    }
}
