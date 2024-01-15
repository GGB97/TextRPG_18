using System;

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
    public static int maxHp;
    public static int maxMp;
    public static int atk;
    public static int dfs;
    public static int CRP;
    public static int CRD;
}

//struct playerConst
//{
//    public const int maxHp = 100;
//}
