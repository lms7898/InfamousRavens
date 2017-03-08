using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public float MoveSpeed;
    public float Damage;
    public GameObject Path;
    public GameObject Player;
    
    public GameObject HealthBar;
    public GameObject Status;

    public Sprite Attacking, Hunting;

    private Vector3 currentTarget;
    Node[] PathPoints;
    private Node Point;

    private float STimer;
    int pointIndex = 0;
    int direction = 1;
    private float DefaultMS;
    private float Health = 100;
    private bool hasTreasure;
    private float currentTime;
    private bool slowed = false;
    private float slowedSpeed;
    
    //debug stuff
    bool hitPlayer = false; //this is so the player isn't chased constantly, the enemy hits you once and goes back to the path


    // Use this for initialization
    void Start()
    {
        //load the path and set the targets
        Path = GameObject.Find("Path");
        Player = GameObject.Find("Player");
        PathPoints = Path.GetComponentsInChildren<Node>();
        Point = PathPoints[pointIndex];
        currentTarget = Point.transform.position;
        DefaultMS = MoveSpeed;
        hasTreasure = false;
    }

    void FixedUpdate()
    {
        //Moving our enemy based on the public MoveSpeed variable
        transform.position += (currentTarget - transform.position).normalized * MoveSpeed * Time.deltaTime;
    }
    
    // Update is called once per frame
    void Update()
    {
        //sprite sheet animation conditions
        Animate();

        //Checking the surroundings for alternative targets
        CheckSurroundings();
        Kill();

        //For slow effect
        if (slowed)
        {
            MoveSpeed = slowedSpeed;
            if (Time.fixedTime >= STimer)
            {
                MoveSpeed = DefaultMS;
                slowed = false;
                GetComponent<SpriteRenderer>().color = Color.white;
                Debug.Log("not trapped");
                Debug.Log(Time.fixedTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Pathing
        if (other.GetComponentInChildren<Node>())
        {
            //Direction to move on the path
            if (pointIndex >= PathPoints.Length - 1)
            {
                direction = -1;
            }
            if (pointIndex <= 0)
            {
                direction = 1;
            }
            hitPlayer = false;


            if (other.GetComponentInChildren<Chest>())
            {
                hasTreasure = true;
                Status.GetComponent<SpriteRenderer>().color = Color.red;
                Debug.Log("Got the treasure");
            }

            //Updating the enemy goal point
            if (direction > 0)
            {
                ++pointIndex;
            }
            else if (direction < 0)
            {
                --pointIndex;
            }
            Point = PathPoints[pointIndex];
            currentTarget = Point.transform.position;
        }
        

        //Hit the player
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().TakeDamage(Damage);
            currentTarget = PathPoints[pointIndex].transform.position;
            hitPlayer = true;
        }


        //Trap Type Checks
        //Bear -- Init Damage + Slow
        if(other.GetComponent<BearTrap>())
        {
            STimer = Time.fixedTime + other.GetComponent<TrapBase>().Duration;
            slowed = true;
            slowedSpeed = MoveSpeed / 2;
            GetComponent<SpriteRenderer>().color = Color.blue;
            Debug.Log("trapped");
            Debug.Log(Time.fixedTime);
        }

        //Spikes -- Bleed Damage over Time
        if(other.GetComponent<SpikeTrap>())
        {
            StartCoroutine(Bleed(1, 3, 10));
        }
    }

    //Method used for changing targets
    //Seeking the Player
    //Seeking Special Objects
    private void CheckSurroundings()
    {
        //Check distance from the player
        float distToPlayer;
        distToPlayer = Vector3.Distance(transform.position, Player.transform.position);
        Debug.DrawLine(Player.transform.position, transform.position, Color.green);
        Debug.DrawLine(transform.position, currentTarget, Color.gray);

        //Did the player come close enough?
        if (distToPlayer <= 3)
        {
            if (!hitPlayer)
            {
                //Go get em!
                currentTarget = Player.transform.position;
                Status.GetComponent<SpriteRenderer>().sprite = Attacking;
                Status.GetComponent<SpriteRenderer>().color = Color.red;
                Debug.DrawLine(Player.transform.position, transform.position, Color.red);
            }
        }

        //Player too far now
        if (distToPlayer > 3)
        {
            Status.GetComponent<SpriteRenderer>().sprite = Hunting;
            if (hasTreasure)
            {
                Status.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                Status.GetComponent<SpriteRenderer>().color = Color.yellow;
            }

            currentTarget = PathPoints[pointIndex].transform.position;
            hitPlayer = true;
        }

    }

    //Bleeding Damage over Time effect
    IEnumerator Bleed(float Duration, int Ticks, float DMG)
    {
        int currentCount = 0;
        GetComponentInChildren<ParticleSystem>().Play();
        while (currentCount < Ticks)
        {
            TakeDamage(DMG);
            yield return new WaitForSeconds(Duration);
            ++currentCount;
        }
        GetComponentInChildren<ParticleSystem>().Stop();
    }

    //Use this when applying damage so the health bar works correctly
    private void TakeDamage(float DmgVal)
    {
        //Divide for the localScale to work properly
        Health = (Health - DmgVal) / 100;
        HealthBar.transform.localScale = new Vector3(
            Mathf.Clamp(Health, 0f, 1f),
            HealthBar.transform.localScale.y,
            HealthBar.transform.localScale.z);

        //Re-multiply to retain an accurate measure of health
        Health *= 100;
        Debug.Log("New Health:" + Health);
    }

    //Kills the enemy
    private void Kill()
    {
        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Animate()
    {
        Vector3 myPos = transform.position;
        Vector3 targetPos = currentTarget - myPos;
        currentTarget.z = 0;
        float multiplier = (targetPos.y < transform.position.y) ? -1.0f : 1.0f;
        float angle = Vector3.Angle(transform.right,targetPos) * multiplier;
        
        //Face left
        if ((angle > 112.5 && angle < 180) || (angle < -157.5 && angle > -180))
        {
            GetComponent<Animator>().SetInteger("Direction", 2);
        }

        //Face right
        if ((angle > 0 && angle < 22.5) || (angle < 0 && angle > -22.5))
        {
            GetComponent<Animator>().SetInteger("Direction", 1);
        }

        //Face down
        if (angle < -67.5 && angle > -112.5)
        {
            GetComponent<Animator>().SetInteger("Direction", 4);
        }

        //Face up
        if (angle > 67.5 && angle < 112.5)
        {
            GetComponent<Animator>().SetInteger("Direction", 3);
        }

        //Face up-left
        if (angle > 112.5 && angle < 157.5)
        {
            GetComponent<Animator>().SetInteger("Direction", 8);
        }

        //Face up-right
        if (angle > 22.5 && angle < 67.5)
        {
            GetComponent<Animator>().SetInteger("Direction", 7);
        }

        //Face down-left
        if (angle < -112.5 && angle > -157.5)
        {
            GetComponent<Animator>().SetInteger("Direction", 6);
        }

        //Face down-right
        if (angle < -22.5 && angle > -67.5)
        {
            GetComponent<Animator>().SetInteger("Direction", 5);
        }
    }

}
