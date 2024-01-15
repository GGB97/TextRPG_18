using System;
using System.Numerics;
using System.Text.Json;

public class PlayerJsonModel
{
    public int level { get; set; }
    public int exp { get; set; }
    public int maxExp { get; set; }
    public string name { get; set; }
    public int hp { get; set; }
    public int maxHp { get; set; }
    public int gold { get; set; }
    public float atk { get; set; }
    public int def { get; set; }
    public int criticalChance { get; set; }
    public int criticalDamage { get; set; }
    public int Avoidance { get; set; } //회피율
    public int MP_Recovery { get; set; } //마나 회복율
    public JobType job { get; set; }

    public string invenStr { get; set; }
    public string weponStr { get; set; }
    public string armorStr { get; set; }
    public string questStr { get; set; }

    public InventoryJsonModel inventory;
    public WeaponJsonModel eWeapon;
    public ArmorJsonModel eArmor;
    public QuestListJsonModel quests;
    
    

    public PlayerJsonModel()
    {
        level = 0;
        exp = 0;
        maxExp = 0;
        name = null;
        hp = 0;
        maxHp = 0;
        gold = 0;
        atk = 0;
        def = 0;

        job = JobType.Berserker;
        criticalChance = 0;
        criticalDamage = 0;
        Avoidance = 0;
        MP_Recovery = 0;

        inventory = new();
        eWeapon = new();
        eArmor = new();
        quests = new();

        invenStr = null;
        weponStr = null;
        armorStr = null;
        questStr = null;
    }
    public PlayerJsonModel(Player player)
    {
        level = player.getLevel();
        exp = player.exp;
        maxExp = player.getmaxExp();
        name = player.name;
        //job = player.getJob();
        hp = player.hp;
        //maxHp = player.maxHp;
        gold = player.gold;
        atk = player.atk;
        def = player.def;

        job = player.SelectedClass.type;
        criticalChance = player.criticalChance;
        criticalDamage = player.criticalDamage;
        Avoidance = player.Avoidance;
        MP_Recovery = player.MP_Recovery;

        inventory = new(player.inventory);
        eWeapon = new(player.eWeapon);
        eArmor = new(player.eArmor);
        quests = new(player.quests);

        invenStr = inventory.SerializeToString();
        weponStr = eWeapon.SerializeToString();
        armorStr = eArmor.SerializeToString();
        questStr = quests.SerializeToString();
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
        playerData.quests = playerData.quests.Deserialize(playerData.questStr);

        return playerData;
    }

    public static Player ModelToPlayer(PlayerJsonModel model)
    {
        Player player = new(model);

        return player;
    }
}
