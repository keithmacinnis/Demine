using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillDisplay : MonoBehaviour {

    public PlayerCharacter PC;

    public Text Mind_Display;
    public Text Body_Display;
    public Text Soul_Display;
    public Text DemoniteDisplay;

    public void LevelMind()
    {
        PC.IncreaseStat("mind");
        Mind_Display.text = "Mind: " + PC.stat_mind;
        DemoniteDisplay.text = "Demonite: " + PC.Resource;
    }
    public void LevelBody()
    {
        PC.IncreaseStat("body");
        Body_Display.text = "Body: " + PC.stat_body;
        DemoniteDisplay.text = "Demonite: " + PC.Resource;
    }
    public void LevelSoul()
    {
        PC.IncreaseStat("soul");
        Soul_Display.text = "Soul: " + PC.stat_soul;
        DemoniteDisplay.text = "Demonite: " + PC.Resource;
    }

    // Use this for initialization
    void Start () {

        PC.levelUp();

        Mind_Display.text = "Mind: " + PC.stat_mind;
        Body_Display.text = "Body: " + PC.stat_body;
        Soul_Display.text = "Soul: " + PC.stat_soul;
        DemoniteDisplay.text = "Demonite: " + PC.Resource;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
