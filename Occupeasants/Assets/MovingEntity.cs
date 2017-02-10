using UnityEngine;
using System.Collections;

public class MovingEntity : MonoBehaviour {

	/* Animator Parameters
	 * 
	 * Boolean - "Walking" - State activated when the entity is walking
	 * 
	*/

	public float maxSpeed = 10.0f;
	public bool startsFacingRight = true;

	bool facingRight;

	protected float move = 0.0f;

	protected Animator myAnimator;

	// Use this for initialization
	public void Start () {
		facingRight = startsFacingRight;

		myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	public void FixedUpdate () {

		GetComponent<Rigidbody2D>().velocity = new Vector2 ((move * maxSpeed), GetComponent<Rigidbody2D>().velocity.y);

		if (move > 0 && !facingRight) {
			Flip();
		} else if (move < 0 && facingRight) {
			Flip();
		}
	
	}

	public void Flip() {
		facingRight = !facingRight;

		Vector2 newScale = transform.localScale;
		newScale.x *= -1;

		transform.localScale = newScale;
	}

	public void MoveLeft() {
		move = -1;

		if (myAnimator != null) {
			myAnimator.SetBool ("Walking", true);
		}
	}

	public void MoveRight() {
		move = 1;

		if (myAnimator != null) {
			myAnimator.SetBool ("Walking", true);
		}
	}

	public void Stop() {
		move = 0;

		if (myAnimator != null) {
			myAnimator.SetBool ("Walking", false);
		}
	}
}
