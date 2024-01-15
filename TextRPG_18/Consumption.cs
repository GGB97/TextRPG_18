using System;


public class Consumption : Item
{
    protected int efficacy;
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
        is_Equip = data.is_Equip;
        type = data.type;
        name = data.name;
        description = data.description;
        efficacy = (int)data.stat;
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
            if (player.hp == playermax.maxHp)
            {
                Console.WriteLine($"이미 체력이 최대치입니다.");
                Console.WriteLine($"=====================================================\n");
                return;
            }
            Console.WriteLine($"{name}을 사용했습니다.");
            int cos = player.hp;
            player.hp += efficacy;
            if (player.hp >= playermax.maxHp)
            {
                player.hp = playermax.maxHp;
                Console.Write($"체력을 ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{playermax.maxHp - cos}");
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
