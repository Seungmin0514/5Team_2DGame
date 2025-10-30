using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int Gold { get; private set; }

    
    private void Awake()
    {
        
        if(Instance != null){
            Destroy(this);
            return;
        }
        
            Instance = this;


    }
    
    public void AddGold(int amonut)
    {
        Gold += amonut;
    }
    public void RemoveGold(int amonut)
    {
        Gold -= amonut;
    }
    public bool Canbuy(int amonut)
    {
        if (Gold>=amonut) return true;
        return false;
    }

}
