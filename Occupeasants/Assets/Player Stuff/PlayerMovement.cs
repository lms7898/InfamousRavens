using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 4f;

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

        Animate();
	}

    void Animate() {
        bool walking;

        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
            walking = true;
        else
            walking = false;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (walking)
        {
            anim.SetFloat("mouseX", mousePos.x);
            anim.SetFloat("mouseY", mousePos.y);
        }else {
            anim.SetFloat("mouseX", 0f);
            anim.SetFloat("mouseY", 0f);
        }
    }
}
