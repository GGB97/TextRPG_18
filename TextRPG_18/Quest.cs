using System;
using TextRPG;

public class Quest
{
    public string name { get; }
    public string description { get; }
    // 클리어 조건
    public bool isCompleted { get; private set; }
    public int requiredType { get; private set; }
    public int requiredCnt { get; private set; }
    public int killCnt { get; private set; }
    // 보상
    public int rGold { get; private set; }
    public int rExp { get; private set; }

    public Quest(string name, string description, int rGold, int rExp, int requiredType, int requiredCnt, bool test = false)
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
    public Quest(Quest q)
    {
        this.name = q.name;
        this.description = q.description;
        this.rGold = q.rGold;
        this.rExp = q.rExp;

        isCompleted = q.isCompleted;
        this.requiredType = q.requiredType;
        this.requiredCnt = q.requiredCnt;
        killCnt = q.killCnt;
    }
    public Quest(QuestJsonModel q)
    {
        if (q != null)
        {
            this.name = q.name;
            this.description = q.description;
            this.rGold = q.rGold;
            this.rExp = q.rExp;

            isCompleted = q.isCompleted;
            this.requiredType = q.requiredType;
            this.requiredCnt = q.requiredCnt;
            killCnt = q.killCnt;
        }
    }

    public void Check(Monster mon)
    {
        if (!this.isCompleted)
        {
            if (requiredType == mon.type || requiredType == (int)MonsterType.Monster) // 몬스터 타입이 일치하거나 모든 몬스터의 경우
            {
                if (mon.hp == 0)
                {
                    killCnt++;
                    CheckComplete();
                }
            }
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
            Console.WriteLine($"\n퀘스트 '{name}'이(가) 완료되었습니다.\n");

            Console.Write($"EXP : {player.exp} -> ");
            player.exp += rExp;
            Console.WriteLine($"{player.exp} (+{rExp})");

            Console.Write($"GOLD : {player.gold} -> ");
            player.gold += rGold;
            Console.WriteLine($"{player.gold} (+{rGold})");

            player.quests.quests.Remove(this);

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
        player.quests.Add(new Quest(this));
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
        Console.WriteLine($"{name} | {description} | {killCnt}/{requiredCnt}");
        Console.ForegroundColor = ConsoleColor.White;
    }
}
