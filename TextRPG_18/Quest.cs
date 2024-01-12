using System;
using TextRPG;

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

    public Quest(string name, string description, int rGold, int rExp, string requiredType, int requiredCnt, bool test = false)
    {
        this.name = name;
        this.description = description;
        this.rGold = rGold;
        this.rExp = rExp;

        isCompleted = test;
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
            Console.WriteLine($"\n퀘스트 '{name}'이(가) 완료되었습니다.\n");
            GameManager.PressEnter();
        }
        else
        {
            Console.WriteLine("\n퀘스트가 완료 가능한 상태가 아닙니다.\n");
            GameManager.PressEnter();
        }
    }

    public void Accept(Player player)
    {
        player.quests.Add(this);
        Console.WriteLine($"\n{name} 퀘스트가 수락 되었습니다.\n");
        GameManager.PressEnter();
    }

    public void Abandon(Player player)
    {
        player.quests.Remove(this);
        Console.WriteLine($"\n{this.name} 퀘스트가 포기 되었습니다.\n");
        GameManager.PressEnter();
    }

    public void Print()
    {
        if (isCompleted)
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        Console.WriteLine($"{name} | {description}");
        Console.ForegroundColor = ConsoleColor.White;
    }
}
