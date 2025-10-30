using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerControl : MonoBehaviour
{
    
     GamePlayer player;


    private void Awake()
    {
        player = GetComponent<GamePlayer>();
    }

    private void OnJump()
    {
        player.Jump();
    }
    private void OnSkill()
    {
        player.UseSkill();
    }
}
