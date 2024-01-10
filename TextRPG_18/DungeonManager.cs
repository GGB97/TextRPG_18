using System;
using TextRPG;



// 추가 해야할 클래스 몬스터




public class DungeonManager
{
    List<Monster> monster;

    Dungeon dungeon = new Dungeon();
    public void Select(Player player)
    { 
        dungeon.DungeonTitle(player); // 던전 화면

        string str;
        str = Console.ReadLine();
        while (true)
        {
            if (str == "1")
            {
                Console.Clear();
                dungeon.PickBattle(player);

                break;
            }

            else
            {
               // Console.Write($"{str} 은(는) 잘못된 입력입니다.");

                break;
            }
        }
    }
}
