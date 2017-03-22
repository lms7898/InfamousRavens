using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public bool attacking;
    public float Health;
    public int numKills;

    public GameObject HealthBar;
    public Animator combatAnim;

    private float moveX;
    private float moveY;
    private float speed;
    private float attackTimer;

    private Rigidbody2D playerBody;
    private SpriteRenderer sprite;
    private Animator anim;
    private GameObject combatParent;    
    private PhaseManager phaseScript;

    // Use this for initialization
    void Start () {
        attackTimer = 0.0f;
        speed = 4.0f;
        numKills = 0;
        attacking = false;
        playerBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        combatParent = GameObject.Find("CombatParent");
        phaseScript = GameObject.Find("PhaseManager").GetComponent<PhaseManager>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        print(attackTimer);
        float posX = transform.position.x;
        float posY = transform.position.y;

        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveX, moveY);
        Vector2 playerPos = new Vector2(posX, posY);

        movement = movement * speed * Time.deltaTime;

        playerBody.MovePosition(playerPos + movement);

        Animate(moveX, moveY);
        
        if(attackTimer <= 0.0f) {
            attacking = false;
        } else {
            attacking = true;
        }

        // If game is in combat phase
        // If left mouse button is down and player is not already attacking
		if (phaseScript.gState == PhaseManager.GameState.combatPhase && attacking == false) {
            if (Input.GetMouseButtonDown(0)) {
                Attack();
                attackTimer = 0.6f;
            }
        }

        if (attacking == true) {
            attackTimer -= Time.deltaTime;
			sprite.color = Color.green;
        } else {
			sprite.color = Color.white;
            attacking = false;
            combatAnim.SetBool("Attacking", false);
        }
	}

    void Animate(float h, float v) {
        // Determine whether player is walking or not
        bool walking = moveX != 0 || moveY != 0;

        // set animation bool
        anim.SetBool("Walking", walking);

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
                anim.SetInteger("Direction", 10);

                // rotate combat trigger
                combatParent.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
            }
            // diagonal back right
            else if(angle > 22.5 && angle < 67.5)
            {
                anim.SetInteger("Direction", 11);

                combatParent.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 135.0f));
            }
            // back
            else if(angle > 67.5 && angle < 112.5)
            {
                anim.SetInteger("Direction", 12);

                combatParent.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f));
            }
            // diagonal back left
            else if(angle > 112.5 && angle < 157.5)
            {
                anim.SetInteger("Direction", 13);

                combatParent.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 225.0f));
            }
            // left
            else if((angle > 112.5 && angle < 180) || (angle < -157.5 && angle > -180))
            {
                anim.SetInteger("Direction", 14);

                combatParent.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 270.0f));
            }
            // diagonal front right
            else if(angle < -22.5 && angle > -67.5)
            {
                anim.SetInteger("Direction", 9);

                combatParent.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 45));
            }
            // front
            else if(angle < -67.5 && angle > -112.5)
            {
                anim.SetInteger("Direction", 8);

                combatParent.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
            }
            // diagonal front left
            else if(angle < -112.5 && angle > -157.5)
            {
                anim.SetInteger("Direction", 15);

                combatParent.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 315.0f));
            }
        }
        else
        {
            // right
            if ((angle > 0 && angle < 22.5) || (angle < 0 && angle > -22.5))
            {
                anim.SetInteger("Direction", 2);

                combatParent.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
            }
            // diagonal back right
            else if (angle > 22.5 && angle < 67.5)
            {
                anim.SetInteger("Direction", 3);

                combatParent.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 135.0f));
            }
            // back
            else if (angle > 67.5 && angle < 112.5)
            {
                anim.SetInteger("Direction", 4);

                combatParent.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f));
            }
            // diagonal back left
            else if (angle > 112.5 && angle < 157.5)
            {
                anim.SetInteger("Direction", 5);

                combatParent.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 225.0f));
            }
            // left
            else if ((angle > 157.5 && angle < 180) || (angle < -157.5 && angle > -180))
            {
                anim.SetInteger("Direction", 6);

                combatParent.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 270.0f));
            }
            // diagonal front right
            else if (angle < -22.5 && angle > -67.5)
            {
                anim.SetInteger("Direction", 1);

                combatParent.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 45.0f));
            }
            // front
            else if (angle < -67.5 && angle > -112.5)
            {
                anim.SetInteger("Direction", 0);

                combatParent.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
            }
            // diagonal front left
            else if (angle < -112.5 && angle > -157.5)
            {
                anim.SetInteger("Direction", 7);

                combatParent.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 315.0f));
            }
        }
    }

    void Attack() {
		attacking = true;
        combatAnim.SetBool("Attacking", true);
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

    void OnTriggerEnter2D(Collider2D other) {
        BoxCollider2D[] colls = GetComponentsInChildren<BoxCollider2D>();

        foreach (Collider2D col in colls) {
			if (other.gameObject.tag == "Enemy" && attacking == true && col.name == "CombatCollider") {
				other.GetComponent<EnemyBase> ().TakeDamage (10);
            }
        }
    }
}
