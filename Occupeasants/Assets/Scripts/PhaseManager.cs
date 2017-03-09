using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour {

    public float prepTimer = 10.0f;

    private GameObject spawner;
    private GameObject tileManager;

    public enum GameState{
        prepPhase,
        combatPhase
    }

    public GameState gState;

	// Use this for initialization
	void Start () {
        // initialize gamestate
        gState = GameState.prepPhase;

        // deactivate spawner
        spawner = GameObject.Find("Spawner");
        spawner.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        prepTimer -= Time.deltaTime;

        if (prepTimer <= 0.0f && gState == GameState.prepPhase){
            // change game phase
            gState = GameState.combatPhase;

            // activate spawner
            spawner.SetActive(true);
        }
	}
}
