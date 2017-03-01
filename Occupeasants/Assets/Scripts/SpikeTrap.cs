using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : TrapBase {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("touching a trap");
            GetComponent<Animator>().SetBool("Active", true);
        }
    }
}
