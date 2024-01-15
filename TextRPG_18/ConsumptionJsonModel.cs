using Newtonsoft.Json;
using System;

public class ConsumptionJsonModel : ItemJsonModel
{
    [JsonProperty("stat")]
    public int efficacy { get; set; }

    public ConsumptionJsonModel()
    {
        is_Equip = false;
        type = (int)ItemType.Consumables;
        name = null;
        description = null;
        cost = 0;

        efficacy = 0;
    }
    public ConsumptionJsonModel(Consumption cons)
    {
        if (cons != null)
        {
            is_Equip = cons.getEquip();
            type = cons.getType();
            name = cons.getName();
            description = cons.getDesc();
            cost = cons.cost;

            efficacy = cons.getEff();
        }
    }

    public string SerializeToString()
    {
        string jsonStr;
        jsonStr = JsonConvert.SerializeObject(this);

        return jsonStr;
    }
    public WeaponJsonModel Deserialize(string str)
    {
        return JsonConvert.DeserializeObject<WeaponJsonModel>(str);
    }
}
