using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public float MoveSpeed;
    public GameObject Path;
    public GameObject Player;
    public float currentTime;
    public bool slowed = false;
    public GameObject HealthBar;

    private Vector3 currentTarget;
    Node[] PathPoints;
    private Node Point;

    float Timer;
    int pointIndex = 0;
    int direction = 1;
    private float DefaultMS;
    private float Health = 100;
    
    //debug stuff
    bool hitPlayer = false; //this is so the player isn't chased constantly, the enemy hits you once and goes back to the path


    // Use this for initialization
    void Start()
    {
        //load the path and set the targets
        PathPoints = Path.GetComponentsInChildren<Node>();
        Point = PathPoints[pointIndex];
        currentTarget = Point.transform.position;
        DefaultMS = MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Timer = Time.fixedTime;

        //Moving our enemy based on the public MoveSpeed variable
        transform.position += (currentTarget - transform.position).normalized * MoveSpeed * Time.deltaTime;

        //Checking the surroundings for alternative targets
        CheckSurroundings();
        Kill();
        if (slowed)
        {
            MoveSpeed = 4;
            if (Timer >= currentTime + 3)
            {
                MoveSpeed = DefaultMS;
                slowed = false;
                GetComponent<SpriteRenderer>().color = Color.white;
                Debug.Log("not trapped");
                Debug.Log(Timer.ToString());
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

            //Updating the enemy goal point
            if (direction == 1)
            {
                ++pointIndex;
            }
            else { --pointIndex; }
            Point = PathPoints[pointIndex];
            currentTarget = Point.transform.position;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            currentTarget = PathPoints[pointIndex].transform.position;
            hitPlayer = true;
            TakeDamage(10);
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
        if (distToPlayer <= 10)
        {
            if (!hitPlayer)
            {
                //Go get em!
                currentTarget = Player.transform.position;
                Debug.DrawLine(Player.transform.position, transform.position, Color.red);
            }
        }

        //Player too far now
        if (distToPlayer > 10)
        {
            currentTarget = PathPoints[pointIndex].transform.position;
            hitPlayer = true;
        }

    }

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

    private void Kill()
    {
        if(Health <= 0)
        {
            Destroy(this);
        }
    }

}
