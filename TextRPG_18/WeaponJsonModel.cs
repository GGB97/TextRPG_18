using Newtonsoft.Json;
using System;

public class WeaponJsonModel : ItemJsonModel
{
    [JsonProperty("stat")]
    public float atk { get; set; }

    public WeaponJsonModel()
    {
        is_Equip = false;
        type = 0;
        name = null;
        description = null;
        cost = 0;

        atk = 0;
    }
    public WeaponJsonModel(Weapon weapon)
    {
        if (weapon != null)
        {
            is_Equip = weapon.getEquip();
            type = weapon.getType();
            name = weapon.getName();
            description = weapon.getDesc();
            cost = weapon.cost;

            atk = weapon.getAtk();
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
