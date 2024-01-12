using System;
using System.Net.Http.Headers;
using System.Numerics;
using TextRPG_18;

public class Player
{
    public int level;
    public int exp { get; set; }
    public int maxExp;
    public string name { get; set;}
    public string job;
    public int hp { get; set; }
    public int gold { get; set; }
    public float atk { get; set; }
    public int def { get; set; }
    public int mp { get; set; }

    public float criticalChance { get; set; }
    public float criticalDamage { get; set; }

    public Inventory inventory;
    public Weapon eWeapon;
    public Armor eArmor;
    public List<Quest> quests;

    public Job SelectedClass { get; set; } //자식 클래스에 접근하기 위한 변수
    public int type = 1;  //클래스 타입

    public Player(string name)
    {
        level = 1;
        exp = 0;
        this.name = name;
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
        inventory.items.Add(new Armor("녹슨 갑옷", "오래된 갑옷", 4, 100));

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
            $"{name}   {job} \n" +
            $"공격력 : {atk} \n" +
            $"방어력 : {def} \n" +
            $"생명력 : {hp} \n" +
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
        Console.WriteLine("원하시는 이름을 설정해주세요.");

        string input = Console.ReadLine();
        name = input;

        Console.WriteLine($"환영합니다. {name}님의 캐릭터가 생성 되었습니다.");
    }
    public void battel_DisplayPlayerInfo()
    {
        Console.WriteLine($"\n[내 정보]");
        Console.WriteLine($"Lv.{level}  {name} ({job}) ");
        Console.WriteLine();
        Console.WriteLine($"HP {hp} ");
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
        if(exp >= maxExp)
        {
            exp -= maxExp;
            level++;
            maxExp = level * 100;
            atk += 0.5f;
            def += 1;
            Console.WriteLine($"{name} Level Up! {level}레벨 달성!");
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
        hp = playermax.maxHp;
        mp = playermax.maxmp;
        Console.WriteLine($"체력을 회복했습니다. (소지금 : {gold}) G");
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

    public int PlayerDamage()
    {
        Random random = new Random();

        int a = (int)(atk + random.NextDouble() * 0.2 + 0.9);


        return a;
    }

    public int PlaerDepance(int Damage)  //방어력 사용할수 있다면
    {
        return Damage - def;
    }

    //public string getJob()
    //{
    //    return job;
    //}
}
