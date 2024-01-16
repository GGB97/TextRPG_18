using System;

public class Kinght : Job
{
    int turn_reset = 0;

    public Kinght()
    {
        this.type = JobType.DragonKnight;
        this.name = "용기사";
        this.hp = 200;
        this.mp = 75;
        this.atk = 20;
        this.def = 30;
        this.criticalChance = 30;
        this.criticalDamage = 130;
        this.Avoidance = 10;
        this.MP_Recovery = 10;

        Skill_name1 = "드래곤 스트라이크 : 마나 30을 소비해 드래곤의 힘을 실은 창을 들고 돌진한다. \n   [모든 적에게 ATK*0.7 피해. 그리고 무작위 적 하나에게 ATK*1.5로 공격.]";
        Skill_name2 = "용혈의 계약 : 마나 25를 소비해 체력을 즉시 10% 회복하고, 자신의 방어력을 50% 증가시킨다. (3턴 지속)";
    }

    public override void skill_1(List<Monster> mon, Player player)
    {
        if (player.mp < 30)
        {
            Console.WriteLine("\n마나가 부족합니다.");
            Console.WriteLine("시전 실패.");
            Console.WriteLine($"{player.name}은(는) 대기했다!\n");
            return;
        }
        player.mp -= 30;
        Console.Write("\n마나 ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("30");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($" 을 소비했다!\n");
        Console.WriteLine(player.name + " (이)가 창에 드래곤을 강림시켜 적들을 빠르게 휩쓴다!"); // 
        Console.ResetColor();
        Thread.Sleep(500);
        Console.WriteLine($"=====================================================");
        foreach (var item in mon)
        {
            Console.WriteLine($"\n{player.name}은(는) 드래곤의 힘을 실은 창으로 {item.name}을(를) 공격!");
            Thread.Sleep(250);
            int minushp = player.PlayerDamage(); //치명타 계산
            Console.Write($"{item.name}은(는) ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{minushp * 70 / 100}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" 의 데미지를 입었다!\n");
            Thread.Sleep(250);
            item.hp -= minushp * 70 / 100;

            if (item.hp <= 0)
            {
                item.hp = 0;
                item.live = "dead";
                Console.Write($"{item.name}은(는) 쓰러졌다!\n");
            }
        }
        Thread.Sleep(300);
        while (true)
        {
            bool allMonstersDead = mon.All(monster => monster.live == "dead");
            if (allMonstersDead)
            {
                break;
            }
            else
            {
                Random random = new Random();
                int random_target = random.Next(0, mon.Count);
                if (mon[random_target].live == "live")
                {
                    int minushp = player.PlayerDamage();
                    Console.WriteLine($"\n그리고 추가 공격!");
                    Console.WriteLine($"{player.name} 이(가) 드래곤의 진노를 담아 {mon[random_target].name}을(를) 꿰뚫는다!");
                    Thread.Sleep(600);
                    Console.Write($"{mon[random_target].name}은(는) ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{minushp * 15 / 10}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($" 의 데미지를 입었다!\n");
                    Thread.Sleep(400);
                    mon[random_target].hp -= minushp * 15 / 10;
                    if (mon[random_target].hp <= 0)
                    {
                        Console.Write($"{mon[random_target].name}은(는) 쓰러졌다!\n");
                        mon[random_target].hp = 0;
                        mon[random_target].live = "dead";
                    }
                    break;
                }
            }
        }



        Console.WriteLine($"=====================================================");
    }

    public override void Skill_2(Player player)
    {
        if (player.mp < 25)
        {
            Console.WriteLine("\n마나가 부족합니다.");
            Console.WriteLine("시전 실패.");
            Console.WriteLine($"{player.name}은(는) 대기했다!\n");
            return;
        }
        int b = (int)(player.def * 0.5);
        int save_hp = player.hp;
        player.hp += player.hp * 10 / 100;
        if (player.hp >= player.maxHp)
        {
            player.hp = player.maxHp;
        }

        Console.WriteLine($"=====================================================");
        Console.Write("\n마나 ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("25");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($" 를 소비했다!");
        Console.Write("\n최대 체력의 ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("10%");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($" 를 회복했다! :");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($" {save_hp} ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"→");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($" {player.hp} \n");
        Console.ForegroundColor = ConsoleColor.White;
        Thread.Sleep(300);

        Console.Write($" {player.name} (은)는 드래곤의 피에 맹세했다! 방어력이");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write($" 50% ");

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"증가한다! ");
        if (turn_reset != 0)
        {
            Console.Write($"(지속시간 + 3턴 :)");
        }
        else
        {
            Console.Write($"(3턴 지속) :");
        }
        turn_reset += 1;
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write($" {player.def} ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"→");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write($" {player.def + b}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($" 남은 턴 수: ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write($" {turn_reset * 3 - turn} ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"턴\n");
        Thread.Sleep(300);

        turnfalse = true;
        player.mp -= 25;
        player.def += b; //50% 증가
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
            if (turn >= turn_reset * 3)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"용혈의 효과가 사라졌다.. 방어력 :");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($" {player.def} ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"→");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($" {playermax.dfs} \n\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ResetColor();
                Thread.Sleep(300);
                turnfalse = false;
                turn = 0;
                turn_reset = 0;
                player.def = playermax.dfs; //다시 돌려놓기
                return true;
            }
            else return false;
        }
        else return false;
    }
}