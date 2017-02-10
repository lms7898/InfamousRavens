using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {


    private Vector3 Player;
    public Vector2 Point;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //GetComponent<Rigidbody2D>().velocity = new Vector2(5, GetComponent<Rigidbody2D>().velocity.y);
	}
}
