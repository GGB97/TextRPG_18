using Newtonsoft.Json;
using System;

public class ArmorJsonModel : ItemJsonModel
{
    [JsonProperty("stat")]
    public int def { get; set; }

    public ArmorJsonModel()
    {
        is_Equip = false;
        type = 0;
        name = null;
        description = null;
        cost = 0;

        def = 0;
    }
    public ArmorJsonModel(Armor armor)
	{
        if (armor != null)
        {
            is_Equip = armor.getEquip();
            type = armor.getType();
            name = armor.getName();
            description = armor.getDesc();
            cost = armor.cost;

            def = armor.getDef();
        }
    }

    public string SerializeToString()
    {
        string jsonStr;
        jsonStr = JsonConvert.SerializeObject(this);

        return jsonStr;
    }
    public ArmorJsonModel Deserialize(string str)
    {
        return JsonConvert.DeserializeObject<ArmorJsonModel>(str);
    }
}
