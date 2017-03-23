using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Weapon : MonoBehaviour {

    private float damage;
    private GameObject player;
    private GameObject parent;

	// Use this for initialization
	void Start () {
        parent = GetComponentInParent<EnemyBase>().gameObject;
        player = parent.GetComponent<EnemyBase>().Player;
        damage = parent.GetComponent<EnemyBase>().Damage;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Hit()
    {
        if (Vector3.Distance(player.transform.position, parent.transform.position) <= 2.0f)
        {
            player.GetComponent<PlayerMovement>().TakeDamage(damage);
            Debug.Log("Hit player");
        }
    }
}
