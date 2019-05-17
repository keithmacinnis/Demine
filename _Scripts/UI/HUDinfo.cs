using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDinfo : MonoBehaviour {

    public PlayerManager PlayerManager;
    public PlayerCharacter PlayerCharacter;

    public Text HealthDisplay;
    public Text ManaDisplay;

    //public Text ButtonText;


	// Update is called once per frame
	void Update () {
        HealthDisplay.text = "Health: "+PlayerManager.CurrentHealth + "/" + PlayerCharacter.Health;
        ManaDisplay.text = "Mana: "+PlayerManager.CurrentMana + "/" + PlayerCharacter.Mana;
    }


    //These are for testing the maths
    //public void DoDamage()
    //{
    //    DamageSource src = DamageSource.WeaponDamage();
    //    ButtonText.text = (PlayerCharacter.DamageCalculation(src)).ToString();
    //}

    public void TakeDamage()
    {
        TakeDamageSource src = new TakeDamageSource()
        {
            DamageSources = new List<DamageSource>()
        };
        src.DamageSources.Add(new DamageSource()
        {
            value = 30,
            tags = new List<string>()
        });
        src.DamageSources[0].tags.Add("physical");
        src.DamageSources[0].tags.Add("all");

        PlayerManager.TakeDamage(src);
    }

}
