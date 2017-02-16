using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 4f;

    private float moveX;
    private float moveY;

    private Rigidbody2D playerBody;
    private SpriteRenderer sprite;

    private Animator anim;


	// Use this for initialization
	void Start () {
        playerBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // reset sprite color *For Now*
        sprite.color = Color.white;

        float posX = transform.position.x;
        float posY = transform.position.y;

        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveX, moveY);
        Vector2 playerPos = new Vector2(posX, posY);

        movement = movement * speed * Time.deltaTime;

        playerBody.MovePosition(playerPos + movement);

        Animate();

        if (Input.GetKey("space"))
            Attack();
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
            anim.SetBool("walking", walking);
            if (Input.GetKey("w")) {
                anim.SetBool("wPressed", true);
                anim.SetBool("aPressed", false);
                anim.SetBool("sPressed", false);
                anim.SetBool("dPressed", false);
            }
            else if (Input.GetKey("a")) {
                anim.SetBool("aPressed", true);
                anim.SetBool("wPressed", false);
                anim.SetBool("sPressed", false);
                anim.SetBool("dPressed", false);
            }
            else if (Input.GetKey("s")) {
                anim.SetBool("sPressed", true);
                anim.SetBool("wPressed", false);
                anim.SetBool("aPressed", false);
                anim.SetBool("dPressed", false);
            }
            else if (Input.GetKey("d")) {
                anim.SetBool("dPressed", true);
                anim.SetBool("wPressed", false);
                anim.SetBool("aPressed", false);
                anim.SetBool("sPressed", false);
            }
        }else {
            anim.SetBool("walking", walking);
            anim.SetBool("wPressed", false);
            anim.SetBool("aPressed", false);
            anim.SetBool("sPressed", false);
            anim.SetBool("dPressed", false);
        }
    }

    void Attack() {
        // Placeholder until we have attack animation
        sprite.color = Color.green;
    }
}
