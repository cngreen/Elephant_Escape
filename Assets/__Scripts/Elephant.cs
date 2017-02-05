using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant: MonoBehaviour {

	public static Elephant instance;
	public bool has_key = false;
	public bool jumping = false;
	public bool on_ground = true;

	protected bool Spacebar;
	protected bool LeftArrow;
	protected bool RightArrow;
	protected bool Z_Key;
	protected bool X_Key;

	protected Rigidbody rb;

	// Use this for initialization
	// ---------------------------------------------------------
	void Start () {
		if (instance != null)
			print("multiple elephants!!");
		instance = this;

		rb = GetComponent<Rigidbody> ();
		
	}
	
	// Update is called once per frame
	// ---------------------------------------------------------
	void Update () {
		float horizontal = Input.GetAxis ("Horizontal");

		RightArrow = horizontal > 0.0f;
		LeftArrow = horizontal < 0.0f;

		Spacebar = Input.GetKeyDown (KeyCode.Space);

		Z_Key = Input.GetKeyDown (KeyCode.Z);
		X_Key = Input.GetKeyDown (KeyCode.X);

		Vector3 vel = rb.velocity;
		vel.x = 0f;
		rb.velocity = vel;

//		if (on_ground) {
//			rb.useGravity = false;
//		}

		if (RightArrow) {
			vel.x = 5f;
			rb.velocity = vel;
			GetComponent<SpriteRenderer> ().flipX = false;
		}

		if (LeftArrow) {
			vel.x = -5f;
			rb.velocity = vel;
			GetComponent<SpriteRenderer> ().flipX = true;
		}

		if (Spacebar) {
			if (!jumping)
				Jump ();
		}

		if (X_Key) {
			print ("X");
		}

		if (Z_Key) {
			print ("Z");
		}
		
	}

	// ---------------------------------------------------------
	void OnTriggerEnter(Collider other){
		print ("trigger: " + other.gameObject.tag);
		if (other.gameObject.tag == "Key") {
			has_key = true;
			Destroy (other.gameObject);
		} else if (other.gameObject.tag == "Cage") {
			if (has_key) {
				Destroy (other.gameObject);
			}
		} else if (other.gameObject.tag == "Baby Elephant") {
			// WIN
			print("You win!");
		}

	}

	void OnCollisionEnter(Collision coll){
		print ("collision: " + coll.gameObject.tag);

		if (coll.gameObject.tag == "Ground") {
			rb.velocity = Vector3.zero;
			jumping = false;
		}


	}

	// ---------------------------------------------------------
	void Jump(){
		jumping = true;
		on_ground = false;

		Vector3 vel = rb.velocity;
		vel.y = 10f;
		rb.velocity = vel;	
	}
}
