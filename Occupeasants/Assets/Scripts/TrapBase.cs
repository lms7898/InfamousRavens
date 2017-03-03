using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBase : MonoBehaviour {

    float Timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Timer = Time.fixedTime;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) { return; }
        Slow(other);
    }

    private void Remove()
    {
        Destroy(this);
    }

    //Make the effect a method and then call the method in OnTriggerEnter2D, 
    //do this for multiple effect traps to keep clutter low
    private void Slow(Collider2D other)
    {
        other.gameObject.GetComponent<EnemyBase>().currentTime = Timer;
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyBase>().slowed = true;
            other.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            Debug.Log("trapped");
            Debug.Log(Timer.ToString());
        }
    }
}
