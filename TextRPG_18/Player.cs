using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Numerics;
using TextRPG;

public class Player
{
    public int level;
    public int exp { get; set; }
    public int maxExp;
    public string name { get; set; }
    public string job;
    public int hp { get; set; }
    public int maxHp { get; set; }
    public int gold { get; set; }
    public float atk { get; set; }
    public int def { get; set; }



    public Inventory inventory;
    public Weapon eWeapon;
    public Armor eArmor;
    public List<Quest> quests;


    public Player(string name)
    {
        level = 1;
        exp = 0;
        this.name = name;
        job = "용병";
        atk = 5;
        def = 5;
        hp = 100;
        maxHp = 150;
        gold = 1500;
        maxExp = level * 100;
        inventory = new Inventory();

        inventory.items.Add(new Weapon("녹슨 검", "오래된 검", 2, 50));
        inventory.items.Add(new Weapon("녹슨 갑옷", "오래된 갑옷", 4, 100));
        inventory.items.Add(new Consumption("하급 회복 포션", "체력을 약간 회복할 수 있는 포션", 50, 250));
        inventory.items.Add(new Consumption("하급 회복 포션", "체력을 약간 회복할 수 있는 포션", 50, 250));
        inventory.items.Add(new Consumption("하급 회복 포션", "체력을 약간 회복할 수 있는 포션", 50, 250));

        quests = new List<Quest>();
    }
    public Player(PlayerJsonModel playerData)
    {
        level = playerData.level;
        exp = playerData.exp;
        maxExp = playerData.maxExp;
        name = playerData.name;
        job = playerData.job;
        hp = playerData.hp;
        maxHp = playerData.maxHp;
        gold = playerData.gold;
        atk = playerData.atk;
        def = playerData.def;

        inventory = new(playerData.inventory);
        eWeapon = new(playerData.eWeapon);
        eArmor = new(playerData.eArmor);
    }

    public void Use_Item_Manager()
    {
        Console.WriteLine("[장비 관리 / 아이템 사용]");
        string str; int num;
        while (true)
        {
            inventory.printNumbering();
            Console.WriteLine("[나가려면 0을 입력하세요.]");
            Console.WriteLine("장비 아이템을 선택하면 장착/해제, 소비 아이템을 선택하면 사용합니다.");
            Console.Write("아이템을 선택하세요 : ");
            str = Console.ReadLine();

            int.TryParse(str, out num);
            if (0 <= num && num <= inventory.items.Count)
            {
                if (num == 0)
                {
                    break;
                }

                num -= 1;
                int checkType = inventory.items[num].getType();

                if (checkType == (int)ItemType.Consumables) 
                {
                    if (hp == maxHp)
                    {
                        Console.WriteLine($"이미 체력이 최대치입니다.");
                        Console.WriteLine($"=====================================================\n");
                    }
                    else
                    {
                        inventory.items[num].useConsume(this);
                        inventory.items.RemoveAt(num);
                    }

                }
                else if (checkType == (int)ItemType.Weapon || checkType == (int)ItemType.Armor) 
                {
                    if (inventory.items[num].getEquip()) // 아이템이 착용되어 있는지 확인
                    {
                        inventory.items[num].unEquip(this); // 장비 해제
                        Console.Write($"{inventory.items[num].getName()}을 장착 해제했습니다.");
                        Console.WriteLine($"=====================================================\n");
                    }
                    else
                    {
                        inventory.items[num].Equip(this);   // 장비 장착
                        Console.Write($"{inventory.items[num].getName()}을 장착했습니다.");
                        Console.WriteLine($"=====================================================\n");
                    }
                }

            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

        }
    }


    public void printStatus()
    {
        Console.WriteLine("---------------------");
        Console.WriteLine(
            $"LV : {level} ({exp}/{maxExp})\n" +
            $"{name} (job) \n" +
            $"공격력 : {atk} \n" +
            $"방어력 : {def} \n" +
            $"생명력 : {hp} / {maxHp} \n" +
            $"소지금 : {gold} G \n"
            );

        if (eWeapon != null)
        {
            Console.WriteLine($"무기 : {eWeapon.getName()}");
        }
        else { Console.WriteLine("무기 : 없음 "); }

        if (eArmor != null)
        {
            Console.WriteLine($"방어구 : {eArmor.getName()} \n");
        }
        else { Console.WriteLine("방어구 : 없음"); }
        Console.WriteLine("---------------------\n");
    }

    public void battel_DisplayPlayerInfo()
    {
        Console.WriteLine($"\n[내 정보]");
        Console.WriteLine($"Lv.{level}  {name} ({job}) ");
        Console.WriteLine($"HP {hp} / {maxHp}");
        Console.WriteLine($"ATK {atk}");
        Console.WriteLine($"DEF {def}");
        Console.WriteLine();
    }

    public void addItem(Item item)
    {
        inventory.items.Add(item);
    }

    public void Levelup()
    {
        if (exp >= maxExp)
        {
            exp -= maxExp;
            level++;
            maxExp = level * 100;
            maxHp += 10 ;
            atk += 2f;
            def += 1;
            Console.WriteLine($"{name} Level Up! {level}레벨 달성!");
            hp = maxHp;
            printStatus();
        }
        else
        {
            Console.WriteLine("경험치가 부족합니다.");
        }
    }

    public void Rest()
    {
        if (hp == maxHp)
        {
            Console.WriteLine($"이미 체력이 최대치입니다.");
            Console.WriteLine($"=====================================================\n");
            return;
        }
        gold -= 500;
        Console.Write($"체력을 ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{maxHp - hp}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($" 회복했습니다.");
        Console.Write($"현재 HP : ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{hp}\n");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"골드 지불 :");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"500");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"소지 골드 :");
        GameManager.printGold(this);
        Console.Write($"\n");
        hp = maxHp;
    }

    public void PrintQuests()
    {
        Console.WriteLine("[진행중인 퀘스트 목록]");
        Console.WriteLine("-------------");
        if (quests.Count == 0)
        {
            Console.WriteLine("진행중인 퀘스트가 없습니다.");
        }
        else
        {
            int n = 1;
            foreach (var q in quests)
            {
                Console.Write($"{n++}. ");
                q.Print();
            }
        }
        Console.WriteLine("-------------\n");
    }

    public int getmaxExp()
    {
        return maxExp;
    }
    public int getLevel()
    {
        return level;
    }
    public string getJob()
    {
        return job;
    }

}
