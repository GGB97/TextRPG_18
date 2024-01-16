using System;

public class Item
{
    protected bool is_Equip;
    protected int type;
    protected string name;
    protected string description;
    public int cost { get; set; }

    protected Item()
    {
        is_Equip = false;
    }

    public virtual void Equip(Player player)
    {
        if(type == (int)ItemType.Weapon || type == (int)ItemType.Armor) // 아이템이 무기or방어구 일때만
        {
            is_Equip = true;
            Console.WriteLine($"{name} 이(가) 착용 되었습니다.");
        }
    }
    public virtual void unEquip(Player player)
    {
        if (type == (int)ItemType.Weapon || type == (int)ItemType.Armor)
        {
            is_Equip = false;
            if(name != null)
                Console.WriteLine($"{name} 이(가) 해제 되었습니다.");
        }
    }

    public virtual void useConsume(Player player)
    {
        //if (type == (int)ItemType.Consumables)
        //{
        //    Console.WriteLine($"{name}을 사용했습니다.");
        //    int cos = player.hp;
        //    player.hp += efficacy;
        //    if (player.hp >= player.maxHp)
        //    {
        //        player.hp = player.maxHp;
        //        Console.WriteLine($"체력을 {player.maxHp - cos}회복했습니다.");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"체력을 {player.hp - cos}회복했습니다.");
        //    }
        //}
    }

    public virtual void print()
    {
        Console.WriteLine($" {name}\t| {description}");
    }

    public bool getEquip()
    {
        return is_Equip;
    }
    public int getType()
    {
        return type;
    }
    public string getName()
    {
        return name;
    }
    public string getDesc()
    {
        return description;
    }


}