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
        player.StartCoroutine(player.SpeedBoost(1.5f, 3f));
        // 1.5배 속도로 3초 동안 유지
        Debug.Log("SkillThree");
    }
}
public enum Skills
{
    One,
    Two,
    Three,
}

