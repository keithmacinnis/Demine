using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDisplays : MonoBehaviour {

    public GameObject InventoryUI;
    public GameObject CharacterUI;



	// Use this for initialization
	void Start () {
        InventoryUI.SetActive(false);
        CharacterUI.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("i"))
        {
            InventoryUI.SetActive(!InventoryUI.activeSelf);
        }
        if (Input.GetKeyDown("c"))
        {
            CharacterUI.SetActive(!CharacterUI.activeSelf);
        }
    }
}
