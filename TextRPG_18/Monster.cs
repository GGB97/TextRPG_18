using System;

public class Monster
{
	public string name { get; set; }
	int level;
	float atk;
	int def;
	public int hp { get; set; }

	public Monster(string name, int level, int hp)
	{
		this.name = name;
		this.level = level;
		this.hp = hp;
		this.atk = level;
		this.def = level * 2;
	}
	public Monster(Monster mon)
	{
		this.name= mon.name;
		this.level = mon.level;
		this.hp = mon.hp;
		this.atk = mon.atk;
		this.def = mon.def;
	}


	public void Attack(Player player)
	{
		// 공격
		Console.WriteLine($"\n{name} 이(가) {player.name} 을(를) 공격합니다. (데미지 : {atk} )");
		player.hp -= Convert.ToInt32(Math.Ceiling(atk)); ;  // 공격력 소수점 올림 후 체력에서 공격력 빼기
		if (player.hp <= 0) 
		{ 
			player.hp = 0;
		}

    }

	public void printStat()
	{
		if(hp == 0)
		{
			Console.ForegroundColor = ConsoleColor.Red;	// 체력이 0인 대상은 붉은색으로 출력 (회색은 뭔가 확인이 애매해서)
            Console.WriteLine($"Lv.{level} {name} (Dead)");
            Console.ForegroundColor = ConsoleColor.White;	// 다시 돌려놔야 나머지 글자들이 하얀색으로 나옴
        }
        else
		{
            Console.WriteLine($"Lv.{level} {name} HP {hp}");
		}
	}
}
