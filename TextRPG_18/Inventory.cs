using System;
using System.Collections.Generic;

public class Inventory
{
    public List<Item> items = new List<Item>(); // 인벤토리를 리스트로 하는건 낭비같긴 하지만 최대 용량을 생각하기 귀찮아서..

    public Inventory()
    {
        items = new List<Item>();
    }
    public Inventory(InventoryJsonModel data)
    {
        foreach (var item in data.items)
        {
            if (item.type == (int)ItemType.Weapon)    // type으로 구분
            {
                items.Add(new Weapon(item));
            }
            else if (item.type == (int)ItemType.Armor)
            {
                items.Add(new Armor(item));
            }
            else if (item.type == (int)ItemType.Consumables)
            {
                items.Add(new Consumption(item));
            }
        }
    }

    public void print()
    {
        Console.WriteLine("[아이템 목록]");
        Console.WriteLine();
        Console.WriteLine("-----------------");
        foreach (Item item in items)
        {
            if (item.getEquip())
                Console.Write("[E]");

            item.print();
            Console.WriteLine();
        }
        Console.WriteLine("-----------------");
        Console.WriteLine();
    }

    public void printNumbering()
    {
        if (items != null)
        {
            int num = 1;
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine("-----------------");
            foreach (Item item in items)
            {
                Console.Write($"{num++}. ");

                if (item.getEquip())
                    Console.Write("[E]");

                item.print();
                Console.WriteLine();
            }
            Console.WriteLine("-----------------");
        }
        else
        {
            Console.WriteLine("아이템이 없습니다.");
            return;
        }
    }

    public void printGold()
    {
            int num = 1;
            Console.WriteLine("-----------------");
            foreach (Item item in items)
            {
                Console.Write($"{num++}. ");
                item.print();
                Console.Write($" | {item.cost} G");
                Console.WriteLine();
            }
            Console.WriteLine("-----------------");
    }



}
