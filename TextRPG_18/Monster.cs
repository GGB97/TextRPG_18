using System;

public class Monster
{
    string name;
    int level;
    public int hp;

    public Monster(string name, int level, int hp)
    {
        this.name = name;
        this.level = level;
        this.hp = hp;
    }

    public void attack(Player player)
    {
        ConsoleManager.YellowColor("Battle!!");
        Console.WriteLine();
        Console.WriteLine();

        Console.WriteLine("Lv. {0} {1}의 공격!",level,name);
        Console.WriteLine("{0} 을(를) 맞췄습니다 ",player.name);

    }

    public int GetLv()
    {
        return level;
    }

    public string GetName()
    {
        return name;
    }

    public bool IsDie() 
    {
        if (hp <= 0) return true;
        else return false;
    }
}
