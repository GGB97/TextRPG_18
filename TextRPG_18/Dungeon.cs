using System;
using System.Collections.Generic;

public class Dungeon
{
    int level;  //던전 난이도
    public int rSpec; // 권장 방어력
    int rGold; // 보상 골드
    int rExp;  // 보상 경험치
    bool Isrun = false;

    // 몬스터 관리
    List<Monster> monsters;
    //램덤으로 나타나는 몬스터
    List<Monster> MonsterRend;
    public Dungeon(int level)
    {
        this.level = level;
        if (level == 1)
        {
            rSpec = 5;
            rGold = 1000;
            rExp = 10;
        }
        else if (level == 2)
        {
            rSpec = 10;
            rGold = 1700;
            rExp = 25;
        }
        else if (level == 3)
        {
            rSpec = 20;
            rGold = 2500;
            rExp = 40;
        }

        monsters = new List<Monster>();

        monsters.Add(new Monster("미니언", 2, 15));
        monsters.Add(new Monster("대포", 5, 25));
        monsters.Add(new Monster("공허충", 3, 10));
    }

    public void Battle(Player player)
    {
        Console.Clear();
        MonsterRend = new List<Monster>();
        RandomMobster(MonsterRend);  //몬스터 설정
        


        do
        {
            BattleStatus(player);

            Console.WriteLine("0. 취소");
            Console.WriteLine();
            Console.WriteLine("대상을 선택해주세요.");
            Console.WriteLine();
        }
        while (Isrun);
        {
            //if() 플레이어가 지거나 클리어할 경우 Isrun = true;


            switch (Console.ReadLine())//여기까지 함
            {
                case "0":
                    player.Attack(MonsterRend[0]);
                    break;
                case "1":
                    player.Attack(MonsterRend[0]);
                    break;
                case "2":
                    player.Attack(MonsterRend[0]);
                    break;
                case "3":
                    player.Attack(MonsterRend[0]);
                    break;
                default:
                    ConsoleManager.RedColor("잘 못된 입력입니다 ");
                    Console.WriteLine();
                    break;
            }


        }

    }

    public void BattleStatus(Player player)
    {

        ConsoleManager.YellowColor("Battle!!");
        Console.WriteLine();
        Console.WriteLine();

        for (int i = 0; i < MonsterRend.Count; i++) 
        {
            Console.Write(i + 1.ToString() + ". [ Lv.");
            ConsoleManager.RedColor(MonsterRend[i].GetLv().ToString());
            Console.WriteLine(" " + MonsterRend[i].GetName() + " HP " + MonsterRend[i].hp.ToString() + " ]");
        }

        Console.WriteLine();

        Console.WriteLine("[내 정보]");
        Console.Write("3. Lv.");
        ConsoleManager.RedColor(player.getLevel().ToString());
        Console.WriteLine(" Char ({0})", player.getJob());
        Console.Write("HP ");
        Console.WriteLine("{0}/{1}", player.hp, player.maxhp);

    }

    void RandomMobster(List<Monster> MonsterRend)
    {
        //생성되는 몬스터 수
        Random rend = new Random();
        int MonsterNum = rend.Next(2, 5);

        //랜덤 몬스터 생성
        
        for (int i = 0; i < MonsterNum; i++)
        {
            Random r = new Random();

            MonsterRend.Add(monsters[r.Next(0, 3)]);

        }
    }

    public void Clear(Player player)
    {
        // 클리어 시 보상
    }

    public void Fail(Player player)
    {
        // 실패 시  패널티
    }
}
