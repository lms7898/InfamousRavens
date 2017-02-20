using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBase : MonoBehaviour {

    float Timer;
    float startTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Timer = Time.fixedTime;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            startTime = Timer;
            other.gameObject.GetComponent<EnemyBase>().slowed = true;
            Debug.Log("trapped");

            if (other.gameObject.GetComponent<EnemyBase>().slowed)
            {
                other.gameObject.GetComponent<EnemyBase>().MoveSpeed = 4;
                if (Timer >= startTime + 3)
                {
                    other.gameObject.GetComponent<EnemyBase>().MoveSpeed = other.gameObject.GetComponent<EnemyBase>().defaultMoveSpeed;
                    other.gameObject.GetComponent<EnemyBase>().slowed = false;
                    Debug.Log("not trapped");
                    Debug.Log(Timer.ToString());
                }
            }
        }
    }
}
