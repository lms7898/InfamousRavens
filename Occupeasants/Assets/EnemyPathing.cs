using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    Node [] PathNodes;
    public float MoveSpeed;
    float Timer;
    static Vector3 CurrentPosition;
    int currentNode = 0;

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

        if(this.transform.position != CurrentPosition)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, CurrentPosition, Timer);
        }
        else
        {
            if(currentNode < PathNodes.Length-1)
            {
                currentNode++;
                CheckNode();
            }
        }
	}
}
