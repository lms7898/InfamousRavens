using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour {

    public GameObject bearTrap;
    public GameObject spikeTrap;

    private GameObject cellBear;
    private GameObject cellSpike;

    private GameObject phaseManager;
    private PhaseManager phaseScript;

    string status;
    Color tmp;

    // Sets the initial values of things
    void Start()
    {
        status = "empty";
        tmp = Color.black;
        tmp.a = 0.0f;

        // Starting all traps in their cells as disabled
        cellBear = Instantiate(bearTrap);
        cellBear.transform.parent = this.transform;
        cellBear.transform.localPosition = Vector3.zero;
        cellBear.active = false;


        cellSpike = Instantiate(spikeTrap);
        cellSpike.transform.parent = this.transform;
        cellSpike.transform.localPosition = Vector3.zero;
        cellSpike.active = false;

        phaseManager = GameObject.Find("PhaseManager");
        phaseScript = phaseManager.GetComponent<PhaseManager>();
    }

    // Change trap type
	void OnMouseDown()
    {
        if (phaseScript.gState == PhaseManager.GameState.prepPhase) {
            Debug.Log(status);
            if (status == "empty" && this.gameObject.GetComponent<SpriteRenderer>().color != tmp)
            {
                status = "bear";
                cellBear.active = true;
            }
            else if (status == "bear")
            {
                status = "spike";
                cellBear.active = false;
                cellSpike.active = true;
            }
            else if (status == "spike")
            {
                status = "empty";
                cellSpike.active = false;
            }
        }
    }
}
