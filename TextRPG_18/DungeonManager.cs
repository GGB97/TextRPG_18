﻿using System;
using TextRPG;



// 추가 해야할 클래스 몬스터




public class DungeonManager
{
    public void Select(Player player)
    {
        string str;

        Console.WriteLine("[던전 선택] --- (0. 나가기)");

        while (true)
        {
            Console.WriteLine(
                "1. 난이도 1 (방어력 8 이상 권장) \n" +
                "2. 난이도 2 (방어력 10 이상 권장) \n" +
                "3. 난이도 3 (방어력 20 이상 권장) \n"
                );
            Console.Write($"{player.name} : ");
            str = Console.ReadLine();

            if (str == "1" || str == "2" || str == "3")
            {
                Enter(player, int.Parse(str));
                //break;
            }
            else if (str == "0")
            {
                break;
            }
            else
            {
                Console.Write($"{str} 은(는) 잘못된 입력입니다.");
            }
        }
    }

    public void Enter(Player player, int level)
    {

        Console.Clear();
        Console.WriteLine($"난이도 {level} 던전에 입장합니다.\n");
        Dungeon dungeon = new Dungeon(level);


        dungeon.BattleSetting(player);
    }


}
