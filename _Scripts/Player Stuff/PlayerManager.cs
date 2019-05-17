using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    public PlayerCharacter PC;

    public int CurrentHealth;
    public int CurrentMana;

    public void RerollWeapon()
    {
        if(PC.Resource >= 15)
        {
            int mhp = PC.Health - CurrentHealth;
            int mm = PC.Mana - CurrentMana;

            PC.Inventory.Weapon = Weapon.CreateNew(3);
            PC.CalculateStats();

            CurrentHealth = PC.Health - mhp;
            CurrentMana = PC.Mana - mm;

            PC.Resource -= 15;
        }
        

    }

    public void RerollTrinket()
    {
        if (PC.Resource >= 15)
        {
            int mhp = PC.Health - CurrentHealth;
            int mm = PC.Mana - CurrentMana;

            PC.Inventory.TrinketA = Trinket.CreateNew(5);
            PC.CalculateStats();

            CurrentHealth = PC.Health - mhp;
            CurrentMana = PC.Mana - mm;
            PC.Resource -= 15;
        }
    }

    public void RerollPants()
    {
            if (PC.Resource >= 15)
            {
                int mhp = PC.Health - CurrentHealth;
            int mm = PC.Mana - CurrentMana;

            PC.Inventory.Body = Pants.CreateNew(3);
            PC.CalculateStats();

            CurrentHealth = PC.Health - mhp;
            CurrentMana = PC.Mana - mm;
            PC.Resource -= 15;
        }
    }



    public void TakeDamage(TakeDamageSource source)
    {
        CurrentHealth -= PC.TakeDamage(source);
        if(CurrentHealth <= 0)
        {
            Destroy(instance.gameObject);
            SceneManager.LoadScene("MainMenu");
        }
    }





    // Use this for initialization
    void Start () {
        CurrentHealth = PC.Health;
        CurrentMana = PC.Mana;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //static instance of the player character,
    //can be access from anywhere
    public static PlayerManager instance = null;

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


        GameObject sp = GameObject.Find("SpawnPoint");
        PlayerManager.instance.transform.position = sp.transform.position;
    }


}
