using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour {

    private float prepTimer = 5.0f;

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

        // instantiate and deactivate spawner
		spawner = Instantiate(spawner);
		spawner.SetActive (false);
    }
	
	// Update is called once per frame
	void Update () {
        prepTimer -= Time.deltaTime;
		print (prepTimer);

        if (prepTimer <= 0.0f && gState == GameState.prepPhase){
            // change game phase
            gState = GameState.combatPhase;

            // activate spawner
            spawner.SetActive(true);
        }
	}
}
