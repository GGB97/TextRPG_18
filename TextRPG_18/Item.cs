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
            Console.WriteLine($"{name} 이(가) 해제 되었습니다.");

        }
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


