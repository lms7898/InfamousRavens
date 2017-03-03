using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject Enemy;

	// Use this for initialization
	void Start () {
        StartCoroutine(Spawn(Enemy, 2, 3));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Spawn(GameObject enemy, float interval, int numEnemies)
    {
        int currentCount = 0;
        while (currentCount < numEnemies)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2);
            ++currentCount;
        }
    }

}
