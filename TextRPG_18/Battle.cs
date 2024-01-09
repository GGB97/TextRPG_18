using System;

public class Battle
{
    


    public void BattleStatus(Dungeon dungeon)
    {
        Console.Write("[ Lv.");
        ConsoleManager.RedColor(dungeon.monsters[0].GetLv());
        Console.WriteLine(" " + dungeon.monsters[0].GetName() + " HP " + dungeon.monsters[0].hp.ToString() + " ");
        Console.Write("[ Lv.");
        ConsoleManager.RedColor(dungeon.monsters[1].GetLv());
        Console.WriteLine(" " + dungeon.monsters[1].GetName() + " HP " + dungeon.monsters[1].hp.ToString() + " ");
        Console.Write("[ Lv.");
        ConsoleManager.RedColor(dungeon.monsters[2].GetLv());
        Console.WriteLine(" " + dungeon.monsters[2].GetName() + " HP " + dungeon.monsters[2].hp.ToString() + " ");
        Console.WriteLine();

        Console.WriteLine("[내 정보]");


    }
}
