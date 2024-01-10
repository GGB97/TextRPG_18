using System;
using System.Collections.Generic;
using TextRPG;


public class Dungeon
{
    int level;  //던전 난이도
    public int rSpec; // 권장 방어력
    int rGold; // 보상 골드
    int rExp;  // 보상 경험치

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

    public void BattleSetting(Player player)//몬스터 설정
    {
        MonsterRend = new List<Monster>();
        RandomMobster(MonsterRend);
        Battle(player);
    }

    public void Battle(Player player)
    {
        bool Out = false;
        bool Isrun = false;

        while (true)  //활동 선택
        {

            BattleStatus(player);

            Console.WriteLine("1. 공격");
            //나중에 여기서 스킬, 방어, 음식 등등 있을듯
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.WriteLine();

            switch (Console.ReadLine())
            {
                case "1":
                    Out = true;
                    break;
                default:
                    ConsoleManager.RedColor("잘못된 입력입니다 ");
                    Console.WriteLine();
                    break;
            }

            if (Out)
            {
                break;
            }

        }


        do  //대상 선택
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

            string Input = Console.ReadLine();
            if (Input == "0") Battle(player);
            else if (Input == "1" && MonsterRend.Count >= 1) player.Attack(MonsterRend[0]);
            else if (Input == "2" && MonsterRend.Count >= 2) player.Attack(MonsterRend[1]);
            else if (Input == "3" && MonsterRend.Count >= 3) player.Attack(MonsterRend[2]);
            else if (Input == "4" && MonsterRend.Count >= 4) player.Attack(MonsterRend[3]);
            else
            {
                ConsoleManager.RedColor("잘못된 입력입니다 ");
                Console.WriteLine();
                //string Input = Console.ReadLine();
            }

            ConsoleManager.RedColor("잘못된 입력입니wdqdq다 ");
            Input = Console.ReadLine();

        }

    }

    public void BattleStatus(Player player)
    {
        Console.Clear();
        ConsoleManager.YellowColor("Battle!!");
        Console.WriteLine();
        Console.WriteLine();

        for (int i = 0; i < MonsterRend.Count; i++)
        {
            if(!MonsterRend[i].IsDie())  //몬스터가 살아있을경우
            {
                Console.Write((i + 1).ToString() + ". [ Lv.");
                ConsoleManager.RedColor(MonsterRend[i].GetLv().ToString());
                Console.WriteLine(" " + MonsterRend[i].GetName() + " HP " + MonsterRend[i].hp.ToString() + " ]");
            }
            else //죽었을 경우
            {
                MonsterRend.RemoveAt(i); //해당 인덱스로 리스트에서 삭제
            }
           
        }

        Console.WriteLine();

        Console.WriteLine("[내 정보]");
        Console.Write("3. Lv.");
        ConsoleManager.RedColor(player.getLevel().ToString());
        Console.WriteLine("   {0} ({1})",player.name, player.getJob());
        Console.Write("HP ");
        Console.WriteLine("{0}/{1}", player.hp, player.maxhp);

        Console.WriteLine();

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


    public void GetOut()
    {

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
