using System;

public class Item
{
    protected bool is_Equip;
    protected int type;
    protected string name;
    protected string description;
    protected int efficacy;
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

    public virtual void useConsume(Player player)
    {
        if (type == (int)ItemType.Consumables)
        {
            Console.WriteLine($"{name}을 사용했습니다.");
            int cos = player.hp;
            player.hp += efficacy;
            if (player.hp >= player.maxHp)
            {
                player.hp = player.maxHp;
                Console.WriteLine($"체력을 {player.maxHp - cos}회복했습니다.");
            }
            else
            {
                Console.WriteLine($"체력을 {player.hp - cos}회복했습니다.");
            }
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

public class Consumption : Item
{
    public Consumption(string name, string des, int efficacy, int cost)
    {
        type = (int)ItemType.Consumables;
        this.name = name;
        this.description = des;
        this.efficacy = efficacy;
        this.cost = cost;
    }
    public Consumption(ItemJsonModel data)
    {
        type = data.type;
        name = data.name;
        description = data.description;
        efficacy = data.efficacy;
        cost = data.cost;
    }

    public override void print()
    {
        Console.Write($"{name} | 회복량: {efficacy} | {description}");
    }
    public int getEff()
    {
        return efficacy;
    }
    public override void useConsume(Player player)
    {
        if (type == (int)ItemType.Consumables)
        {
            if (player.hp == player.maxHp)
            {
                Console.WriteLine($"이미 체력이 최대치입니다.");
                Console.WriteLine($"=====================================================\n");
                return;
            }
            Console.WriteLine($"{name}을 사용했습니다.");
            int cos = player.hp;
            player.hp += efficacy;
            if (player.hp >= player.maxHp)
            {
                player.hp = player.maxHp;
                Console.Write($"체력을 ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{player.maxHp - cos}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($" 회복했습니다.");
                Console.Write($"현재 HP : ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{player.hp}\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"=====================================================\n");
            }
            else
            {
                Console.Write($"체력을 ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{player.hp - cos}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($" 회복했습니다.\n");
                Console.Write($"현재 HP : ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{player.hp}\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"=====================================================\n");
            }
        }
    }
}


