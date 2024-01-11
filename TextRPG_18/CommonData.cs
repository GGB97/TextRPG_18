using System;
enum MenuType
{
    STATUS = 1,
    INVENTORY,
    STORE,
    DUNGEON,
    REST,
    SAVE = 9,
    EXIT = 0
}

enum ItemType
{
    Weapon,
    Armor,
    consumables
}

struct playerConst  // 그냥 상수 집합
{
    public const int maxHp = 100;
    public const float dmgRange = 0.1f;
}
