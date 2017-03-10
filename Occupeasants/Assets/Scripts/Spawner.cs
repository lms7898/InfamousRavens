using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject Enemy;
    public int SpawnTotal;
    private bool active;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf)
        {
            Activate();
        }
    }

    private void Activate()
    {
        while (!active)
        {
            StartCoroutine(Spawn(Enemy, 2, SpawnTotal));
            active = true;
        }
        return;
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
