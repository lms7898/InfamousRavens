using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    Node [] PathNodes;
    public GameObject Enemy;
    public float MoveSpeed;
    float Timer;
    static Vector3 CurrentPosition;
    int currentNode = 0;
    public GameObject TryAgain;

	// Use this for initialization
	void Start () {
        PathNodes = GetComponentsInChildren<Node>();
        CheckNode();
	}

    void CheckNode() {
        Timer = 0;
        CurrentPosition = PathNodes[currentNode].transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        Timer += Time.deltaTime * MoveSpeed;

        if(Enemy.transform.position != CurrentPosition)
        {
            Enemy.transform.position = Vector3.Lerp(Enemy.transform.position, CurrentPosition, Timer);
        }
        else
        {
            if(currentNode < PathNodes.Length-1)
            {
                currentNode++;
                CheckNode();
            }
            if(PathNodes[currentNode] == PathNodes[PathNodes.Length-1])
            {
                currentNode = -1;
            }
        }

        //if enemy reaches door and has treasure
        if(PathNodes[currentNode] == PathNodes[PathNodes.Length] && Enemy.GetComponent<EnemyBase>().hasTreasure)
        {
            //show play again button
            TryAgain.gameObject.SetActive(true);
        }
	}
}
