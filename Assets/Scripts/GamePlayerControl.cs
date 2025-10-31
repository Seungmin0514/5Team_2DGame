using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using static UnityEngine.InputSystem.InputAction;

public class GamePlayerControl : MonoBehaviour
{

    GamePlayer player;
    [SerializeField] private UnityEvent OnJumpEvent;

    private void Awake()
    {
        player = GetComponent<GamePlayer>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) // ´­·¶À» ¶§ ÇÑ ¹ø
        {
           player.Jump();
        
        }
        
    }
   public void OnSkill(InputAction.CallbackContext context)
    {
        if (context.performed) // ´­·¶À» ¶§ ÇÑ ¹ø
        {
            player.UseSkill();
            
        }
        
    }
    public void OnSlide(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        
        Debug.Log(value);
        if (value>0)
        {
            
            player.UseSlide();
        }
        else 
        {
            // ¶ÃÀ» ¶§
            player.EndSlide();
        }
    }
}
