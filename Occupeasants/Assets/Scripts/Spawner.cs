using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject Enemy;
    public List<GameObject> Enemies = new List<GameObject>();
    public int _numEnemies;

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
        _numEnemies = numEnemies;
        GameObject tempEnemy;
        while (currentCount < numEnemies)
        {
            tempEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
            Enemies.Add(tempEnemy);
            tempEnemy.GetComponent<EnemyBase>().index = currentCount;
            yield return new WaitForSeconds(2);
            ++currentCount;
        }
    }

}
