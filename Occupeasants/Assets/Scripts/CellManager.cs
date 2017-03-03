using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour {

    public GameObject bearTrap;
    public GameObject spikeTrap;

    private GameObject cellBear;
    private GameObject cellSpike;

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
    }

    // Change trap type
	void OnMouseDown()
    {
        Debug.Log(status);
        if (status == "empty" && this.gameObject.GetComponent<SpriteRenderer>().color != tmp)
        {
            Debug.Log("Yo");
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
