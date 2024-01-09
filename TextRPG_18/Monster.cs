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
		Console.Write($"\n{name} 이(가) {player.name} 을(를) 공격합니다. ");
		Console.Write($"(hp : {player.hp} -> ");
		player.hp -= Convert.ToInt32(Math.Ceiling(atk)); ;  // 공격력 소수점 올림 후 체력에서 공격력 빼기
		Console.WriteLine($"{player.hp} )");

    }

	public void printStat()
	{
		Console.WriteLine($"Lv.{level} {name} HP {hp}");
	}
}
