using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Elephant: MonoBehaviour {

	public static Elephant instance;
	public bool has_key = false;
	public bool jumping = false;
	public bool on_ground = true;
	public bool walking = false;

	public bool needs_help = false;

	public Sprite[] walking_sprites;
	public Sprite normal_sprite;

	protected bool Spacebar;
	protected bool LeftArrow;
	protected bool RightArrow;
	protected bool RightArrow_up;
	protected bool LeftArrow_up;

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

		needs_help = true;

	}

	// Update is called once per frame
	// ---------------------------------------------------------

	private float right_prev = 0f;
	private float left_prev = 0f;

	void Update () {
		animation_state_machine.Update ();

		if (animation_state_machine.IsFinished ()) {
			animation_state_machine.ChangeState (NextAnimationState ());
		}

		float horizontal = Input.GetAxis ("Horizontal");

		RightArrow = horizontal > 0.0f;
		LeftArrow = horizontal < 0.0f;

		Spacebar = Input.GetKeyDown (KeyCode.Space);

		Z_Key = Input.GetKeyDown (KeyCode.Z);
		X_Key = Input.GetKeyDown (KeyCode.X);

		Vector3 vel = rb.velocity;
		vel.x = 0f;
		rb.velocity = vel;

		// CHECK IF BUTTON STILL PRESSED
		if (RightArrow && right_prev > horizontal){
			RightArrow = false;
		}
		else if (LeftArrow && left_prev < horizontal) {
			LeftArrow = false;
		}

		if (RightArrow)
			right_prev = horizontal;
		else
			right_prev = 0f;
		if (LeftArrow)
			left_prev = horizontal;
		else
			left_prev = 0f;
		
		//--------------------------

		if (rb.velocity.x <= 0.5) {
			walking = false;
		}
		
		if (rb.velocity.y > 0.1 || rb.velocity.y < -0.1)
			jumping = true;
		else
			jumping = false;

		RaycastHit hit;

		if (RightArrow) {
			walking = true;
			vel.x = 7f;
			rb.velocity = vel;
			GetComponent<SpriteRenderer> ().flipX = false;	
		} else if (LeftArrow) {
			walking = true;
			vel.x = -7f;
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
		}
	}

	void OnCollisionEnter(Collision coll){
		print ("collision: " + coll.gameObject.tag);

		if (coll.gameObject.tag == "Ground") {
			rb.velocity = Vector3.zero;
			jumping = false;
		} else if (coll.gameObject.tag == "Jump Collider") {
			needs_help = true;
		}

		else if (coll.gameObject.tag == "Baby Elephant") {
			print ("You win!");
		}

		else if (coll.gameObject.tag == "Cage") {
			if (has_key) {
				Destroy (coll.gameObject);
			}
		}


	}

	// ---------------------------------------------------------
	void Jump(){
		jumping = true;
		on_ground = false;

		Vector3 vel = rb.velocity;
		vel.y = 15f;
		rb.velocity = vel;	
	}
	//-------------
}
