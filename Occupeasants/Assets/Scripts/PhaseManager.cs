using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseManager : MonoBehaviour {

    private float prepTimer;
    private int maxKills;
    private PlayerMovement playerScript;

	public int numTraps;
    public GameObject spawner;

    public enum GameState{
        prepPhase,
        combatPhase
    }
    public GameState gState;

	// Use this for initialization
	void Start () {
        // initialize player script
        playerScript = GameObject.Find("Player").GetComponent<PlayerMovement>();

        // initialize gamestate
        gState = GameState.prepPhase;

        // set number of traps
		numTraps = 6;

        // set prep timer
        prepTimer = 7.0f;

        // set max kills
        maxKills = 3;
    }
	
	// Update is called once per frame
	void Update () {
        if (gState == GameState.prepPhase) {
            prepTimer -= Time.deltaTime;
        }

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

        if (playerScript.numKills == maxKills) {
            SceneManager.LoadSceneAsync("StartScreen");
        }
	}
}
