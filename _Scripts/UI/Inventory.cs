using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public PlayerCharacter PC;

    public Text WeaponText;
    public Text TrinketText;
    public Text PantsText;

    public Text DemoniteText;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        WeaponText.text = PC.Inventory.Weapon.DisplayString();
        TrinketText.text = PC.Inventory.TrinketA.DisplayString();
        PantsText.text = PC.Inventory.Body.DisplayString();
        DemoniteText.text = "Demonite: "+PC.Resource.ToString();
    }
}
