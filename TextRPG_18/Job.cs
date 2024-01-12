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


        public string Skill_name1;
        public string Skill_name2;


        int PMhp;
        int PMmp;
        int PMatk;
        int PMdef;

        //회피율 (공통) , 기본 공격력 업 (전사) 치명타 확률(기사), 치명타 피해량(마법사) 

        public Job(string name,  int hp, int mp, int atk, int def, float criticalChance, float criticalDamage)
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
            player.job = this.name;
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


        public void skill_1(Monster[] mon) //범위계
        {
            
        }

        public void Skill_2(Player player) //서포터
        {

        }
    }

    public class Warrior : Job
    {
        public string Skill_name1 = "광 : 마나 10을 사용해 모든 몬스터에게 참격을 가합니다";
        public string Skill_name2 = "굶주림 :자신의 피를 20% 깎고 기본 공격력을 20%증가합니다";

        public Warrior(string name, int hp, int mp, int atk, int def, float criticalChance, float criticalDamage) : base(name, hp, mp, atk, def, criticalChance, criticalDamage)
        {

            
        }


        public override int type()
        {
            return 1;
        }

        public void skill_1(Monster[] mon, Player player)
        {
            //player.
        }

        public void Skill_2(Player player)
        {

        }

        public void Initialization()//초기화
        {

        }


    }
    public class Kinght : Job  
    {
        public string Skill_name1 = "";
        public string Skill_name2 = "";
        public Kinght(string name, int hp, int mp, int atk, int def, float criticalChance, float criticalDamage) : base(name, hp, mp, atk, def, criticalChance, criticalDamage)
        {
        }

        public override int type()
        {
            return 2;
        }
        public void skill_1(Monster[] mon, Player player)
        {

        }

        public void Skill_2(Player player)
        {

        }
    }

    public class Mage : Job 
    {
        public string Skill_name1 = "";
        public string Skill_name2 = "";
        public Mage(string name, int hp, int mp, int atk, int def, float criticalChance, float criticalDamage) : base(name, hp, mp, atk, def, criticalChance, criticalDamage)
        {
        }

        public override int type()
        {
            return 3;
        }

        public void skill_1(Monster[] mon, Player player)
        {

        }

        public void Skill_2(Player player)
        {

        }
    }


}
