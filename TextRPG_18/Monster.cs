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
        player.hp -= 5;
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
