using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : IItem
{
    public List<Modifier> modifiers;
    public int tier;
    public string WeaponName;


    public string DisplayString()
    {
        string displayString = WeaponName + "\n";
        foreach(Modifier modifier in modifiers)
        {
            displayString += modifier.ToString() + "\n";
        }
        return displayString;
    }






    public static Weapon CreateNew(int tier)
    {

        Weapon weapon = new Weapon
        {
            tier = tier,
            modifiers = new List<Modifier>()
        };
        for (int i = 0; i < tier; i++)
        {
            weapon.modifiers.Add(Modifier.RandomModifier());
        }

        weapon.WeaponName = RandomName();

        return weapon;
    }

    public static string RandomName()
    {
        return "Pick of Doom";
    }
    
}
