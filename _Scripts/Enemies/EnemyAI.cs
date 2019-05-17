using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    //this script started with help from:
    //https://answers.unity.com/questions/616346/enemy-patrol-random-walk.html
    public Transform target;
    public int moveSpeed = 1;
    public int rotationSpeed = 1;
    //public int enemyHP = 0;
    public Collider attack;
    private bool countDamage = true;
    private Animation anim;
    private Transform myTransform;
    private Vector3 originalPosition;
    //public AudioSource attackAudio;

    private Enemy stats;

    void Awake()
    {
        myTransform = transform;
        originalPosition = gameObject.transform.position;
    }

    // Use this for initialization
    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponent<Animation>();
        target = go.transform;
        stats = new Enemy()
        {
            Health = 100,
            ColdResist = 0,
            FireResist = 0,
            Armour = 0
        };
    }

    // Update is called once per frame
    void Update()
    {

        //enemy death, can alter this as desired
        /*if(enemyHP == 0)
        {
            Destroy(gameObject);
        }*/

        float distance = Vector3.Distance(target.transform.position, transform.position);
        float originalDistance = Vector3.Distance(originalPosition, transform.position);

        //Debug.Log(distance);
        if(distance <= 1.2 && originalDistance <= 15)
        {   
            //look at the target/rotatee
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);

            //animations
            //first number is how long into the animation to check for dmg
            //second number is how long the total animation length is
            if (gameObject.tag == "Undead")
            {
                anim.Play("attack1Weapon");
                //attackAudio.Play();
                if (countDamage == true)
                    enemyAttack(0.28f, 1.0f); 
            }
            else if (gameObject.tag == "Skeleton")
            {
                anim.Play("1handedAttack2");
                //attackAudio.Play();
                if (countDamage == true)
                    enemyAttack(0.26f, 0.925f);
            }
            else if (gameObject.tag == "Lizard")
            {
                anim.Play("attack1Spear");
                //attackAudio.Play();
                if (countDamage == true)
                    enemyAttack(0.29f, 1.005f);
            }
            else if (gameObject.tag == "Goblin")
            {
                anim.Play("attack1Daggers");
                //attackAudio.Play();
                if (countDamage == true)
                    enemyAttack(0.175f, 0.66f);
            }
        }
        else if (distance <= 15 && originalDistance <= 15)
        {
            Debug.DrawLine(target.position, myTransform.position, Color.red);

            //look at target/rotate
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);

            //animations
            if (gameObject.tag == "Undead")
                anim.Play("walkWeapon");
            else if (this.tag == "Skeleton")
                anim.Play("1handedWalkCombat");
            else if (gameObject.tag == "Lizard")
                anim.Play("walklSpear");
            else if (gameObject.tag == "Goblin")
                anim.Play("walkForwardDaggers");

            //move towards target
            myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
        }
        else if (distance > 15 || originalDistance > 15 )
        {
            //if they are idle
            if (originalDistance <= 1)
            {
                if (gameObject.tag == "Undead")
                    anim.Play("idleNormal");
                else if (this.tag == "Skeleton")
                    anim.Play("idle1handed");
                else if (gameObject.tag == "Lizard")
                    anim.Play("idleSpear");
                else if (gameObject.tag == "Goblin")
                    anim.Play("idleProtectedDaggers");
            }
            //moving back to original position
            else if(originalDistance > 1)
            {
                Debug.DrawLine(originalPosition, myTransform.position, Color.red);

                //look at target/rotate
                myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(originalPosition - myTransform.position), rotationSpeed * Time.deltaTime);

                //animations
                if (gameObject.tag == "Undead")
                    anim.Play("walkWeapon");
                else if (this.tag == "Skeleton")
                    anim.Play("1handedWalkCombat");
                else if (gameObject.tag == "Lizard")
                    anim.Play("walklSpear");
                else if (gameObject.tag == "Goblin")
                    anim.Play("walkForwardDaggers");

                //move towards target
                myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
            }
        }
    }

    //enemy attack function, checking for overlap with the player hitbox
    private void enemyAttack(float wait, float dmg)
    {
        countDamage = false;
        //represents time to wait before initial attack animation hits
        StartCoroutine(attackWait(wait));
        //represents time in between attacks where damage is valid
        StartCoroutine(damageCheck(dmg));
    }

    //coroutine function to ensure the player isn't continuously being damaged
    private IEnumerator damageCheck(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        countDamage = true;
    }

    //coroutine function to check for damage at the proper point in the animation
    private IEnumerator attackWait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Collider[] cols = Physics.OverlapBox(attack.bounds.center, attack.bounds.extents, attack.transform.rotation, LayerMask.GetMask("PlayerHitbox"));

        //put damage calculations here
        foreach (Collider c in cols)
            PlayerManager.instance.TakeDamage(stats.DealDamage(20, 0, 0));
    }

    public IEnumerator TakeDamage(DamageSource source)
    {
        stats.TakeDamage(source);
        if(stats.Health <= 0)
        {
            PlayerManager.instance.PC.Resource += 5;
            this.transform.Rotate(-80f, 0f, -80f);
            yield return new WaitForSeconds(0.3f);
            Destroy(gameObject);
        }
    }
}