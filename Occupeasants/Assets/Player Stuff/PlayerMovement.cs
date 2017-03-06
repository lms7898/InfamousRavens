using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 4f;
    public bool attacking;
    public float Health;
    public GameObject HealthBar;

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
        bool walking = moveX != 0 || moveY != 0;

        // raycast mouse to screen
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // set mouse z position to 0
        // z position is unnecessary in 2D
        mousePos.z = 0;

        // check angle between player right vector
        // and normalized distance between player position and mouse position
        Vector3 targetDir = mousePos - transform.position;

        // check if mouse y position is greater or less than players y position
        float sign = (mousePos.y < transform.position.y) ? -1.0f : 1.0f; // allows for negative angles

        // calculate angle between players right vector and the mouse position
        float angle = Vector3.Angle(transform.right, targetDir) * sign;

        if (walking) {
            // right
            if ((angle > 0 && angle < 22.5) || (angle < 0 && angle > -22.5))
            {
                anim.SetBool("walkRight", true);
                anim.SetBool("idleRight", false);
                anim.SetBool("idleLeft", false);
                anim.SetBool("idleFront", false);
                anim.SetBool("idleBack", false);
                anim.SetBool("diagIdleRF", false);
                anim.SetBool("diagIdleRB", false);
                anim.SetBool("diagIdleLF", false);
                anim.SetBool("diagIdleLB", false);
                anim.SetBool("walkLeft", false);
                anim.SetBool("walkFront", false);
                anim.SetBool("walkBack", false);
                anim.SetBool("walkRF", false);
                anim.SetBool("walkRB", false);
                anim.SetBool("walkLF", false);
                anim.SetBool("walkLB", false);

                // rotate combat trigger
                combatObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
            }
            // diagonal back right
            else if(angle > 22.5 && angle < 67.5)
            {
                anim.SetBool("walkRB", true);
                anim.SetBool("walkRight", false);
                anim.SetBool("idleRight", false);
                anim.SetBool("idleLeft", false);
                anim.SetBool("idleFront", false);
                anim.SetBool("idleBack", false);
                anim.SetBool("diagIdleRF", false);
                anim.SetBool("diagIdleRB", false);
                anim.SetBool("diagIdleLF", false);
                anim.SetBool("diagIdleLB", false);
                anim.SetBool("walkLeft", false);
                anim.SetBool("walkFront", false);
                anim.SetBool("walkBack", false);
                anim.SetBool("walkRF", false);
                anim.SetBool("walkLF", false);
                anim.SetBool("walkLB", false);

                combatObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 135.0f));
            }
            // back
            else if(angle > 67.5 && angle < 112.5)
            {
                anim.SetBool("walkBack", true);
                anim.SetBool("walkRight", false);
                anim.SetBool("idleRight", false);
                anim.SetBool("idleLeft", false);
                anim.SetBool("idleFront", false);
                anim.SetBool("idleBack", false);
                anim.SetBool("diagIdleRF", false);
                anim.SetBool("diagIdleRB", false);
                anim.SetBool("diagIdleLF", false);
                anim.SetBool("diagIdleLB", false);
                anim.SetBool("walkLeft", false);
                anim.SetBool("walkFront", false);
                anim.SetBool("walkRF", false);
                anim.SetBool("walkRB", false);
                anim.SetBool("walkLF", false);
                anim.SetBool("walkLB", false);

                combatObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f));
            }
            // diagonal back left
            else if(angle > 112.5 && angle < 157.5)
            {
                anim.SetBool("walkLB", true);
                anim.SetBool("walkRight", false);
                anim.SetBool("idleRight", false);
                anim.SetBool("idleLeft", false);
                anim.SetBool("idleFront", false);
                anim.SetBool("idleBack", false);
                anim.SetBool("diagIdleRF", false);
                anim.SetBool("diagIdleRB", false);
                anim.SetBool("diagIdleLF", false);
                anim.SetBool("diagIdleLB", false);
                anim.SetBool("walkLeft", false);
                anim.SetBool("walkFront", false);
                anim.SetBool("walkBack", false);
                anim.SetBool("walkRF", false);
                anim.SetBool("walkRB", false);
                anim.SetBool("walkLF", false);

                combatObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 225.0f));
            }
            // left
            else if((angle > 112.5 && angle < 180) || (angle < -157.5 && angle > -180))
            {
                anim.SetBool("walkLeft", true);
                anim.SetBool("walkRight", false);
                anim.SetBool("idleRight", false);
                anim.SetBool("idleLeft", false);
                anim.SetBool("idleFront", false);
                anim.SetBool("idleBack", false);
                anim.SetBool("diagIdleRF", false);
                anim.SetBool("diagIdleRB", false);
                anim.SetBool("diagIdleLF", false);
                anim.SetBool("diagIdleLB", false);
                anim.SetBool("walkFront", false);
                anim.SetBool("walkBack", false);
                anim.SetBool("walkRF", false);
                anim.SetBool("walkRB", false);
                anim.SetBool("walkLF", false);
                anim.SetBool("walkLB", false);

                combatObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 270.0f));
            }
            // diagonal front right
            else if(angle < -22.5 && angle > -67.5)
            {
                anim.SetBool("walkRF", true);
                anim.SetBool("walkRight", false);
                anim.SetBool("idleRight", false);
                anim.SetBool("idleLeft", false);
                anim.SetBool("idleFront", false);
                anim.SetBool("idleBack", false);
                anim.SetBool("diagIdleRF", false);
                anim.SetBool("diagIdleRB", false);
                anim.SetBool("diagIdleLF", false);
                anim.SetBool("diagIdleLB", false);
                anim.SetBool("walkLeft", false);
                anim.SetBool("walkFront", false);
                anim.SetBool("walkBack", false);
                anim.SetBool("walkRB", false);
                anim.SetBool("walkLF", false);
                anim.SetBool("walkLB", false);

                combatObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 45));
            }
            // front
            else if(angle < -67.5 && angle > -112.5)
            {
                anim.SetBool("walkFront", true);
                anim.SetBool("walkRight", false);
                anim.SetBool("idleRight", false);
                anim.SetBool("idleLeft", false);
                anim.SetBool("idleFront", false);
                anim.SetBool("idleBack", false);
                anim.SetBool("diagIdleRF", false);
                anim.SetBool("diagIdleRB", false);
                anim.SetBool("diagIdleLF", false);
                anim.SetBool("diagIdleLB", false);
                anim.SetBool("walkLeft", false);
                anim.SetBool("walkBack", false);
                anim.SetBool("walkRF", false);
                anim.SetBool("walkRB", false);
                anim.SetBool("walkLF", false);
                anim.SetBool("walkLB", false);

                combatObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
            }
            // diagonal front left
            else if(angle < -112.5 && angle > -157.5)
            {
                anim.SetBool("walkLF", true);
                anim.SetBool("walkRight", false);
                anim.SetBool("idleRight", false);
                anim.SetBool("idleLeft", false);
                anim.SetBool("idleFront", false);
                anim.SetBool("idleBack", false);
                anim.SetBool("diagIdleRF", false);
                anim.SetBool("diagIdleRB", false);
                anim.SetBool("diagIdleLF", false);
                anim.SetBool("diagIdleLB", false);
                anim.SetBool("walkLeft", false);
                anim.SetBool("walkFront", false);
                anim.SetBool("walkBack", false);
                anim.SetBool("walkRF", false);
                anim.SetBool("walkRB", false);
                anim.SetBool("walkLB", false);

                combatObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 315.0f));
            }
        }
        else
        {
            // right
            if ((angle > 0 && angle < 22.5) || (angle < 0 && angle > -22.5))
            {
                anim.SetBool("idleRight", true);
                anim.SetBool("idleLeft", false);
                anim.SetBool("idleFront", false);
                anim.SetBool("idleBack", false);
                anim.SetBool("diagIdleRF", false);
                anim.SetBool("diagIdleRB", false);
                anim.SetBool("diagIdleLF", false);
                anim.SetBool("diagIdleLB", false);
                anim.SetBool("walkRight", false);
                anim.SetBool("walkLeft", false);
                anim.SetBool("walkFront", false);
                anim.SetBool("walkBack", false);
                anim.SetBool("walkRF", false);
                anim.SetBool("walkRB", false);
                anim.SetBool("walkLF", false);
                anim.SetBool("walkLB", false);

                combatObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
            }
            // diagonal back right
            else if (angle > 22.5 && angle < 67.5)
            {
                anim.SetBool("diagIdleRB", true);
                anim.SetBool("idleRight", false);
                anim.SetBool("idleLeft", false);
                anim.SetBool("idleFront", false);
                anim.SetBool("idleBack", false);
                anim.SetBool("diagIdleRF", false);
                anim.SetBool("diagIdleLF", false);
                anim.SetBool("diagIdleLB", false);
                anim.SetBool("walkRight", false);
                anim.SetBool("walkLeft", false);
                anim.SetBool("walkFront", false);
                anim.SetBool("walkBack", false);
                anim.SetBool("walkRF", false);
                anim.SetBool("walkRB", false);
                anim.SetBool("walkLF", false);
                anim.SetBool("walkLB", false);

                combatObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 135.0f));
            }
            // back
            else if (angle > 67.5 && angle < 112.5)
            {
                anim.SetBool("idleBack", true);
                anim.SetBool("idleRight", false);
                anim.SetBool("idleLeft", false);
                anim.SetBool("idleFront", false);
                anim.SetBool("diagIdleRF", false);
                anim.SetBool("diagIdleRB", false);
                anim.SetBool("diagIdleLF", false);
                anim.SetBool("diagIdleLB", false);
                anim.SetBool("walkRight", false);
                anim.SetBool("walkLeft", false);
                anim.SetBool("walkFront", false);
                anim.SetBool("walkBack", false);
                anim.SetBool("walkRF", false);
                anim.SetBool("walkRB", false);
                anim.SetBool("walkLF", false);
                anim.SetBool("walkLB", false);

                combatObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f));
            }
            // diagonal back left
            else if (angle > 112.5 && angle < 157.5)
            {
                anim.SetBool("idleRight", false);
                anim.SetBool("idleLeft", false);
                anim.SetBool("idleFront", false);
                anim.SetBool("idleBack", false);
                anim.SetBool("diagIdleRF", false);
                anim.SetBool("diagIdleRB", false);
                anim.SetBool("diagIdleLF", false);
                anim.SetBool("diagIdleLB", true);
                anim.SetBool("walkRight", false);
                anim.SetBool("walkLeft", false);
                anim.SetBool("walkFront", false);
                anim.SetBool("walkBack", false);
                anim.SetBool("walkRF", false);
                anim.SetBool("walkRB", false);
                anim.SetBool("walkLF", false);
                anim.SetBool("walkLB", false);

                combatObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 225.0f));
            }
            // left
            else if ((angle > 157.5 && angle < 180) || (angle < -157.5 && angle > -180))
            {
                anim.SetBool("idleRight", false);
                anim.SetBool("idleLeft", true);
                anim.SetBool("idleFront", false);
                anim.SetBool("idleBack", false);
                anim.SetBool("diagIdleRF", false);
                anim.SetBool("diagIdleRB", false);
                anim.SetBool("diagIdleLF", false);
                anim.SetBool("diagIdleLB", false);
                anim.SetBool("walkRight", false);
                anim.SetBool("walkLeft", false);
                anim.SetBool("walkFront", false);
                anim.SetBool("walkBack", false);
                anim.SetBool("walkRF", false);
                anim.SetBool("walkRB", false);
                anim.SetBool("walkLF", false);
                anim.SetBool("walkLB", false);

                combatObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 270.0f));
            }
            // diagonal front right
            else if (angle < -22.5 && angle > -67.5)
            {
                anim.SetBool("idleRight", false);
                anim.SetBool("idleLeft", false);
                anim.SetBool("idleFront", false);
                anim.SetBool("idleBack", false);
                anim.SetBool("diagIdleRF", true);
                anim.SetBool("diagIdleRB", false);
                anim.SetBool("diagIdleLF", false);
                anim.SetBool("diagIdleLB", false);
                anim.SetBool("walkRight", false);
                anim.SetBool("walkLeft", false);
                anim.SetBool("walkFront", false);
                anim.SetBool("walkBack", false);
                anim.SetBool("walkRF", false);
                anim.SetBool("walkRB", false);
                anim.SetBool("walkLF", false);
                anim.SetBool("walkLB", false);

                combatObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 45.0f));
            }
            // front
            else if (angle < -67.5 && angle > -112.5)
            {
                anim.SetBool("idleRight", false);
                anim.SetBool("idleLeft", false);
                anim.SetBool("idleFront", true);
                anim.SetBool("idleBack", false);
                anim.SetBool("diagIdleRF", false);
                anim.SetBool("diagIdleRB", false);
                anim.SetBool("diagIdleLF", false);
                anim.SetBool("diagIdleLB", false);
                anim.SetBool("walkRight", false);
                anim.SetBool("walkLeft", false);
                anim.SetBool("walkFront", false);
                anim.SetBool("walkBack", false);
                anim.SetBool("walkRF", false);
                anim.SetBool("walkRB", false);
                anim.SetBool("walkLF", false);
                anim.SetBool("walkLB", false);

                combatObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
            }
            // diagonal front left
            else if (angle < -112.5 && angle > -157.5)
            {
                anim.SetBool("idleRight", false);
                anim.SetBool("idleLeft", false);
                anim.SetBool("idleFront", false);
                anim.SetBool("idleBack", false);
                anim.SetBool("diagIdleRF", false);
                anim.SetBool("diagIdleRB", false);
                anim.SetBool("diagIdleLF", true);
                anim.SetBool("diagIdleLB", false);
                anim.SetBool("walkRight", false);
                anim.SetBool("walkLeft", false);
                anim.SetBool("walkFront", false);
                anim.SetBool("walkBack", false);
                anim.SetBool("walkRF", false);
                anim.SetBool("walkRB", false);
                anim.SetBool("walkLF", false);
                anim.SetBool("walkLB", false);

                combatObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 315.0f));
            }
        }
    }

    void Attack() {
        // Placeholder until we have attack animation
        sprite.color = Color.green;

        attacking = true;
    }

    public void TakeDamage(float DmgVal)
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

    void OnTriggerStay2D(Collider2D other) {
        BoxCollider2D[] colls = GetComponentsInChildren<BoxCollider2D>();
        print(colls.Length);

        foreach(Collider2D col in colls)
        if (other.gameObject.tag == "Enemy" && attacking == true && col.name == "CombatCollider") {
            Destroy(other.gameObject);
        }
    }
}
