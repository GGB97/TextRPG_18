using System;

public class QuestList
{
    public List<Quest> quests = new();
    public QuestList()
    {
        quests = new List<Quest>();
    }

    public QuestList(QuestListJsonModel data)
    {
        if (data.quests != null)
        {
            foreach (var item in data.quests)
            {
                this.quests.Add(new Quest(item));
            }
        }
    }

    public void Add(Quest q)
    {
        quests.Add(q);
    }
    public void Remove(Quest q)
    {
        quests.Remove(q);
    }
}
