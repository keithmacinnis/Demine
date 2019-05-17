using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    //additional variables
    Rigidbody rb;
    Collider coll;
    public Collider attack;
    public Collider magic;

    //initialization
    void Start()
    {
        //get the rigid body, collider, and refreshing the hud
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    //checking for a collision to ground the player
    void OnCollisionEnter(Collision collision)
    {
    }

    //update to handle attacking
    void Update()
    {
        //physical attack
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Debug.Log("attack pressed");
            physicalAttack();
        }
        //magic attack
        else if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("magic pressed");
            //first number is how long into the animation to check for dmg
            //second number is how long the total animation length is
            magicAttack(1f, 2f);
        }
    }

    //method checking various collisions
    void OnTriggerEnter(Collider collider)
    {
        //entering the village
        if (collider.gameObject.tag == "VillageTrigger")
        {
            SceneManager.LoadScene("HubWorld");
            PlayerManager.instance.CurrentHealth = PlayerManager.instance.PC.Health;
        }
        //entering the mine
        else if (collider.gameObject.tag == "CaveHubTrigger")
        {
            SceneManager.LoadScene("Cave Hub");
        }
        //entering level1
        else if (collider.gameObject.tag == "Level1Trigger")
        {
            SceneManager.LoadScene("Level 1");
        }
        //entering level2
        else if (collider.gameObject.tag == "Level2Trigger")
        {
            SceneManager.LoadScene("Level 2");
        }
        //entering level3
        else if (collider.gameObject.tag == "Level3Trigger")
        {
            SceneManager.LoadScene("Level 3");
        }
        //entering boss1
        else if (collider.gameObject.tag == "Boss1Trigger")
        {
            SceneManager.LoadScene("Boss 1");
        }
        //entering boss2
        else if (collider.gameObject.tag == "Boss2Trigger")
        {
            SceneManager.LoadScene("Boss 2");
        }
        //entering boss3
        else if (collider.gameObject.tag == "Boss3Trigger")
        {
            SceneManager.LoadScene("Boss 3");
        }
        else if (collider.gameObject.tag == "HubWorldsTrigger")
        {
            SceneManager.LoadScene("Cave Hub");
        }
    }

    //enemy attack function, checking for overlap with the player hitbox
    private void magicAttack(float wait, float dmg)
    {
        //represents time to wait before initial attack animation hits
        StartCoroutine(attackWait(wait));
        //represents time in between attacks where damage is valid
        StartCoroutine(damageCheck(dmg));
    }

    //coroutine function to ensure the player isn't continuously being damaged
    private IEnumerator damageCheck(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    //coroutine function to check for damage at the proper point in the animation
    private IEnumerator attackWait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Collider[] cols = Physics.OverlapBox(magic.bounds.center, magic.bounds.extents, magic.transform.rotation, LayerMask.GetMask("EnemyHitbox"));

        //put damage calculations here
        foreach (Collider c in cols)
            Debug.Log(c.name);
    }

    //physical attack
    private void physicalAttack()
    {
        Collider[] cols = Physics.OverlapBox(attack.bounds.center, attack.bounds.extents, attack.transform.rotation, LayerMask.GetMask("EnemyHitbox"));

        //put damage calculations here
        foreach (Collider c in cols) {
            c.SendMessageUpwards("TakeDamage", PlayerManager.instance.PC.DamageCalculation(DamageSource.WeaponDamage()));
        }
    }
}