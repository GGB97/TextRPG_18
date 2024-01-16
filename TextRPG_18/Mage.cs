using System;

public class Mage : Job
{
    public int magic_cast = 0;

    public Mage()
    {
        this.type = JobType.Mage;
        this.name = "마법사";
        this.hp = 100;
        this.mp = 300;
        this.atk = 10;
        this.def = 10;
        this.criticalChance = 0;
        this.criticalDamage = 250;
        this.Avoidance = 15;
        this.MP_Recovery = 25;

        Skill_name1 = "대마법:메테오 : 마나 500을 소비해 유성우를 소환한다.\n   [모든 적에게 ATK*1000의 피해.]";
        Skill_name2 = "주문 영창 : 마나 75를 소비해 자신의 치명타 확률을 50%, 치명타 피해를 2배 증가시킨다. (3턴 지속) \n   [또한, 자신이 다음에 시전하는 마법의 마나 소모량이 250 감소한다. (중첩 및 지속 시간 연장 가능)]";
    }

    public override void skill_1(List<Monster> mon, Player player)
    {
        if (player.mp < (500 - 250 * magic_cast))
        {
            Console.WriteLine("\n마나가 부족합니다.");
            Console.WriteLine("시전 실패.");
            Console.WriteLine($"{player.name}은(는) 대기했다!\n");
            player.Recovery();
            return;
        }
        player.mp -= (500 - 250 * magic_cast);
        Console.Write("\n마나 ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write($"{500 - 250 * magic_cast}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($" 을 소비했다!\n");
        Console.WriteLine(player.name + " 은(는) 거대한 운석의 폭풍을 소환했다!\n"); //
        Console.ResetColor();
        Random random = new Random();

        for (int j = 0; j < 13; j++)
        {
            int random_star = random.Next(0, 3);
            if (random_star == 0)
            {
                Console.Write($" ");
            }
            else if (random_star == 1)
            {
                Console.Write($"");
            }
            else
            {
                Console.Write($"  ");
            }
            for (int i = 0; i < 33; i++)
            {
                random_star = random.Next(0, 4);
                if (random_star == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (random_star == 1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                else if (random_star == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                random_star = random.Next(0, 2);
                if (random_star == 0)
                {
                    Console.Write($"★");
                }
                else
                {
                    Console.Write($"☆");
                }
                random_star = random.Next(0, 2);
                if (random_star == 0)
                {
                    Console.Write($"  ");
                }
                else
                {
                    Console.Write($" ");
                }


            }
            Thread.Sleep(250);
            Console.Write($"\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        Console.WriteLine($"=====================================================");
        foreach (var item in mon)
        {
            if (item.live == "live")
            {
                Console.WriteLine($"\n{player.name}의 유성우가 {item.name}을(를) 강타!!!!");
                Thread.Sleep(350);
                int minushp = player.PlayerDamage(); //치명타 계산
                Console.Write($"{item.name}은(는) ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{minushp * 1000}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" 의 엄청난 데미지를 입었다!!!!!\n");
                Thread.Sleep(400);
                item.hp -= minushp * 1000;

                if (item.hp <= 0)
                {
                    item.hp = 0;
                    item.live = "dead";
                    Console.Write($"{item.name}은(는) 쓰러졌다!\n");
                }
            }
        }
        Console.WriteLine($"=====================================================");
        player.Recovery();
    }

    public override void Skill_2(Player player)
    {
        if (player.mp < 75)
        {
            Console.WriteLine("\n마나가 부족합니다.");
            Console.WriteLine("시전 실패.");
            Console.WriteLine($"{player.name}은(는) 대기했다!\n");
            player.Recovery();
            return;
        }

        Console.Write("\n마나 ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write($"75");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($" 를 소비했다!\n");
        player.mp -= 75;

        Console.Write($" {player.name} (은)는 마법의 주문을 영창했다! 치명타 확률이");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($" 50%, ");
        magic_cast += 1;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"치명타 피해가");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($" {magic_cast * 250}%");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($" 증가한다!");
        if (turnfalse)
        {
            Console.Write($"(지속시간 + 3턴)\n");
        }
        else
        {
            Console.Write($"(3턴 지속)\n");
        }
        Console.ForegroundColor = ConsoleColor.White;
        Thread.Sleep(300);
        Console.Write($"현재 주문 영창 중첩 : ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"★");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write($" {magic_cast} ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"★");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($" 남은 턴 수: ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write($" {magic_cast * 3 - turn} ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"턴\n");
        player.Recovery();
        if (player.mp >= (500 - (250 * magic_cast)))
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"[대마법 시전 가능]");
            Console.ForegroundColor = ConsoleColor.White;
        }


        turnfalse = true;

        //치명타 증가
        player.criticalChance += 50;
        player.criticalDamage *= 2;

    }
    public override string GetName1()
    {
        return Skill_name1;
    }
    public override string GetName2()
    {
        return Skill_name2;
    }

    public override bool Initialization(Player player)//초기화
    {
        if (turnfalse)
        {
            if (turn >= magic_cast * 3)
            {
                Console.Write($"주문 영창의 효과가 사라졌다.. 치명타 확률 :");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($" {player.criticalChance} ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"→");
                player.criticalChance = playermax.CRP;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($" {player.criticalChance} \n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"치명타 피해 :");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($" {player.criticalDamage} ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"→");
                player.criticalDamage = playermax.CRD;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($" {player.criticalDamage} \n\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ResetColor();
                Thread.Sleep(300);
                magic_cast = 0;
                turnfalse = false;
                turn = 0;
                return true;
            }
            else return false;
        }
        else return false;
    }
}
