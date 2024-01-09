using System;
using System.Numerics;
using System.Text.Json;

public class PlayerJsonModel
{
    public int level { get; set; }
    public int exp { get; set; }
    public int maxExp { get; set; }
    public string name { get; set; }
    public string job { get; set; }
    public int hp { get; set; }
    public int gold { get; set; }
    public float atk { get; set; }
    public int def { get; set; }

    public string invenStr { get; set; }
    public string weponStr { get; set; }
    public string armorStr { get; set; }


    public InventoryJsonModel inventory;
    public WeaponJsonModel eWeapon;
    public ArmorJsonModel eArmor;

    public PlayerJsonModel()
    {
        level = 0;
        exp = 0;
        maxExp = 0;
        name = null;
        job = null;
        hp = 0;
        gold = 0;
        atk = 0;
        def = 0;

        inventory = new();
        eWeapon = new();
        eArmor = new();

        invenStr = null;
        weponStr = null;
        armorStr = null;
    }
    public PlayerJsonModel(Player player)
    {
        level = player.getLevel();
        exp = player.exp;
        maxExp = player.getmaxExp();
        name = player.name;
        job = player.getJob();
        hp = player.hp;
        gold = player.gold;
        atk = player.atk;
        def = player.def;

        inventory = new(player.inventory);
        eWeapon = new(player.eWeapon);
        eArmor = new(player.eArmor);

        invenStr = inventory.SerializeToString();
        weponStr = eWeapon.SerializeToString();
        armorStr = eArmor.SerializeToString();
    }

    public string SerializeToString()
    {
        var option = new JsonSerializerOptions() { WriteIndented = true };
        string jsonStr = JsonSerializer.Serialize(this, option);

        return jsonStr;
    }

    public static PlayerJsonModel Deserialize(string str)
    {
        PlayerJsonModel playerData = new();
        playerData = JsonSerializer.Deserialize<PlayerJsonModel>(str);

        playerData.inventory = playerData.inventory.Deserialize(playerData.invenStr);
        playerData.eWeapon = playerData.eWeapon.Deserialize(playerData.weponStr);
        playerData.eArmor = playerData.eArmor.Deserialize(playerData.armorStr);

        return playerData;
    }

    public static Player ModelToPlayer(PlayerJsonModel model)
    {
        Player player = new(model);

        return player;
    }
}
