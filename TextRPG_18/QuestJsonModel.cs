using System;

public class QuestJsonModel
{
    public string name { get; set; }
    public string description { get; set; }
    // 클리어 조건
    public bool isCompleted { get; set; }
    public int requiredType { get; set; }
    public int requiredCnt { get; set; }
    public int killCnt { get; set; }
    // 보상
    public int rGold { get; set; }
    public int rExp { get; set; }

    public QuestJsonModel()
    {

    }

    public QuestJsonModel(Quest q)
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
}
