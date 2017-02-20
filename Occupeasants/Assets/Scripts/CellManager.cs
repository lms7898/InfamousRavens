using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour {

    // Change to an active grid color
	void OnMouseDown()
    {
        if (this.gameObject.GetComponent<SpriteRenderer>().color == Color.green)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (this.gameObject.GetComponent<SpriteRenderer>().color == Color.red)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
