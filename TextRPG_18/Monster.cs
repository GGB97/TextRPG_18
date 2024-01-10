using System;

public class Monster
{
    string name;
    int level;
    public int hp;
    public int atk;

    public Monster(string name, int level, int hp, int atk)
    {
        this.name = name;
        this.level = level;
        this.hp = hp;
        this.atk = atk;
    }

    public void attack(Player player)
    {

        ConsoleManager.YellowColor("Battle!!");
        Console.WriteLine();
        Console.WriteLine();

        Console.WriteLine("Lv. {0} {1}의 공격!", level, name);
        Console.WriteLine("{0} 을(를) 맞췄습니다   [데미지  :  {1}]", player.name, atk);
        Console.WriteLine();
        Console.WriteLine("Lv. {0}   {1}", player.getLevel(), player.name);
        Console.Write("HP ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("{0} -> {1} ", player.hp, (player.hp - atk));
        Console.ResetColor();
        Console.WriteLine();

        

        player.hp -= atk;



    }

    public int GetLv()
    {
        return level;
    }

    public string GetName()
    {
        return name;
    }
}
