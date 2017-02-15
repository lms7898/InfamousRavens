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
    

	// Use this for initialization
	void Start () {
        //load the path
        PathPoints = Path.GetComponentsInChildren<Node>();
        Point = PathPoints[pointIndex];
        currentTarget = Point.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //Moving our enemy based on the public MoveSpeed variable
        transform.position += (currentTarget - transform.position).normalized * MoveSpeed * Time.deltaTime;
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


    }
}
