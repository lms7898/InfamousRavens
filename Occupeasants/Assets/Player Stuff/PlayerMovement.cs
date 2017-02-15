using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 2.5f;

    private float moveX;
    private float moveY;

    private Rigidbody2D playerBody;

    private Animator anim;


	// Use this for initialization
	void Start () {
        playerBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float posX = transform.position.x;
        float posY = transform.position.y;

        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveX, moveY);
        Vector2 playerPos = new Vector2(posX, posY);

        movement = movement * speed * Time.deltaTime;

        playerBody.MovePosition(playerPos + movement);
	}
}
