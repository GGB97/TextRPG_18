using System;
using TextRPG;

public class Shop
{
    public List<Item> items = new List<Item>();

    public Shop()
    {
        items.Add(new Armor("수련자 갑옷", "수련에 도움을 주는 갑옷", 10, 1000));
        items.Add(new Armor("스파르타의 갑옷", "스파르타의 전사들이 사용하던 갑옷", 25, 4500));
        items.Add(new Armor("아다만틴 갑옷", "언더다크의 대장간에서 만들어진 갑옷", 50, 10000));
        Console.WriteLine();
        items.Add(new Weapon("청동 메이스", "어디선가 사용됐던거 같은 메이스", 10, 1500));
        items.Add(new Weapon("스파르타의 창", "스파르타의 전사들이 사용하던 창", 35, 5500));
        items.Add(new Weapon("영혼 포획의 장갑", "발더스 게이트에서 주워왔습니다", 100, 12000));
        items.Add(new Consumption("하급 회복 포션", "체력을 약간 회복할 수 있는 포션", 30, 500));
        items.Add(new Consumption("중급 회복 포션", "체력을 적당히 회복할 수 있는 포션", 75, 700));
        items.Add(new Consumption("고급 회복 포션", "체력을 대폭 회복할 수 있는 포션", 120, 1000));
    }

    // 질문 : 아이템이 팔렸는지 확인하는 조건을 item클래스 자체에 bool값으로 판단하는게 나은지
    // 상점 내부에서 몇번 상품이 팔렸는지 확인하는 리스트나 배열을 추가하는게 나은지 궁금합니다.
    // item 클래스에 넣으면 다른곳에서는 사용하지 않는데 낭비하는 느낌이 드는것 같기도 하고
    // shop에 넣어도 뭔가 깔끔하게 들어가지 않고 어거지로 넣는 느낌이라
    public void buy(Player player)
    {
        string str; int num;
        while (true)
        {
            Console.WriteLine("[아이템 구매]");
            Console.WriteLine();
            Console.Write($"소지 골드 :");
            GameManager.printGold(player);
            print();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write($"{player.name} : ");
            str = Console.ReadLine();

            if (int.TryParse(str, out num))
            {
                num -= 1;
                if (0 <= num && num < items.Count)
                {
                    if (player.gold >= items[num].cost) // player가 아이템을 살 수 있는지
                    {
                        player.addItem(items[num]);
                        player.gold -= items[num].cost;
                        Console.WriteLine($"{items[num].getName()}을(를) 구매했습니다.");
                        Console.Write($"골드 지불 :");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"{-items[num].cost}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($"소지 골드 :");
                        GameManager.printGold(player);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("소지금이 부족합니다!!");
                        Console.WriteLine();
                    }
                }
                else if (num == -1)
                {
                    break;
                }
            }
            else
            {
                GameManager.printError(str);
            }
        }
    }

    public void sell(Player player)
    {
        string str; int num;
        while (true)
        {
            Console.WriteLine("[아이템 판매]");
            Console.Write($"소지 골드 :");
            GameManager.printGold(player);
            player.inventory.printGold();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.Write($"{player.name} : ");
            str = Console.ReadLine();

            if (int.TryParse(str, out num))
            {
                num -= 1;
                if (0 <= num && num < player.inventory.items.Count)
                {
                    if (player.inventory.items[num].getEquip()) // 판매하려는 아이템이 장착되어 있다면.
                        player.inventory.items[num].unEquip(player);

                    player.gold += (int)(player.inventory.items[num].cost * 0.85f);
                    Console.WriteLine($"{player.inventory.items[num].getName()} 이(가) 판매 되었습니다.");
                    Console.Write($"골드 획득 :");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"+{(int)(player.inventory.items[num].cost * 0.85f)}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"소지 골드 :");
                    GameManager.printGold(player);
                    player.inventory.items.RemoveAt(num);
                    Console.WriteLine() ;
                }
                else if (num == -1)
                {
                    break;
                }
            }
            else
            {
                GameManager.printError(str);
            }
            
        }
    }

    public void print()
    {
        int num = 1;

        Console.WriteLine("------------------------------------------");
        foreach (Item item in items)
        {
            Console.Write($"{num++}. ");
            item.print();
            Console.Write($" | {item.cost} G");
            Console.WriteLine();
        }
        Console.WriteLine("------------------------------------------");
    }

    public void print2()
    {
        Console.WriteLine("[상품 목록]");
        Console.WriteLine();
        Console.WriteLine("------------------------------------------");
        foreach (Item item in items)
        {
            Console.Write("- ");
            item.print();
            Console.Write($" | {item.cost} G");
            Console.WriteLine();
        }
        Console.WriteLine("------------------------------------------");
    }
}
