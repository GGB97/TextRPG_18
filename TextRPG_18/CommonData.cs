using System;

public enum JobType
{
    Berserker,
    DragonKnight,
    Mage
}
enum MenuType
{
    EXIT,
    STATUS,
    INVEN,
    STORE,
    DUNGUEON,
    REST,
    QUEST,
    SAVE = 9
}
enum MonsterType
{
    Monster,
    Goblin,
    Goblin_Frist,
    Orc,
    LizardMan,
    Vampire_bat,
    Troll
}

enum ItemType
{
    Weapon,
    Armor,
    Consumables
}

struct playermax
{
    public static float atk;
    public static int dfs;
    public static int CRP;
    public static int CRD;
}

public struct playerConst
{
    public const int pointHp = 10;
    public const int pointMp = 5;
    public const float pointAtk = 1;
    public const int pointDef = 2;
}
