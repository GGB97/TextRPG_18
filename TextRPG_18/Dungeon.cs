using System;
using System.Collections.Generic;
using TextRPG;


public class Dungeon
{
    int level;  //던전 난이도
    public int rSpec; // 권장 방어력
    int rGold; // 보상 골드
    int rExp;  // 보상 경험치

    int M_Turn;  //몬스터중 공격할 차례

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

        monsters.Add(new Monster("미니언", 2, 15, 5));
        monsters.Add(new Monster("대포", 5, 25, 12));
        monsters.Add(new Monster("공허충", 3, 10, 7));
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
        M_Turn = 0;

        while (!Out)  //활동 선택
        {

            if (1 == 1)  //접기위함
            {
                Console.Clear();
                ConsoleManager.YellowColor("Battle!!");
                Console.WriteLine();
                Console.WriteLine();

                for (int i = 0; i < MonsterRend.Count; i++)
                {
                    if (MonsterRend[i].IsDie())  //죽었을경우
                    {
                        MonsterRend.RemoveAt(i); //해당 인덱스로 리스트에서 삭제


                    }

                    Console.Write("[ Lv.");
                    ConsoleManager.RedColor(MonsterRend[i].GetLv().ToString());
                    Console.WriteLine(" " + MonsterRend[i].GetName() + " HP " + MonsterRend[i].hp.ToString() + " ]");

                }

                Console.WriteLine();

                Console.WriteLine("[내 정보]");
                Console.Write("3. Lv.");
                ConsoleManager.RedColor(player.getLevel().ToString());
                Console.WriteLine("   {0} ({1})", player.name, player.getJob());
                Console.Write("HP ");
                Console.WriteLine("{0}/{1}", player.hp, player.maxhp);

                Console.WriteLine();

                Console.WriteLine("1. 공격");
                //나중에 여기서 스킬, 방어, 음식 등등 있을듯
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.WriteLine();
            }

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

        }


        while (!Isrun)
        {
            //if() 플레이어가 지거나 클리어할 경우 Isrun = true;
            BattleStatus(player);

            Console.WriteLine("0. 취소");
            Console.WriteLine();
            Console.WriteLine("대상을 선택해주세요.");
            Console.WriteLine();

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

            }

            MonsterTurn(MonsterRend, player);

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
            if (MonsterRend[i].IsDie())  //죽었을경우
            {
                MonsterRend.RemoveAt(i); //해당 인덱스로 리스트에서 삭제


            }

            Console.Write((i + 1).ToString() + ". [ Lv.");
            ConsoleManager.RedColor(MonsterRend[i].GetLv().ToString());
            Console.WriteLine(" " + MonsterRend[i].GetName() + " HP " + MonsterRend[i].hp.ToString() + " ]");

        }

        Console.WriteLine();

        Console.WriteLine("[내 정보]");
        Console.Write("3. Lv.");
        ConsoleManager.RedColor(player.getLevel().ToString());
        Console.WriteLine("   {0} ({1})", player.name, player.getJob());
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


    public void MonsterTurn(List<Monster> MonsterRend, Player player)
    {
        Console.Clear();
        Random rend = new Random();


        if (MonsterRend.Count <= M_Turn)
        {
            M_Turn = 0; //리셋
        }

        //약간의 변수 추가
        if (rend.Next(0, 11) < 2 && MonsterRend.Count >= 2)
        {
            //희소한 확률로 두명이 공격

            MonsterRend[M_Turn++].attack(player);
            MonsterRend[M_Turn++].attack(player);

            Console.WriteLine();
            ConsoleManager.RedColor("극악의 확률 발생!");
            Console.WriteLine();
            ConsoleManager.RedColor("두명의 몬스터가 당신을 때렸습니다!");
            Console.WriteLine();
            Console.WriteLine();
            MonsterRend[M_Turn++].attack(player);
        }
        else
        {
            MonsterRend[M_Turn++].attack(player);
        }
        Console.WriteLine("0. 다음");
        bool Out = false;
        while (!Out)
        {
            if (Console.ReadLine() == "0") //실질적으로 hp깍기
            {
                Out = true;
            }
            else
            {
                ConsoleManager.RedColor("잘못된 입력입니다 ");
                Console.WriteLine();
            }
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
