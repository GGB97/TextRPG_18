using System;

public class Dungeon
{
    int level;  //던전 난이도
	public int rSpec; // 권장 방어력
	int rGold; // 보상 골드
    int rExp;  // 보상 경험치

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
