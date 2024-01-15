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
            playermax.maxHp = hp;
            playermax.maxMp = mp;
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
        public string Skill_name1 = "피의 광란 : 마나 25와 최대 체력의 45%를 소비해 광란의 참격을 가한다. (무작위 적에게 ATK*2의 피해. 대상을 처치했을 경우 1회 더 반복.)"; //모든 적에게 ATK 1.5배 데미지
        public string Skill_name2 = "갈망 : 최대 체력의 30%를 소비해 기본 공격력을 50% 증가 시킨다. (3턴 지속)";

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
        }


        public override void skill_1(List<Monster> mon, Player player)
        {
            int save_hp = player.hp;
            player.hp -= playermax.maxHp * 45 / 100;
            if (player.hp <= 0)
            {
                player.hp = 1;
            }
            player.mp -= 25;
            Console.WriteLine("\n마나 ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("25");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" 와 체력 ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"45%");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"를 소비했다! :");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($" {save_hp} ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"→");
            Console.ForegroundColor = ConsoleColor.Red;
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
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"피의 광란");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($" 을(를) 재시전 한다!");
                    }
                    Console.WriteLine($"\n{player.name} (이)의 참격이 {mon[random_target].name}을(를) 공격!");
                    Thread.Sleep(700);
                    int minushp = player.PlayerDamage(); //치명타 계산
                    Console.Write($"{mon[random_target].name}은(는) ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{minushp * 150 / 100}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($" 의 데미지를 입었다!\n");
                    Thread.Sleep(400);
                    mon[random_target].hp -= minushp * 150 / 100;

                    if (mon[random_target].hp <= 0)
                    {
                        Console.Write($"{mon[random_target].name}은(는) 쓰러졌다!");
                        mon[random_target].hp = 0;
                        mon[random_target].live = "dead";
                        save_hp = player.hp;
                        player.hp += player.hp * 10 / 100;

                        if (player.hp >= playermax.maxHp)
                        {
                            player.hp = playermax.maxHp;
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
                    n += 1;
                    if (n >= 7)
                    {
                        break;
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
            player.hp -= playermax.maxHp * 30 / 100;
            if (player.hp <= 0)
            {
                player.hp = 1;
            }

            Console.WriteLine($"=====================================================");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"\n최대 체력의 ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"30%");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" 를 소비했다! :");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($" {save_hp} ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"→");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($" {player.hp} \n");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(300);


            Console.Write($" {player.name} (이)가 피에 굶주린다! 공격력이");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($" 50% ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"증가한다! (3턴 지속) :");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($" {player.atk} ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"→");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($" {player.atk + b} \n");
            Console.ForegroundColor = ConsoleColor.White;
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
                if (turn >= 3)
                {
                    Console.WriteLine($"갈망이 사그라들었다. :");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($" {player.atk} ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"→");
                    Console.Write($" {playermax.atk} \n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ResetColor();
                    Thread.Sleep(300);
                    turnfalse = false;
                    turn = 0;
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


        public string Skill_name1 = "드래곤 스트라이크 : 마나 30을 소비해 드래곤의 힘으로 속공한다. (모든 적에게 ATK*0.7 피해)";
        public string Skill_name2 = "불굴 : 마나 25를 소비해 체력을 즉시 10% 회복하고, 자신의 방어력을 50% 증가시킨다. (3턴 지속)";
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
            this.MP_Recovery = 5;
        }

        public override void skill_1(List<Monster> mon, Player player)
        {
            player.mp -= 30;
            Console.WriteLine("\n마나 ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("30");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" 을 소비했다! :");
            Console.WriteLine(player.name + " (이)가 창에 드래곤을 강림시켜 적들을 빠르게 휩쓴다!"); // 
            Console.ResetColor();
            Thread.Sleep(500);
            Console.WriteLine($"=====================================================");
            foreach (var item in mon)
            {

                Console.WriteLine($"\n{player.name} (이)의 힘이 {item.name}을(를) 공격!");
                Thread.Sleep(350);
                int minushp = player.PlayerDamage(); //치명타 계산
                Console.Write($"{item.name}은(는) ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{minushp * 70 / 100}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" 의 데미지를 입었다!\n");
                Thread.Sleep(400);
                item.hp -= minushp * 70 / 100;

                if (item.hp <= 0)
                {
                    item.hp = 0;
                    item.live = "dead";
                    player.hp += player.hp * 10 / 100;
                }


            }
            Console.WriteLine($"=====================================================");
        }

        public override void Skill_2(Player player)
        {
            int b = (int)(player.def * 0.5);
            int save_hp = player.hp;
            player.hp += player.hp * 10 / 100;
            if (player.hp >= playermax.maxHp)
            {
                player.hp = playermax.maxHp;
            }

            Console.WriteLine($"=====================================================");
            Console.WriteLine("\n마나 ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("25");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" 를 소비했다!");
            Console.WriteLine("\n최대 체력의 ");
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

            Console.Write($" {player.name} (은)는 드래곤의 비늘을 둘렀다! 방어력이");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($" 50% ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"증가한다! (3턴 지속) :");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($" {player.def} ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"→");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($" {player.def + b} \n");
            Console.ForegroundColor = ConsoleColor.White;
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
                if (turn >= 3)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"용기가 사그라듭니다  | 방어력 : {player.def} -> {playermax.dfs}");
                    Console.ResetColor();
                    turnfalse = false;
                    turn = 0;
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
        public string Skill_name1 = "메테오 스톰 : 마나 75 를 소비해 적 전체에게 ";
        public string Skill_name2 = "욕망 : 마나 15를 사용해 자신의 치명타 확률을 10%, 치명타 피해를 30%, 증가합니다";
        public Mage()
        {
            this.type = JobType.Mage;
            this.name = "마법사";
            this.hp = 120;
            this.mp = 300;
            this.atk = 10;
            this.def = 10;
            this.criticalChance = 10;
            this.criticalDamage = 250;
            this.Avoidance = 5;
            this.MP_Recovery = 50;
        }

        public override void skill_1(List<Monster> mon, Player player)
        {
            player.mp -= 15;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n마나 15가 소비되었습니다.");
            Console.WriteLine(player.name + " (이)가 불 원소를 적들에게 날립니다!");
            Console.ResetColor();
            Thread.Sleep(500);
            Console.WriteLine($"=====================================================");
            foreach (var item in mon)
            {

                Console.WriteLine($"\n{player.name} (이)의 마법이 {item.name}을(를) 공격!");
                int minushp = player.PlayerDamage(); //치명타 계산
                Console.WriteLine($"{item.name}은(는) {minushp}의 데미지를 입었다!\n");
                Thread.Sleep(600);
                item.hp -= minushp;

                if (item.hp <= 0)
                {
                    item.hp = 0;
                    item.live = "dead";
                }


            }
            Console.WriteLine($"=====================================================");
        }

        public override void Skill_2(Player player)
        {


            Console.WriteLine($"=====================================================");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n마나 15가 소비되었습니다. ({player.mp} -> {player.mp - 15})");
            Console.WriteLine($" {player.name} (이)의 의지가 강해집니다. 치명타가 강해집니다 : " +
                $"({player.criticalChance},{player.criticalDamage}) -> ({player.criticalChance + 10},{player.criticalDamage + 30})");
            Console.ResetColor();

            turnfalse = true;
            player.mp -= 15;
            //치명타 증가
            player.criticalChance += 10;
            player.criticalDamage += 30;
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
                if (turn >= 3)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"의지가 사그라듭니다   |  치명타 : ({player.criticalChance}/{player.criticalDamage}) -> ({playermax.CRP}/{playermax.CRD})");
                    Console.ResetColor();
                    turnfalse = false;
                    turn = 0;
                    player.criticalChance = playermax.CRP; //다시 돌려놓기
                    player.criticalDamage = playermax.CRD;
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}
