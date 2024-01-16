using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_18
{
    public class Job
    {
        public JobType type;
        public string name;
        public int hp;
        public int mp;
        public int atk;
        public int def;
        public int criticalChance;
        public int criticalDamage;
        public int Avoidance;  //회피율
        public int MP_Recovery;  //마나 회복율


        public int turn = 0;
        public bool turnfalse = false;



        public string Skill_name1;
        public string Skill_name2;


        //회피율 (공통) , 기본 공격력 업 (광전사) 치명타 확률(용기사), 치명타 피해량(마법사) 

        public Job()
        {

        }

        public void Pick(Player player) // 직업선택시 스택 적용
        {
            player.maxHp = hp;
            player.maxMp = mp;
            playermax.atk = atk;
            playermax.dfs = def;
            playermax.CRD = criticalDamage;
            playermax.CRP = criticalChance;

            player.hp = this.hp;
            player.mp = this.mp;
            player.atk = this.atk;
            player.def = this.def;
            player.criticalChance = this.criticalChance;
            player.criticalDamage = this.criticalDamage;
            player.Avoidance = Avoidance;
            player.MP_Recovery = MP_Recovery;
        }

        public virtual void skill_1(List<Monster> mon, Player player) //범위계
        {

        }

        public virtual void Skill_2(Player player) //서포터
        {

        }

        public virtual string GetName1()
        {
            return "DefaultName1";
        }
        public virtual string GetName2()
        {
            return "DefaultName1";
        }

        public virtual bool Initialization(Player player)//초기화
        {
            return false;

        }

    }

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
            player.hp -= player.maxHp * 45/100;
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
                    if (n>=2)
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
                if (turn >= turn_reset*3)
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
            Skill_name2 = "주문 영창 : 마나 75을 소비해 자신의 치명타 확률을 50%, 치명타 피해를 2배 증가시킨다. (3턴 지속) \n   [또한, 자신이 다음에 시전하는 마법의 마나 소모량이 250 감소한다. (중첩 및 지속 시간 연장 가능)]";
        }

        public override void skill_1(List<Monster> mon, Player player)
        {
            if (player.mp < (500 - 250*magic_cast))
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
            Console.Write($" {magic_cast*250}%");
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
            Console.Write($" {magic_cast*3 -turn} ");
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
                if (turn >= magic_cast*3)
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
}
