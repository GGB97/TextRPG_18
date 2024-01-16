using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Numerics;
using TextRPG;
using TextRPG_18;

public class Player
{
    public int level;
    public int exp { get; set; }
    public int maxExp;
    public string name { get; set; }

    public int hp { get; set; }
    public int maxHp { get; set; }
    public int mp { get; set; }
    public int maxMp { get; set; }
    public int gold { get; set; }
    public float atk { get; set; }
    public int def { get; set; }

    public int criticalChance { get; set; }
    public int criticalDamage { get; set; }

    public int statPoint { get; private set; }

    public int Avoidance { get; set; } //회피율
    public int MP_Recovery { get; set; } //마나 회복율
    public Job SelectedClass { get; set; } //자식 클래스에 접근하기 위한 변수
    public Inventory inventory;
    public Weapon eWeapon;
    public Armor eArmor;
    public QuestList quests;

    public Player(string name)
    {
        level = 1;
        exp = 0;
        this.name = name;
        atk = 5;
        def = 5;
        hp = 10;
        atk = 0;
        def = 0;
        mp = 0;
        maxMp =
        hp = 100;
        criticalChance = 0;
        criticalDamage = 0;
        gold = 1500;
        maxExp = level * 100;
        statPoint = 1;

        inventory = new Inventory();
        inventory.items.Add(new Weapon("녹슨 검", "오래된 검", 5, 50));
        inventory.items.Add(new Armor("녹슨 갑옷", "오래된 갑옷", 3, 100));
        inventory.items.Add(new Consumption("하급 회복 포션", "체력을 약간 회복할 수 있는 포션", 30, 500));
        inventory.items.Add(new Consumption("하급 회복 포션", "체력을 약간 회복할 수 있는 포션", 30, 500));
        inventory.items.Add(new Consumption("하급 회복 포션", "체력을 약간 회복할 수 있는 포션", 30, 500));

        quests = new QuestList();
    }

    public Player(PlayerJsonModel playerData)
    {
        level = playerData.level;
        exp = playerData.exp;
        maxExp = playerData.maxExp;
        name = playerData.name;

        hp = playerData.hp;
        maxHp = playerData.maxHp;
        mp = playerData.mp;
        maxMp = playerData.maxMp;
        gold = playerData.gold;
        atk = playerData.atk;
        def = playerData.def;

        statPoint = playerData.statPoint;

        switch (playerData.job)
        {
            case JobType.Berserker:
                SelectedClass = new Warrior();
                break;
            case JobType.DragonKnight:
                SelectedClass = new Kinght();
                break;
            case JobType.Mage:
                SelectedClass = new Mage();
                break;

        }

        criticalChance = playerData.criticalChance;
        criticalDamage = playerData.criticalDamage;
        Avoidance = playerData.Avoidance;
        MP_Recovery = playerData.MP_Recovery;

        inventory = new(playerData.inventory);
        eWeapon = new(playerData.eWeapon);
        eArmor = new(playerData.eArmor);
        quests = new(playerData.quests);

        playermax.atk = this.atk + eWeapon.getAtk();
        playermax.dfs = this.def + eArmor.getDef();
        playermax.CRP = SelectedClass.criticalChance;
        playermax.CRD = SelectedClass.criticalDamage;
    }

    public void Use_Item_Manager()
    {
        Console.WriteLine("[장비 관리 / 아이템 사용]");
        string str; int num;
        while (true)
        {
            inventory.printNumbering();
            Console.WriteLine("장비 아이템을 선택하면 장착/해제, 소비 아이템을 선택하면 사용합니다.");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write($"{name} : ");
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
                GameManager.printError(str);
            }

        }
    }


    public void printStatus()
    {
        Console.WriteLine("[상태창]\n");
        Console.WriteLine("---------------------");
        Console.WriteLine(
            $"LV : {level} ({exp}/{maxExp})\n" +
            $"{name}   {SelectedClass.name} \n" +
            $"공격력 : {atk} \n" +
            $"방어력 : {def} \n" +
            $"생명력 : {hp} / {maxHp} \n" +
            $"마나 : {mp} / {maxMp} \n" +
            $"치명타 확률 : {criticalChance} \n" +
            $"치명타 피해 : {criticalDamage} \n" +
            $"SP : {statPoint}" + 
            "\n" +
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
        Console.WriteLine($"Lv.{level}  {name} ({SelectedClass.name}) ");
        Console.WriteLine($"HP {hp} / {maxHp}");
        Console.WriteLine($"MP {mp} / {maxMp}");
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
            maxExp = level * 150;
            maxHp += 5;
            atk += 2f;
            def += 1;
            Console.WriteLine($"{name} Level Up! {level}레벨 달성!");
            Console.WriteLine($"마나와 체력이 전부 회복되었다!");
            hp = maxHp;
            mp = maxMp;
            statPoint += 1; // Test 용으로 기본값 1
            printStatus();
        }
        else
        {
            Console.WriteLine("경험치가 부족합니다.");
        }
    }

    public void AllocateStats()
    {
        while (true)
        {
            printStatus();
            Console.WriteLine($"1. HP (+ {playerConst.pointHp})");
            Console.WriteLine($"2. MP (+ {playerConst.pointMp})");
            Console.WriteLine($"3. ATK (+ {playerConst.pointAtk})");
            Console.WriteLine($"4. DEF (+ {playerConst.pointDef})");
            Console.WriteLine("0. 나가기\n");

            Console.Write($"{name} : ");
            string str = Console.ReadLine();

            if (str == "1" || str == "2" || str == "3" || str == "4")
            {
                UsePoint(str);
                break;
            }
            else if (str == "0")
            {
                break;
            }
            else
            {
                GameManager.printError(str);
            }
        }
    }

    void UsePoint(string str)
    {
        if (statPoint > 0)
        {
            Console.WriteLine("\n능력치가 증가 되었습니다.");
            switch (str)
            {
                case "1":
                    maxHp += playerConst.pointHp;
                    break;
                case "2":
                    maxMp += playerConst.pointMp;
                    break;
                case "3":
                    atk += playerConst.pointAtk;
                    break;
                case "4":
                    def += playerConst.pointDef;
                    break;
            }
            statPoint--;
        }
        else
        {
            Console.WriteLine("스탯 포인트가 부족합니다.");
        }
        GameManager.PressEnter();
        
    }

    public void Rest()
    {
        if (hp == maxHp)
        {
            if (mp < maxMp)
            {
                if (gold >= 150)
                {
                    gold -= 150;
                    Console.Write($"마나를 ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{maxMp - mp}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($" 회복했습니다.");
                    mp = maxMp;
                    Console.Write($"현재 MP : ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"{mp} / {maxMp}\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.Write($"골드 지불 :");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"150");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"소지 골드 :");
                    TextRPG.GameManager.printGold(this);
                    Console.Write($"\n");
                }
                else
                {
                    Console.WriteLine($"골드가 부족합니다.");
                    Console.WriteLine($"=====================================================\n");
                }
            }
            else
            {
                Console.WriteLine($"이미 체력과 마나가 최대치입니다.");
                Console.WriteLine($"=====================================================\n");
            }
            return;
        }
        if (gold >= 300)
        {
            gold -= 300;
            Console.Write($"체력을 ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{maxHp - hp}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" 회복했습니다.");
            hp = maxHp;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"마력을 ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{maxMp - mp}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" 회복했습니다.");
            mp = maxMp;
            Console.Write($"현재 체력: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"[");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{hp}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"/");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{maxHp}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"] ");
            Console.Write($"현재 마나: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"[");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{mp}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"/");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{maxMp}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"]\n\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"골드 지불 :");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"-300");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"소지 골드 :");
            TextRPG.GameManager.printGold(this);
            Console.Write($"\n");
        }
        else
        {
            Console.WriteLine($"골드가 부족합니다.");
            Console.WriteLine($"=====================================================\n");
        }
    }

    public void PrintQuests()
    {
        Console.WriteLine("[진행중인 퀘스트 목록]");
        Console.WriteLine("-------------------------------------");
        if (quests.quests.Count == 0)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("진행중인 퀘스트가 없습니다.");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
        }
        else
        {
            int n = 1;
            foreach (var q in quests.quests)
            {
                Console.Write($"{n++}. ");
                q.Print();
            }
        }
        Console.WriteLine("-------------------------------------\n");
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

        if (random.Next(0, 100) < criticalChance) //크리티컬 확률계산
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" 치명타 발동!!\n");
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
        int save_mp = mp;
        mp += MP_Recovery;
        if (mp >= maxMp)
        {
            mp = maxMp;
        }
        Console.Write($"{name} (이)의 마나가 회복되었습니다. : ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write($"{mp - save_mp}");
        Console.ResetColor();
        Console.Write($" 회복되었습니다. : ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write($"{save_mp}");
        Console.ResetColor();
        Console.Write(" -> ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(mp);
        Console.ResetColor();


    }

}
