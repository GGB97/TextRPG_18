using System;

public class Monster
{
	public string name;
    public int type;
    public int level;
    public int hp;
    public int atk;
    public int gold;
    public int exp;
    public string live = "live";

	public Monster(string name, int type, int level, int hp, int atk, int gold, int exp)
	{
		this.name = name;
		this.type = type;
		this.level = level;
		this.hp = hp;
		this.atk = atk;
        this.gold = gold;
        this.exp = exp;
	}



    public void attack(Player player, ref string turn)
    {
        if (turn == "enemy_turn")
        {
            if (live == "live")
            {
                int damage = (atk - player.def);
                player.hp -= damage;
                if (player.hp <= 0)
                {
                    player.hp = 0;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[{name}의 턴!]");
                Console.ResetColor();
                Thread.Sleep(600);
                Console.WriteLine($"{name}이(가) {player.name}을(를) 공격!");
                Thread.Sleep(600);
                Console.WriteLine($"{player.name}은(는) {name}에게 {damage}의 데미지를 입었다! / 남은 HP: {player.hp}");
                Thread.Sleep(600);

                if (player.hp <= 0)
                {
                    Console.WriteLine($"{player.name}은(는) 패배했다..");
                    turn = "battle_defeat";
                }
            }
        }
    }
}
