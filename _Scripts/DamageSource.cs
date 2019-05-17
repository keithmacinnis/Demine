using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource
{

    //needs to always include "all"
    public List<string> tags;

    public int value;


    public DamageSource()
    {
    }

    public bool CompareTags(Modifier mod)
    {
        foreach (string tag in tags)
        {
            foreach (string modTag in mod.tags)
            {
                if (tag == modTag) return true;
            }
        }
        return false;
    }


    public static DamageSource WeaponDamage()
    {
        DamageSource src = new DamageSource
        {
            value = Random.Range(8, 12),
            tags = new List<string>
            {
                "physical",
                "all"
            }
        };
        return src;
    }

}
