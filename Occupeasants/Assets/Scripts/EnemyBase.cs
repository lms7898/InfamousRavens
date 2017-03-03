using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public float MoveSpeed;
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
    private float GlobalAttackCD = 3;
    public float Damage;
    
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
            if (other.GetComponent<Collider2D>().IsTouching(this.GetComponent<Collider2D>()))
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
            }

            if(other.GetComponentInChildren<Chest>())
            {
                hasTreasure = true;
                Status.GetComponent<SpriteRenderer>().color = Color.red;
                Debug.Log("Got the treasure");
            }

            //Updating the enemy goal point
            if (direction == 1)
            {
                ++pointIndex;
            }
            else { --pointIndex; }
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

        //Did the player come close enough?
        if (distToPlayer <= 5)
        {
            if (!hitPlayer)
            {
                //Go get em!
                currentTarget = Player.transform.position + (Mathf.PI * Mathf.Pow(Vector3.Distance(Player.transform.position, new Vector3(2,2,0)),2));
                Status.GetComponent<SpriteRenderer>().sprite = Attacking;
                Status.GetComponent<SpriteRenderer>().color = Color.red;
                Debug.DrawLine(Player.transform.position, transform.position, Color.red);
            }
        }

        //Player too far now
        if (distToPlayer > 5)
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

}
