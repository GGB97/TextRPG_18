using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


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