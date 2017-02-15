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
    

	// Use this for initialization
	void Start () {
        PathPoints = GetComponentsInChildren<Node>();
        Point = PathPoints[pointIndex];
        currentTarget = Point.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += (currentTarget - transform.position).normalized * MoveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<>
        {
            ++pointIndex;
        }
        Point = PathPoints[pointIndex];
        currentTarget = Point.transform.position;
    }
}
