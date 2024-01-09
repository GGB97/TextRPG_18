using System;
using System.Net.Http.Headers;
using System.Numerics;

public class Player
{
    int level;
    public int exp { get; set; }
    int maxExp;
    public string name { get; set;}
    string job;
    public int hp { get; set; }
    public int gold { get; set; }
    public float atk { get; set; }
    public int def { get; set; }

    public Inventory inventory;
    public Weapon eWeapon;
    public Armor eArmor;


    public Player(string name)
    {
        level = 1;
        exp = 0;
        this.name = name;
        job = "용병";
        atk = 2;
        def = 5;
        hp = 100;
        gold = 1500;
        maxExp = level * 100;
        inventory = new Inventory();

        inventory.items.Add(new Weapon("녹슨 검", "오래된 검", 2, 50));
        inventory.items.Add(new Armor("녹슨 갑옷", "오래된 갑옷", 4, 100));
    }
    public Player(PlayerJsonModel playerData)
    {
        level = playerData.level;
        exp = playerData.exp;
        maxExp = playerData.maxExp;
        name = playerData.name;
        job = playerData.job;
        hp = playerData.hp;
        gold = playerData.gold;
        atk = playerData.atk;
        def = playerData.def;

        inventory = new(playerData.inventory);
        eWeapon = new(playerData.eWeapon);
        eArmor = new(playerData.eArmor);
    }

    public void EquipManager()
    {
        Console.WriteLine("[장비 관리]");

        inventory.printNumbering();
        string str; int num;
        while (true)
        {
            Console.Write("장착/해제 할 장비 : ");
            str = Console.ReadLine();

            int.TryParse(str, out num);
            if (0 <= num && num <= inventory.items.Count)
            {
                num -= 1;
                if (inventory.items[num].getEquip()) // 아이템이 착용되어 있는지 확인
                {
                    inventory.items[num].unEquip(this); // 장비 해제
                    break;
                }
                else
                {
                    inventory.items[num].Equip(this);	// 장비 장착
                    break;
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
            $"생명력 : {hp} \n" +
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

    public void addItem(Item item)
    {
        inventory.items.Add(item);
    }

    public void Levelup()
    {
        if(exp >= maxExp)
        {
            exp -= maxExp;
            level++;
            maxExp = level * 100;
            atk += 0.5f;
            def += 1;
            Console.WriteLine("레벨이 올랐습니다");
            printStatus();
        }
        else
        {
            Console.WriteLine("경험치가 부족합니다.");
        }
    }

    public void Rest()
    {
        if (hp == 0)
        {
            gold -= 1000;
        }
        else
        {
            gold -= 500;
        }
        hp = playerConst.maxHp;
        Console.WriteLine($"체력을 회복했습니다. (소지금 : {gold}) G");
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
