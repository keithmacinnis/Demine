using UnityEngine;
using System.Collections;

public class EyeAI : MonoBehaviour
{
    public Transform target;
    public int rotationSpeed = 1;
    public Collider attack;
    public Collider attack2;
    private bool countDamage = true;
    private bool fightBegin = false;
    private Animation anim;
    private Transform myTransform;

    private Enemy stats;

    void Awake()
    {
        myTransform = transform;
    }

    // Use this for initialization
    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponent<Animation>();
        target = go.transform;
        stats = new Enemy()
        {
            Health = 50,
            ColdResist = 20,
            FireResist = 20,
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

        if(fightBegin==false)
        {
            //if they are idle
            anim.Play("sleep");
        }

        if (distance <= 15 && fightBegin == false)
        {
            Debug.DrawLine(target.position, myTransform.position, Color.red);

            //starting the fight once in range
            fightBegin = true;

            //look at target/rotate
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
        }
        //Debug.Log(distance);
        else
        {
            //animations
            //melee attack
            if (distance < 3)
            {
                if (countDamage == true)
                {
                    anim.Play("attack2");
                    enemyAttack(0.47f, 2.5f, attack);
                }
            }
            else
            {
                //look at the target/rotatee
                myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);

                if (countDamage == true)
                {
                    anim.Play("attack1");
                    enemyAttack(0.47f, 2.5f, attack2);
                }
            }
        }
    }

    //enemy attack function, checking for overlap with the player hitbox
    private void enemyAttack(float wait, float dmg, Collider attack)
    {
        countDamage = false;
        //represents time to wait before initial attack animation hits
        StartCoroutine(attackWait(wait, attack));
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
    private IEnumerator attackWait(float seconds, Collider attack)
    {
        yield return new WaitForSeconds(seconds);
        Collider[] cols = Physics.OverlapBox(attack.bounds.center, attack.bounds.extents, attack.transform.rotation, LayerMask.GetMask("PlayerHitbox"));

        //put damage calculations here
        foreach (Collider c in cols)
            PlayerManager.instance.TakeDamage(stats.DealDamage(10, 30, 0));
    }
    public IEnumerator TakeDamage(DamageSource source)
    {
        stats.TakeDamage(source);
        if (stats.Health <= 0)
        {
            PlayerManager.instance.PC.Resource += 20;
            this.transform.Rotate(-80f, 0f, -80f);
            yield return new WaitForSeconds(0.3f);
            Destroy(gameObject);
        }
    }
}