using UnityEngine;
using System.Collections;

public class PacingEntity : MovingEntity {

	/* Animator Parameters
	 * 
	 * Boolean - "Walking" - State activated when the entity is walking
	 * 
	*/

	// Use this for initialization
	void Start () {
		base.Start ();

		MoveLeft ();
	}
	
	// Update is called once per frame
	public void FixedUpdate () {
		base.FixedUpdate ();
	}

	void OnTriggerEnter2D (Collider2D other) {
		PacingEntityBorder border = other.gameObject.GetComponent<PacingEntityBorder> ();

		if (border != null) {
			Flip ();
		}
	}

	public void Flip() {
		base.Flip ();

		if (move < 0) {
			MoveRight();
		}else{
			MoveLeft ();
		}
	}


}
