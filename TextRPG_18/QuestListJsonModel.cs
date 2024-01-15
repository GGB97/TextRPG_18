using Newtonsoft.Json;
using System;

public class QuestListJsonModel
{
    public List<QuestJsonModel> quests;

    public QuestListJsonModel()
	{
        quests = new List<QuestJsonModel>();
	}
    public QuestListJsonModel(QuestList pq)
    {
        foreach (var q in pq.quests)
        {
            this.quests.Add(new QuestJsonModel(q));
        }
    }

    public string SerializeToString()
    {
        string jsonStr;
        jsonStr = JsonConvert.SerializeObject(this);
        return jsonStr;
    }

    public QuestListJsonModel Deserialize(string str)
    {
        return JsonConvert.DeserializeObject<QuestListJsonModel>(str);
    }
}
