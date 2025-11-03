using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class GameActoinManager : MonoBehaviour
{
    public GameObject scanObject;
    public TalkManager talkManager;
    public int talkIndex;
    public Text talkText;
    public GameObject talkPanel;
    public Image portraitImg;
    public bool isAction;
    
    public void Action(GameObject scanObj)
    {
        isAction = true;
        scanObject = scanObj;
        ObjectData objData = scanObj.GetComponent<ObjectData>();
        if(objData.id == 200)
        {   
            
            LoadSceneManager.Instance.EnterGame();
            
            return;
        }
        Talk(objData.id, objData.isNpc);
        talkPanel.SetActive(isAction);
    }
    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }
        if (isNpc)
        {
            talkText.text = talkData.Split(':')[0];
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(":")[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 0);
        }
        isAction = true;
        talkIndex++;
    }
}
