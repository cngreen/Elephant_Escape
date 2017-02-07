﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Elephant: MonoBehaviour {

	public static Elephant instance;
	public bool has_key = false;
	public bool jumping = false;
	public bool walking = false;
	public bool drinking = false;

	public bool needs_help = false;

	public int water_meter = 0;
	public int max_water = 10;

	public Sprite[] walking_sprites;
	public Sprite[] drinking_sprites;
	public Sprite normal_sprite;

	public GameObject water_drip;

	protected bool Spacebar;
	protected bool LeftArrow;
	protected bool RightArrow;
	protected bool RightArrow_up;
	protected bool LeftArrow_up;

	protected bool Z_Key;
	protected bool X_Key;
	protected bool X_Keyup;

	public bool near_water = false;

	protected Rigidbody rb;

	private string direction = "right";

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
		X_Keyup = Input.GetKeyUp (KeyCode.X);

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

		if (RightArrow && !drinking) {
			direction = "right";
			walking = true;
			vel.x = 7f;
			rb.velocity = vel;
			GetComponent<SpriteRenderer> ().flipX = false;	
		} else if (LeftArrow && !drinking) {
			direction = "left";
			walking = true;
			vel.x = -7f;
			rb.velocity = vel;
			GetComponent<SpriteRenderer> ().flipX = true;
		}

		if (Spacebar) {
			if (!jumping)
				Jump ();
		}

		if (X_Key && near_water) {
			print ("X");
			drinking = true;
			animation_state_machine.ChangeState (new State_Animation_Drinking (8, this));
		}

		if (drinking) {
			if (water_meter < max_water) {
//				print ("water meter " + water_meter);
				water_meter += 1;
			}
		}
		if (X_Key && !near_water && water_meter > 0) {
//			print ("water meter " + water_meter);
//			water_meter -= 1;
			SprayWater ();
		}
		if (X_Keyup) {
			drinking = false;
			animation_state_machine.ChangeState (new State_Animation_Movement (8, this));
		}

		if (Z_Key) {
			print ("Z");
		}

	}
	// ---------------------------------------------------------
	void Jump(){
		jumping = true;

		Vector3 vel = rb.velocity;
		vel.y = 15f;
		rb.velocity = vel;	
	}

	void SprayWater(){
		GameObject droplet = Instantiate (water_drip);
		if (direction == "right") {
			droplet.transform.position = this.transform.position;
			droplet.transform.position += new Vector3 (2f, 0, 0);
			droplet.GetComponent<Rigidbody> ().velocity = new Vector3 (6.0f, -3.0f, 0.0f);
		} else if (direction == "left") {
			droplet.transform.position = this.transform.position;
			droplet.transform.position += new Vector3 (-2, 0, 0);
			droplet.GetComponent<Rigidbody> ().velocity = new Vector3 (-6.0f, -3.0f, 0.0f);
		}
	}
	//-------------
}
