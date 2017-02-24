using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 4f;
    public bool attacking;

    private float moveX;
    private float moveY;

    private Rigidbody2D playerBody;
    private SpriteRenderer sprite;

    private Animator anim;

    private GameObject combatObject;


    // Use this for initialization
    void Start () {
        attacking = false;
        playerBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        combatObject = GameObject.Find("CombatParent");
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

        Animate(moveX, moveY);

        if (Input.GetKey("space"))
            Attack();
        else {
            attacking = false;
            sprite.color = Color.white;
        }

	}

    void Animate(float h, float v) {
        bool walking = false;

        if (moveX != 0 || moveY != 0) {
            walking = true;
        }

        // raycast mouse to screen
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // set mouse z position to 0
        // z position is unnecessary in 2D
        mousePos.z = 0;

        if (walking) {
            anim.SetBool("walking", walking);
            // check angle between player right vector
            // and normalized distance between player position and mouse position
            Vector3 targetDir = transform.position - mousePos;

            // check if mouse y position is greater or less than players y position
            // allows for negative angles
            float sign = (mousePos.y < transform.position.y) ? -1.0f : 1.0f;

            // calculate angle between players right vector and the mouse position
            float angle = Vector3.Angle(transform.right, targetDir) * sign;

            // set animation state trigger based on mouse position
            if ((angle >= 0 && angle < 45) || angle <= 0 && angle > -45)
            {
                anim.SetTrigger("walkingLeft");
                anim.ResetTrigger("walkingUp");
                anim.ResetTrigger("walkingRight");
                anim.ResetTrigger("walkingDown");

                // rotate combat trigger
                combatObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 270.0f));
            }
            else if (angle >= 45 && angle < 135)
            {
                anim.SetTrigger("walkingUp");
                anim.ResetTrigger("walkingLeft");
                anim.ResetTrigger("walkingRight");
                anim.ResetTrigger("walkingDown");

                combatObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f));
            }
            else if ((angle >= 135 && angle < 180) || (angle <= -135 && angle > -180))
            {
                anim.SetTrigger("walkingRight");
                anim.ResetTrigger("walkingLeft");
                anim.ResetTrigger("walkingUp");
                anim.ResetTrigger("walkingDown");

                combatObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
            }
            else if (angle <= -45 && angle > -135)
            {
                anim.SetTrigger("walkingDown");
                anim.ResetTrigger("walkingLeft");
                anim.ResetTrigger("walkingUp");
                anim.ResetTrigger("walkingRight");

                combatObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
            }
        }
        else
        {
            anim.SetBool("walking", false);
            anim.ResetTrigger("walkingLeft");
            anim.ResetTrigger("walkingUp");
            anim.ResetTrigger("walkingRight");
            anim.ResetTrigger("walkingDown");

            combatObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0));
        }
    }

    void Attack() {
        // Placeholder until we have attack animation
        sprite.color = Color.green;

        attacking = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy" && attacking == true) {
            Destroy(other.gameObject);
        }
    }
}
