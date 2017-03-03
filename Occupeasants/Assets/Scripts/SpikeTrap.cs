using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : TrapBase {


    private double timer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.fixedTime >= timer)
        {
            GetComponent<Animator>().SetTime(0);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            timer = Time.fixedTime + .5;
            Debug.Log("touching a trap");
            GetComponent<Animator>().SetBool("Active", true);
        }
    }
}
