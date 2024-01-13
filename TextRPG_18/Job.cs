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

        public string name;
        public int hp;
        public int mp;
        public int atk;
        public int def;
        public float criticalChance;
        public float criticalDamage;
        public int turn = 0;
        public bool turnfalse = false;

        public string Skill_name1;
        public string Skill_name2;


        //회피율 (공통) , 기본 공격력 업 (전사) 치명타 확률(기사), 치명타 피해량(마법사) 

        public Job(string name, int hp, int mp, int atk, int def, float criticalChance, float criticalDamage)
        {
            this.name = name;
            this.hp = hp;
            this.mp = mp;
            this.atk = atk;
            this.def = def;
            this.criticalChance = criticalChance;
            this.criticalDamage = criticalDamage;
        }

        public void Pick(Player player) // 직업선택시 스택 적용
        {
            playermax.maxHp = hp;
            playermax.maxmp = mp;
            playermax.atk = atk;
            playermax.dfs = def;
            player.hp = this.hp;
            player.mp = this.mp;
            player.atk = this.atk;
            player.def = this.def;
            player.criticalChance = this.criticalChance;
            player.criticalDamage = this.criticalDamage;
            //player.type = type();
        }

        public virtual int type()
        {
            return 0;
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
        public string Skill_name1 = "광 : 마나 10을 사용해 모든 몬스터에게 참격을 가합니다";
        public string Skill_name2 = "굶주림 :자신의 전체 피에서 20% 깎고 기본 공격력을 30%증가시킵니다 (3턴동안)";

        public Warrior(string name, int hp, int mp, int atk, int def, float criticalChance, float criticalDamage) : base(name, hp, mp, atk, def, criticalChance, criticalDamage)
        {


        }

        public override int type()
        {
            return 1;
        }

        public override void skill_1(List<Monster> mon, Player player)
        {
            player.mp -= 10;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n마나 10이 소비되었습니다.");
            Console.WriteLine(player.name + " (이)가 거대한 참격을 준비합니다!");
            Console.ResetColor();
            Thread.Sleep(500);
            Console.WriteLine($"=====================================================");
            foreach (var item in mon)
            {

                Console.WriteLine($"\n{player.name} (이)가 {item.name}을(를) 공격!");
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
            int a = (int)(playermax.maxHp * 0.2);
            int b = (int)(atk * 0.3);

            Console.WriteLine($"=====================================================");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n 전체 HP에서 20%이 감소되었습니다. ({player.hp} -> {player.hp - a})");
            Console.WriteLine($" {player.name} (이)가 피에 굶주림니다. 공격력이 30%증가합니다 ({player.atk} -> {player.atk + b})");
            Console.ResetColor();

            turnfalse = true;
            player.hp -= a; //20% 빠짐
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
            Console.WriteLine("실행되긴 하는거임?");
            if (turnfalse)
            {
                if (turn >= 3)
                {
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
        public string Skill_name1 = "격 : 마나 10을 이용해 모든 몬스터에게 용의 힘을 발산합니다 ";
        public string Skill_name2 = "용기: 마나 10을 이용해 자신의 방어력을 30% 증가시킵니다";
        public Kinght(string name, int hp, int mp, int atk, int def, float criticalChance, float criticalDamage) : base(name, hp, mp, atk, def, criticalChance, criticalDamage)
        {
        }

        public override int type()
        {
            return 2;
        }
        public override void skill_1(List<Monster> mon, Player player)
        {

        }

        public override void Skill_2(Player player)
        {

        }
        public override string GetName1()
        {
            return Skill_name1;
        }
        public override string GetName2()
        {
            return Skill_name2;
        }
    }

    public class Mage : Job
    {
        public string Skill_name1 = "화 : 마나 15를 이용해 적 모두에게 파이어 볼을";
        public string Skill_name2 = "욕망: 마나 15를 이용해 자신의 치명타 피해를 30% 증가합니다";
        public Mage(string name, int hp, int mp, int atk, int def, float criticalChance, float criticalDamage) : base(name, hp, mp, atk, def, criticalChance, criticalDamage)
        {
        }

        public override int type()
        {
            return 3;
        }

        public override void skill_1(List<Monster> mon, Player player)
        {
            foreach (var item in mon)
            {
                //
            }
        }

        public override void Skill_2(Player player)
        {

        }
        public override string GetName1()
        {
            return Skill_name1;
        }
        public override string GetName2()
        {
            return Skill_name2;
        }
    }


}
