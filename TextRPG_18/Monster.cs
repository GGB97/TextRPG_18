using System;

public class Monster
{
	string name;
	int level;
	int hp;

	public Monster(string name, int level, int hp)
	{
		this.name = name;
		this.level = level;
		this.hp = hp;
	}
	public Monster(Monster mon)
	{
		this.name= mon.name;
		this.level = mon.level;
		this.hp = mon.hp;
	}


	public void attack()
	{
		// 공격
	}

	public void printStat()
	{
		Console.WriteLine($"Lv.{level} {name} HP {hp}");
	}
}
