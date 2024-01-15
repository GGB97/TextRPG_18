using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Numerics;
using TextRPG_18;

public class Player
{
    public int level;
    public int exp { get; set; }
    public int maxExp;
    public string name { get; set; }
    
    public int hp { get; set; }
    public int maxHp { get; set; }
    public int gold { get; set; }
    public float atk { get; set; }
    public int def { get; set; }
    public int mp { get; set; }

    public int criticalChance { get; set; }
    public int criticalDamage { get; set; }

    public int Avoidance { get; set; } //회피율
    public int MP_Recovery { get; set; } //마나 회복율
    public Job SelectedClass { get; set; } //자식 클래스에 접근하기 위한 변수
    public Inventory inventory;
    public Weapon eWeapon;
    public Armor eArmor;
    public List<Quest> quests;

    
    public int type = 1;  //클래스 타입

    public Player(string name)
    {
        level = 1;
        exp = 0;
        this.name = name;
        atk = 5;
        def = 5;
        hp = 10;
        maxHp = 150;
        atk = 0;
        def = 0;
        mp = 0;
        hp = 100;
        criticalChance = 0;
        criticalDamage = 0;
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
        //job = playerData.job;
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
            $"{name}   {SelectedClass.name} \n" +
            $"공격력 : {atk} \n" +
            $"방어력 : {def} \n" +
            $"생명력 : {hp} / {maxHp} \n" +
            $"마나 : {mp} \n"+
            $"치명타 확률 : { criticalChance } \n" +
            $"치명타 피해 : { criticalDamage } \n" +
            "\n"+
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

    public void CreateCharacter() //!!!!!!!!!!캐릭터 생성!!!!!!!!!!!!
    {
        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.Write("원하시는 이름을 설정해주세요. : ");

        string input = Console.ReadLine();
        name = input;

        Console.WriteLine($"환영합니다. {name}님의 캐릭터가 생성 되었습니다.");
    }
    public void battel_DisplayPlayerInfo()
    {
        Console.WriteLine($"\n[내 정보]");
        Console.WriteLine($"Lv.{level}  {name} ({SelectedClass.name}) ");
        Console.WriteLine($"HP {hp} / {maxHp}");
        Console.WriteLine($"MP {mp}");
        Console.WriteLine($"ATK {atk}");
        Console.WriteLine($"DEF {def}");
        Console.WriteLine($"CRP {criticalChance}");
        Console.WriteLine($"CRD {criticalDamage}");

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
        hp = maxHp;
        Console.Write($"현재 HP : ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{hp}\n");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"마력을 ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write($"{playermax.maxMp - mp}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($" 회복했습니다.");
        mp = playermax.maxMp;

        Console.Write($"골드 지불 :");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"500");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"소지 골드 :");
        TextRPG.GameManager.printGold(this);
        Console.Write($"\n");
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

    public int PlayerDamage() //공격력(치명타 계산까지)
    {
        Random random = new Random();

        int a = (int)atk;

        if(random.Next(0,100) < criticalChance) //크리티컬 확률계산
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("치명타 발동!!");
            Console.ResetColor();

            //치명타
            float b = (float)criticalDamage / 100;

            a += (int)(atk * b);  
        }
        return a;
    }

    public int PlaerDepance(int Damage)  //방어력 사용할수 있다면
    {
        return Damage - def;
    }

    public bool Avoidance_percentage(int percentage)  //회피 확률계산
    {
        Random rend = new Random();

        if (rend.Next(0, 100) < percentage)
        {
            return true;
        }
        else { return false; }
    }

    public void Recovery()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"{name} (이)의 마나가 회복되었습니다. : {mp} -> {mp + MP_Recovery}");
        Console.ResetColor();

        mp += MP_Recovery;
    }

}
