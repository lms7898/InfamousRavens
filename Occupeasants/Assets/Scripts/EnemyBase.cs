using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {


    private Vector3 currentTarget;
    Node [] PathPoints;
    public float MoveSpeed;
    float Timer;
    private Node Point;
    int pointIndex = 0;
    public GameObject Path;
    int direction = 1;
    public GameObject Player;


    //debug stuff
    bool hitPlayer = false; //this is so the player isn't chased constantly, the enemy hits you once and goes back to the path
    

	// Use this for initialization
	void Start () {
        //load the path and set the targets
        PathPoints = Path.GetComponentsInChildren<Node>();
        Point = PathPoints[pointIndex];
        currentTarget = Point.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //Moving our enemy based on the public MoveSpeed variable
        transform.position += (currentTarget - transform.position).normalized * MoveSpeed * Time.deltaTime;

        //Checking the surroundings for alternative targets
        CheckSurroundings();
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
        
        if(other.GetComponent<Shiny>())
        {
            currentTarget = PathPoints[pointIndex].transform.position;
            hitPlayer = true;
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

        //Did the player come close enough?
        if(distToPlayer < 5)
        {
            if (!hitPlayer)
            {
                //Go get em!
                currentTarget = Player.transform.position;
            }
        }

        //Player too far now, fack
        if(distToPlayer > 8)
        {
            currentTarget = PathPoints[pointIndex].transform.position;
            hitPlayer = true;
        }

    }
}
