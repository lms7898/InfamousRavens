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
        Invoke("Activate", GameObject.Find("PhaseManager").GetComponent<PhaseManager>().prepTimer);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Activate()
    {
        StartCoroutine(Spawn(Enemy, 2, SpawnTotal));
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
        active = false;
    }

}
