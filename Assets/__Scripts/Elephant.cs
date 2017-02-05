using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant: MonoBehaviour {

	public static Elephant instance;
	public bool has_key = false;
	public bool jumping = false;
	public bool on_ground = true;
	public bool walking = false;

	public Sprite[] walking_sprites;
	public Sprite normal_sprite;

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

//		if (on_ground) {
//			rb.useGravity = false;
//		}

		if (rb.velocity.x <= 0.5) {
			walking = false;
		}

		if (RightArrow) {
			walking = true;
			vel.x = 5f;
			rb.velocity = vel;
			GetComponent<SpriteRenderer> ().flipX = false;
		}

		if (LeftArrow) {
			walking = true;
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

		if (coll.gameObject.tag == "Baby Elephant") {
			print ("You win!");
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
	//-------------
	/* Protected Member Variables */
	protected StateMachine animation_state_machine = new StateMachine();

	public class State_Animation_Movement : State
	{
		private float elapsedTime = 0.0f;
		private uint sprite_index = 0;
		private float spriteChangeRate;
		private Elephant Ella;

		public State_Animation_Movement(float spriteChangeRate, Elephant Ella)
		{
			this.spriteChangeRate = spriteChangeRate;
			elapsedTime = spriteChangeRate;
			this.Ella = Ella;
		}

		public override void OnStart ()
		{}

		public override void OnUpdate (float time_delta_fraction)
		{
			if (Ella.walking)
				elapsedTime += 1;

			if (!Ella.walking) {
				Ella.GetComponent<SpriteRenderer> ().sprite = Ella.normal_sprite;
				return;
			}

			if (elapsedTime >= spriteChangeRate) {
				Ella.GetComponent<SpriteRenderer> ().sprite = Ella.walking_sprites [sprite_index];

				sprite_index += 1;

				if (sprite_index >= Ella.walking_sprites.Length)
					sprite_index = 0;

				elapsedTime = 0;
			}	
		}
	}


	public virtual State NextAnimationState()
	{
		return new State_Animation_Movement(8, instance);
	}
}

