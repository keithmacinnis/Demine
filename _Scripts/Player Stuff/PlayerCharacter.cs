using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

    public int Health { get; set; }
    public int Mana { get; set; }
    public int Resource { get; set; }

    private List<Modifier> Mod_IncreasedDamage = new List<Modifier>();
    private List<Modifier> Mod_MoreDamage = new List<Modifier>();
    private List<Modifier> Mod_AddedDamage = new List<Modifier>();
    private List<Modifier> Mod_AdditionalDamage = new List<Modifier>();

    private int FireResist;
    private int ColdResist;

    private int Armour;

    public int stat_body;
    public int stat_mind;
    public int stat_soul;

    public int skillPoints { get; set; }
    public int level { get; set; }

    private PlayerSkills skills;

    public PlayerInventory Inventory;

    public DamageSource DamageCalculation(DamageSource source)
    {

        double totalInc = 1;
        double totalMore = 1;
        double totalAdded = 0;
        double totalAdditional = 0;

        foreach (Modifier mod in Mod_IncreasedDamage)
        {
            if (source.CompareTags(mod))
            {
                totalInc += (((double)mod.value)/100);
            }
        }
        foreach (Modifier mod in Mod_MoreDamage)
        {
            if (source.CompareTags(mod))
            {
                totalMore *= 1 + (((double)mod.value)/100);
            }
        }
        foreach (Modifier mod in Mod_AddedDamage)
        {
            if (source.CompareTags(mod))
            {
                totalAdded += mod.value;
            }
        }
        foreach (Modifier mod in Mod_AdditionalDamage)
        {
            if (source.CompareTags(mod))
            {
                totalAdditional += mod.value;
            }
        }
        double finalDamage = ((source.value + totalAdded) * totalInc * totalMore) + totalAdditional;
        source.value =  (int) finalDamage;
        return source;
    }

    public int TakeDamage(TakeDamageSource source)
    {

        int totalDamage = 0;

        foreach (DamageSource dmgSource in source.DamageSources)
        {
            if (dmgSource.tags.Contains("fire"))
            {
                totalDamage += dmgSource.value - FireResist;
            }
            if (dmgSource.tags.Contains("cold"))
            {
                totalDamage += dmgSource.value - ColdResist;
            }
            if (dmgSource.tags.Contains("physical"))
            {
                totalDamage += (dmgSource.value * dmgSource.value) / (dmgSource.value + Armour);
            }
        }

        //print(totalDamage);
        return totalDamage;
    }

    public void levelUp()
    {
        level++;
        skillPoints += 3;
    }

    public bool IncreaseStat(string stat)
    {
        if (Resource >= 3)
        {
            switch (stat)
            {
                case "body":
                    stat_body++;
                    break;
                case "mind":
                    stat_mind++;
                    break;
                case "soul":
                    stat_soul++;
                    break;
                default:
                    return false;
            }
            Resource -= 3;
            CalculateStats();
            return true;
        }
        return false;
    }

    public void CalculateStats()
    {
        Health = 100 + 3 * stat_body;
        Mana = 100 + 5 * stat_mind;

        Mod_AddedDamage = new List<Modifier>();
        Mod_IncreasedDamage = new List<Modifier>();
        Mod_MoreDamage = new List<Modifier>();
        Mod_AdditionalDamage = new List<Modifier>();

        foreach (Modifier modifier in Inventory.Weapon.modifiers)
        {
            if (modifier.More) { Mod_MoreDamage.Add(modifier); }
            if (modifier.Added) { Mod_AddedDamage.Add(modifier); }
            if (modifier.Increased) { Mod_IncreasedDamage.Add(modifier); }
            if (modifier.Additional) { Mod_AdditionalDamage.Add(modifier); }
        }

        foreach (Modifier modifier in Inventory.TrinketA.modifiers)
        {
            if (modifier.More) { Mod_MoreDamage.Add(modifier); }
            if (modifier.Added) { Mod_AddedDamage.Add(modifier); }
            if (modifier.Increased) { Mod_IncreasedDamage.Add(modifier); }
            if (modifier.Additional) { Mod_AdditionalDamage.Add(modifier); }
        }

        Health += Inventory.TrinketA.Health;
        Mana += Inventory.TrinketA.Mana;
        Armour += Inventory.TrinketA.Armour;
        ColdResist += Inventory.TrinketA.ColdResist;
        FireResist += Inventory.TrinketA.FireResist;

        Health += Inventory.Body.Health;
        Mana += Inventory.Body.Mana;
        Armour += Inventory.Body.Armour;
        ColdResist += Inventory.Body.ColdResist;
        FireResist += Inventory.Body.FireResist;

        Modifier soul_increase = new Modifier
        {
            value = 2 * stat_soul,
            Increased = true,
            tags = new List<string>()
        };
        soul_increase.tags.Add("all");
        

        Mod_IncreasedDamage.Add(soul_increase);
    }



    private void Start()
    {
        Inventory = new PlayerInventory
        {
            Weapon = Weapon.CreateNew(3),
            TrinketA = Trinket.CreateNew(3),
            Body = Pants.CreateNew(3)
        };
        CalculateStats();
        Resource = 0;
    }

    //static instance of the player character,
    //can be access from anywhere
    public static PlayerCharacter instance = null;

    //initialization
    void Awake()
    {
        //if it doesn't exist
        if (instance == null)
        {
            //set the instance to the current object (this)
            instance = this;
        }

        //making sure there is only one instance
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //don't destroy on load
        DontDestroyOnLoad(gameObject);
    }


}
