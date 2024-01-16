using System;

public class Warrior : Job
{
    int turn_reset = 0;

    public Warrior()
    {
        this.type = JobType.Berserker;
        this.name = "광전사";

        this.hp = 150;
        this.mp = 50;
        this.atk = 30;
        this.def = 15;
        this.criticalChance = 25;
        this.criticalDamage = 150;
        this.Avoidance = 25;
        this.MP_Recovery = 5;

        Skill_name1 = "피의 광란 : 마나 15와 최대 체력의 45%를 소비해 광란의 참격을 가한다.\n   [무작위 적에게 ATK*2의 피해. 대상을 처치했을 경우 체력을 10% 회복하고 1회 더 반복. 체력이 10% 이하라면 시전 불가.]";
        Skill_name2 = "갈망 : 최대 체력의 25%를 소비해 기본 공격력을 50% 증가 시킨다. (3턴 지속)\n   [이 스킬은 체력이 부족해도 시전할 수 있다.]";
    }


    public override void skill_1(List<Monster> mon, Player player)
    {
        if (player.mp < 15)
        {
            Console.WriteLine("\n마나가 부족합니다.");
            Console.WriteLine("시전 실패.");
            Console.WriteLine($"{player.name}은(는) 대기했다!\n");
            return;
        }
        if (player.hp < player.maxHp * 10 / 100)
        {
            Console.WriteLine("\n체력이 부족합니다.");
            Console.WriteLine("시전 실패.");
            Console.WriteLine($"{player.name}은(는) 대기했다!\n");
            return;
        }
        int save_hp = player.hp;
        player.hp -= player.maxHp * 45 / 100;
        if (player.hp <= 0)
        {
            player.hp = 1;
        }
        player.mp -= 15;
        Console.Write("\n마나 ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("15");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(" 와 체력 ");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write($"45%");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"를 소비했다! :");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write($" {save_hp} ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"→");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write($" {player.hp} \n");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(player.name + " (이)가 대검을 휘둘러 광란의 참격을 발한다!");
        Console.ResetColor();
        Thread.Sleep(500);
        Console.WriteLine($"=====================================================");
        int n = 1;
        for (int i = 0; i < n; i++)
        {
            Random random = new Random();
            int random_target = random.Next(0, mon.Count);
            if (mon[random_target].live == "live")
            {
                if (n >= 2)
                {
                    Console.Write($"적을 처치하는데 성공! ");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write($"피의 광란");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($" 을(를) 재시전 한다!");
                }
                Console.WriteLine($"\n{player.name}의 참격이 {mon[random_target].name}을(를) 공격!");
                Thread.Sleep(700);
                int minushp = player.PlayerDamage(); //치명타 계산
                Console.Write($"{mon[random_target].name}은(는) ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{minushp * 2}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" 의 데미지를 입었다!\n");
                Thread.Sleep(400);
                mon[random_target].hp -= minushp * 2;

                if (mon[random_target].hp <= 0)
                {
                    Console.Write($"{mon[random_target].name}은(는) 쓰러졌다!\n");
                    mon[random_target].hp = 0;
                    mon[random_target].live = "dead";
                    save_hp = player.hp;
                    player.hp += player.maxHp * 10 / 100;

                    if (player.hp >= player.maxHp)
                    {
                        player.hp = player.maxHp;
                    }
                    Console.Write($"\n{player.name}은(는) 최대 체력의 ");
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
                    n += 1;
                }
                else
                {

                }
            }
            else
            {
                bool allMonstersDead = mon.All(monster => monster.live == "dead");
                if (allMonstersDead)
                {
                    break;
                }
                else
                {
                    n += 1;
                }
            }
        }
        Console.WriteLine($"=====================================================");
    }

    public override void Skill_2(Player player)
    {
        //int a = (int)(playermax.maxHp * 0.2);
        int b = (int)(player.atk * 0.5);
        int save_hp = player.hp;
        player.hp -= player.maxHp * 25 / 100;
        if (player.hp <= 0)
        {
            player.hp = 1;
        }

        Console.WriteLine($"=====================================================");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"\n최대 체력의 ");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write($"25%");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($" 를 소비했다! :");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write($" {save_hp} ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"→");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write($" {player.hp} \n");
        Console.ForegroundColor = ConsoleColor.White;
        Thread.Sleep(300);


        Console.Write($" {player.name} (이)가 피에 굶주린다! 공격력이");
        Console.ForegroundColor = ConsoleColor.DarkRed;
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
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write($" {player.atk} ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"→");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write($" {player.atk + b}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($" 남은 턴 수: ");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write($" {turn_reset * 3 - turn} ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"턴\n");
        Thread.Sleep(300);


        turnfalse = true;
        //player.hp -= a; //20% 빠짐
        player.atk += b; //30% 증가
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
                Console.WriteLine($"갈망이 사그라들었다.. 공격력 :");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($" {player.atk} ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"→");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($" {playermax.atk} \n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ResetColor();
                Thread.Sleep(300);
                turnfalse = false;
                turn = 0;
                turn_reset = 0;
                player.atk = playermax.atk; //다시 돌려놓기
                return true;
            }
            else return false;
        }
        else return false;
    }



}
