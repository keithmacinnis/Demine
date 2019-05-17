using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy{

    public int Health;

    public int ColdResist;
    public int FireResist;

    public int Armour;


    public void TakeDamage(DamageSource src)
    {
        if (src.tags.Contains("fire")) src.value -= FireResist;
        if (src.tags.Contains("cold")) src.value -= ColdResist;
        if (src.tags.Contains("physical")) src.value -= (src.value - (src.value * src.value) / (src.value + Armour));

        Health -= src.value;
    }

    public TakeDamageSource DealDamage(int phys, int cold, int fire)
    {
        int n = 0;
        TakeDamageSource src = new TakeDamageSource()
        {
            DamageSources = new List<DamageSource>()
        };
        if (phys != 0)
        {
            src.DamageSources.Add(new DamageSource()
            {
                value = phys,
                tags = new List<string>()
            });
            src.DamageSources[n].tags.Add("physical");
            src.DamageSources[n].tags.Add("all");
            n++;
        }
        if (cold != 0)
        {
            src.DamageSources.Add(new DamageSource()
            {
                value = cold,
                tags = new List<string>()
            });
            src.DamageSources[n].tags.Add("physical");
            src.DamageSources[n].tags.Add("all");
            n++;
        }
        if (fire != 0)
        {
            src.DamageSources.Add(new DamageSource()
            {
                value = fire,
                tags = new List<string>()
            });
            src.DamageSources[n].tags.Add("physical");
            src.DamageSources[n].tags.Add("all");
            n++;
        }

        return src;
    }
}
