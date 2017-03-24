using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhaseManager : MonoBehaviour {

    public float prepTimer;
    private PlayerMovement playerScript;

	public int numTraps;
    public int maxKills;
    public GameObject spawner;

    public Text phaseText;
    public Text timerLabel;
    public Text numKillsLabel;
    public Text timerText;
    public Text numKillsText;
    public Text eRemainingLabel;
    public Text eRemainingText;

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
        prepTimer = 3.0f;

        // set max kills
        maxKills = 3;
    }
	
	// Update is called once per frame
	void Update () {
        if(gState == GameState.prepPhase) {
            phaseText.text = "Prep Phase";
            phaseText.resizeTextForBestFit = true;
            timerLabel.GetComponent<Text>().enabled = true;
            numKillsLabel.GetComponent<Text>().enabled = false;
            timerText.GetComponent<Text>().enabled = true;
            numKillsText.GetComponent<Text>().enabled = false;
            timerText.text = prepTimer.ToString("F2");
            eRemainingLabel.GetComponent<Text>().enabled = false;
            eRemainingText.GetComponent<Text>().enabled = false;

        } else if(gState == GameState.combatPhase) {
            phaseText.text = "Combat Phase";
            phaseText.resizeTextForBestFit = true;
            timerLabel.GetComponent<Text>().enabled = false;
            numKillsLabel.GetComponent<Text>().enabled = true;
            timerText.GetComponent<Text>().enabled = false;
            numKillsText.GetComponent<Text>().enabled = true;
            numKillsText.text = playerScript.numKills.ToString();
            eRemainingLabel.GetComponent<Text>().enabled = true;
            eRemainingText.GetComponent<Text>().enabled = true;
            eRemainingText.text = maxKills.ToString();
        }

        if (gState == GameState.prepPhase) {
            prepTimer -= Time.smoothDeltaTime;
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

        if (maxKills <= 0) {
            SceneManager.LoadSceneAsync("StartScreen");
        }
	}
}
