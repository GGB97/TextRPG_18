using System;
using System.Collections.Generic;
using System.Linq;
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
            player.hp = this.hp;
            player.mp = this.mp;
            player.atk = this.atk;
            player.def = this.def;    
            player.criticalChance = this.criticalChance;
            player.criticalDamage = this.criticalDamage;
        }
    }

    public class Warrior : Job
    {
        public Warrior(string name, int hp, int mp, int atk, int def, float criticalChance, float criticalDamage) : base(name, hp, mp, atk, def, criticalChance, criticalDamage)
        {
        }
    }
    public class Kinght : Job
    {
        public Kinght(string name, int hp, int mp, int atk, int def, float criticalChance, float criticalDamage) : base(name, hp, mp, atk, def, criticalChance, criticalDamage)
        {
        }
    }

    public class Mage : Job
    {
        public Mage(string name, int hp, int mp, int atk, int def, float criticalChance, float criticalDamage) : base(name, hp, mp, atk, def, criticalChance, criticalDamage)
        {
        }
    }


}
