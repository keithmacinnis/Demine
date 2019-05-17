using System.Collections.Generic;
using UnityEngine;

public class Modifier
{

    public bool More { get; set; }
    public bool Added { get; set; }
    public bool Increased { get; set; }
    public bool Additional { get; set; }

    public int value { get; set; }

    public List<string> tags { get; set; }

    public override string ToString()
    {
        if (More)
        {
            return value + "% More Damage";
        }
        if (Added)
        {
            return value + " Added Damage";
        }
        if (Increased)
        {
            return value + "% Increased Damage";
        }
        if (Additional)
        {
            return value + " Additional Damage";
        }

        return null;
    }

    public static Modifier RandomModifier()
    {
        Modifier RanMod = new Modifier();
        int type = Random.Range(1,4);
        switch (type)
        {
            case 1:
                RanMod.More = true;
                RanMod.value = Random.Range(5,15);
                break;
            case 2:
                RanMod.Added = true;
                RanMod.value = Random.Range(5,20);
                break;
            case 3:
                RanMod.Additional = true;
                RanMod.value = Random.Range(10,30);
                break;
            case 4:
                RanMod.Increased = true;
                RanMod.value = Random.Range(10,30);
                break;
        }
        RanMod.tags = new List<string>();
        RanMod.tags.Add("physical");
        return RanMod;
    }
}
