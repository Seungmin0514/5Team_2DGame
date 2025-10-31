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
        Debug.Log("SkillTwo");
        
    }
}
public class ThreeSkill : ISkill
{
    public void UseSkill(GamePlayer player)
    {
        Debug.Log("SkillThree");
    }
}
public enum Skills
{
    One,
    Two,
    Three,
}

