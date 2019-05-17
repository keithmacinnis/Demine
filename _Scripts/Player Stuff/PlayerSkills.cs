using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills
{

    PlayerCharacter player;

    public bool skillM1 { get; set; }
    public bool skillM2 { get; set; }

    public bool skillB1 { get; set; }
    public bool skillB2 { get; set; }

    public bool skillS1 { get; set; }
    public bool skillS2 { get; set; }

    public bool skillMB { get; set; }
    public bool skillMS { get; set; }
    public bool skillBS { get; set; }


    public PlayerSkills()
    {
    }

    public bool AllocateSkill(string skill)
    {
        switch (skill)
        {
            case "skillM1":
                if (player.stat_mind >= 10) return true;
                else return false;
            case "skillM2":
                if (player.stat_mind >= 30) return true;
                else return false;
            case "skillMB":
                if (player.stat_mind >= 15 && player.stat_body >= 15) return true;
                else return false;
            default:
                return false;
        }

    }
}
