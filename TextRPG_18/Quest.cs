using System;

public class Quest
{
    public string name { get; }
    public string description { get; }
    // 클리어 조건
    public bool isCompleted { get; private set; }
    public string requiredType { get; private set; }
    public int requiredCnt { get; private set; }
    public int killCnt { get; private set; }
    // 보상
    public int rGold { get; private set; }
    public int rExp { get; private set; }

    public Quest(string name, string description, int rGold, int rExp, string requiredType, int requiredCnt)
    {
        this.name = name;
        this.description = description;
        this.rGold = rGold;
        this.rExp = rExp;

        isCompleted = false;
        this.requiredType = requiredType;
        this.requiredCnt = requiredCnt;
        killCnt = 0;
    }

    public void Check(Monster mon)
    {
        if(requiredType == mon.type && mon.hp == 0)
        {
            killCnt++;
            CheckComplete();
        }
    }

    public void CheckComplete()
    {
        if (killCnt == requiredCnt)
            isCompleted = true;
    }

    public void Complete(Player player)
    {
        if (isCompleted)
        {
            player.exp += rExp;
            player.gold += rGold;
            Console.WriteLine($"퀘스트 '{name}'이 완료되었습니다.");
        }
    }

    public void Print()
    {
        Console.WriteLine($"{name} | 내용 :  {description}");
    }
}
