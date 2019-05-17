using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trinket : IItem
{

    public int Health;
    public int Mana;

    public int FireResist;
    public int ColdResist;
    public int Armour;

    public List<Modifier> modifiers;

    public string DisplayString()
    {
        string dstring;
        dstring = RandomName() +"\n";
        if(Health != 0)
        {
            dstring += "Adds " + Health + " Health \n";
        }
        if (Mana != 0)
        {
            dstring += "Adds " + Mana + " Mana \n";
        }
        if (FireResist != 0)
        {
            dstring += "Adds " + FireResist + " Fire Resist \n";
        }
        if (ColdResist != 0)
        {
            dstring += "Adds " + ColdResist + " Cold Resist \n";
        }
        if (Armour != 0)
        {
            dstring += "Adds " + Armour + " Armour \n";
        }
        foreach (Modifier modifier in modifiers)
        {
            dstring += modifier.ToString() + "\n";
        }

        return dstring;
    }

    public static Trinket CreateNew(int tier)
    {
        Trinket trinket = new Trinket
        {
            modifiers = new List<Modifier>()
        };

        while (tier > 0)
        {
            int scale = Random.Range(1, tier);
            tier -= scale;
            int ran = Random.Range(1,10);
            if(ran < 5)
            {
                Modifier mod = Modifier.RandomModifier();
                mod.value *= scale;
                if(tier == 0)
                {
                    mod.value *= -1;
                }
                trinket.modifiers.Add(mod);
            }else if(ran < 6)
            {
                int hp = scale * Random.Range(5, 15);
                if(tier == 0) { hp *= -1; }
                trinket.Health += hp;
            }else if (ran < 7)
            {
                int mana = scale * Random.Range(10, 20);
                if (tier == 0) { mana *= -1; }
                trinket.Mana += mana;
            }
            else if (ran < 8)
            {
                int fres = scale * Random.Range(5, 10);
                if (tier == 0) { fres *= -1; }
                trinket.FireResist += fres;
            }
            else if (ran < 9)
            {
                int cres = scale * Random.Range(5, 10);
                if (tier == 0) { cres *= -1; }
                trinket.ColdResist += cres;
            }
            else if (ran < 10)
            {
                int armour = scale * Random.Range(15, 30);
                if (tier == 0) { armour *= -1; }
                trinket.Armour += armour;
            }
        }
        
        return trinket;
    }

    public static string RandomName()
    {
        return "Demonite Amulet";
    }

}
