using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ISkill
{
    void UseSkill(GamePlayer player);

    
}



public class OneSkill : ISkill
{
    public void UseSkill(GamePlayer player)
    {
        Debug.Log("SkillOne");

        player.StartCoroutine(player.SizeChange(1.5f, 3f));
        player.StartCoroutine(player.SpeedBoost(1.1f, 3f));




    }
    
    
}
public class TwoSkill : ISkill
{
    public void UseSkill(GamePlayer player)
    {
        if (player.Hp >= player.characterData.maxHp)
        {
            Debug.Log("최대체력입니다.");
            return;
        }
        player.HpHeal(1);
        Debug.Log("SkillTwo");
        
    }
}
public class ThreeSkill : ISkill
{
    public void UseSkill(GamePlayer player)
    {
        Debug.Log("스피드 스킬 사용!");
        player.StartCoroutine(player.SpeedBoost(1.2f, 3f));
        player.StartCoroutine(player.IgnoreWall(3.5f));
        
        Debug.Log("SkillThree");
    }
}
public enum Skills
{
    One,
    Two,
    Three,
}

