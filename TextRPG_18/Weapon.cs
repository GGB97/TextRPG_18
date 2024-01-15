using System;
using System.Runtime.CompilerServices;

public class Weapon : Item
{
    float atk;

    public Weapon(string name, string des, float atk, int cost)
    {
        type = (int)ItemType.Weapon;
        this.name = name;
        description = des;
        this.atk = atk;
        this.cost = cost;
    }
    public Weapon(ItemJsonModel weaponData)
    {
        is_Equip = weaponData.is_Equip;
        type = weaponData.type;
        name = weaponData.name;
        description = weaponData.description;
        atk = weaponData.stat;
        cost = weaponData.cost;
    }

    public override void Equip(Player player)
    {
        base.Equip(player);
        if(player.eWeapon == null)  //무기를 끼고 있지 않다면
        {
            player.eWeapon = this;
        }
        else
        {
            player.eWeapon.unEquip(player);
            player.eWeapon = this;
        }

        player.atk += atk;
    }
    public override void unEquip(Player player)
    {
        base.unEquip(player);
        player.eWeapon = null;
        player.atk -= atk;
    }

    public override void print()
    {
        Console.Write($"{name} | 공격력+{atk} | {description}");
    }

    public float getAtk()
    {
        return atk;
    }
}
