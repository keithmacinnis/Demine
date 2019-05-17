using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pants : IItem {
    public int Health;
    public int Mana;

    public int FireResist;
    public int ColdResist;
    public int Armour;
    
    public string DisplayString()
    {
        string dstring;
        dstring = RandomName() + "\n";
        if (Health != 0)
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

        return dstring;
    }

    public static Pants CreateNew(int tier)
    {
        Pants pants = new Pants();

        for(int i = 0; i < tier; i++)
        {
            int ran = Random.Range(0, 5);
            if (ran < 1)
            {
                int hp = Random.Range(10, 15);
                pants.Health += hp;
            }
            else if (ran < 2)
            {
                int mana = Random.Range(10, 20);
                pants.Mana += mana;
            }
            else if (ran < 3)
            {
                int fres = Random.Range(7, 10);
                pants.FireResist += fres;
            }
            else if (ran < 4)
            {
                int cres = Random.Range(7, 10);
                pants.ColdResist += cres;
            }
            else if (ran < 5)
            {
                int armour = Random.Range(20, 30);
                pants.Armour += armour;
            }
        }

        return pants;
    }

    public static string RandomName()
    {
        return "Silky Smooth Pajamas";
    }
}
