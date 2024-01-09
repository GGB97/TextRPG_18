using System;

public class Dungeon
{
    int level;  //던전 난이도
	public int rSpec; // 권장 방어력
	int rGold; // 보상 골드
    int rExp;  // 보상 경험치

    // 출현 가능 몬스터 list
    List<Monster> monsters;
    Monster[] enemies;
	public Dungeon(int level)
	{
        this.level = level;
		if(level == 1)
		{
			rSpec = 5;
            rGold = 1000;
            rExp = 10;
		}
        else if (level == 2)
        {
            rSpec = 10;
            rGold = 1700;
            rExp = 25;
        }
        else if (level == 3)
        {
            rSpec = 20;
            rGold = 2500;
            rExp = 40;
        }

        monsters = new List<Monster>(); // 출현 가능한 몬스터들 미리 넣어두기
        monsters.Add(new Monster("미니언", 2, 15));
        monsters.Add(new Monster("대포", 5, 25));
        monsters.Add(new Monster("공허충", 3, 10));
        
        Random rand = new Random();     
        enemies = new Monster[rand.Next(1,5)];  // 랜덤으로 1~4 마리의 몬스터를 넣어 둘 수 있는 Monster[] 배열을 생성
        for (int i = 0; i < enemies.Length; i++)    // 생성한 몬스터의 배열에 리스트에 있는 몬스터(프리팹 같은 역할)
        {                                           // 을 랜덤으로 가져와서 할당 반복
            enemies[i] = new Monster(monsters[rand.Next(0, monsters.Count)]);
        }

    }

    public void Battle(Player player)
    {
        Console.Clear();
        Console.WriteLine("Battle!! \n");

        foreach(Monster mon in enemies)
        {
            mon.printStat();
        }
        Console.WriteLine();
    }

    public void Clear(Player player)
    {
        // 클리어 시 보상
    }

    public void Fail(Player player)
    {
        // 실패 시  패널티
    }
}
