using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ISkill
{
    void UseSkill(GameObject player);
}

public class OneSkill : ISkill
{
    public void UseSkill(GameObject player)
    {
        Debug.Log("SkillOne");
    }
}
public class TwoSkill : ISkill
{
    public void UseSkill(GameObject player)
    {
        Debug.Log("SkillTwo");
    }
}
public class ThreeSkill : ISkill
{
    public void UseSkill(GameObject player)
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

