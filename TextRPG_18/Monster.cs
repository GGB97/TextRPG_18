using System;

public class Monster
{   

    public string Name { get; set; }
    public int Level { get; set; }
    public int HP { get; set; }
    public int Atk { get; set; }

    public bool IsDefeated { get; set; }
    public Monster(string name, int level, int hp, int atk, bool isDefeated)
    {
        Name = name;
        Level = level;
        HP = hp;
        Atk = atk;
        IsDefeated = isDefeated;
    }

    public void Attack()
    {
        

    }


}

