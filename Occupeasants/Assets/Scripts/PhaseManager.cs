using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour {

    private float prepTimer = 5.0f;
	public int numTraps;

    public GameObject spawner;

    public enum GameState{
        prepPhase,
        combatPhase
    }

    public GameState gState;

	// Use this for initialization
	void Start () {
        // initialize gamestate
        gState = GameState.prepPhase;

        // set number of traps
		numTraps = 6;
    }
	
	// Update is called once per frame
	void Update () {
        prepTimer -= Time.deltaTime;

        if (prepTimer <= 0.0f && gState == GameState.prepPhase){
            // change game phase
            gState = GameState.combatPhase;

            // activate spawner
			spawner = Instantiate(spawner);

			GameObject[] floorTiles = GameObject.FindGameObjectsWithTag("Floor");

			foreach (GameObject f in floorTiles) {
				if (f.GetComponent<CellManager> ().status == "empty") {
					Destroy (f);
				}
			}
		}
	}
}
