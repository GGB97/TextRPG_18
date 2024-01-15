using Newtonsoft.Json;
using System;

public class InventoryJsonModel
{
    public List<ItemJsonModel> items = new List<ItemJsonModel>(); // 인벤토리를 리스트로 하는건 낭비같긴 하지만 최대 용량을 생각하기 귀찮아서..

    public InventoryJsonModel()
    {
        items = new List<ItemJsonModel>();
    }
    public InventoryJsonModel(Inventory inven)
	{
		foreach (var item in inven.items)
		{
			if (item is Weapon)
			{
				items.Add(new WeaponJsonModel((Weapon)item));
			}
			else if (item is Armor)
            {
                items.Add(new ArmorJsonModel((Armor)item));
            }
            else if (item is Consumption)
            {
                items.Add(new ConsumptionJsonModel((Consumption)item));
            }
        }
	}

    public string SerializeToString()
    {
        string jsonStr;
        jsonStr = JsonConvert.SerializeObject(this);
        return jsonStr;
    }

    public InventoryJsonModel Deserialize(string str)
    {
       return JsonConvert.DeserializeObject<InventoryJsonModel>(str);
    }
}
