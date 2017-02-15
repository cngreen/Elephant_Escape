using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Wildfire: MonoBehaviour {

	public Sprite normal_sprite;
	public Sprite bright_sprite;

	private Rigidbody rb;
	protected StateMachine animation_state_machine = new StateMachine();

	void Awake(){
		rb = this.GetComponent<Rigidbody>();
	}

	private bool wasPositive = false;

	void Update(){
		animation_state_machine.Update ();

		if (animation_state_machine.IsFinished ()) {
			animation_state_machine.ChangeState (NextAnimationState ());
		}


		Vector3 vel = rb.velocity;

		if (!wasPositive && rb.velocity.x >= -0.05f) {
			vel.x = 4;
			wasPositive = true;
		} else if (rb.velocity.x <= 0.05f) {
			vel.x = -4;
			wasPositive = false;
		}

		rb.velocity = vel;
	}

	// ---------------------------------------------------------
	void OnCollisionEnter(Collision coll){
		print ("wildfire collision: " + coll.gameObject.tag);
		if (coll.gameObject.tag == "Droplet") {
			Destroy (this.gameObject);
		}
	}

	public virtual State NextAnimationState()
	{
		return new State_Animation_Wildfire(8, this);
	}

	public class State_Animation_Wildfire : State{
		private float spriteChangeRate;
		private float elapsedTime = 0.0f;

		private Wildfire fire;

		public State_Animation_Wildfire(float spriteChangeRate, Wildfire thisfire)
		{
			this.spriteChangeRate = spriteChangeRate;
			elapsedTime = spriteChangeRate;
			this.fire = thisfire;
		}

		public override void OnFinish() {
			fire.GetComponent<SpriteRenderer> ().sprite = fire.normal_sprite;
		}

		public override void OnUpdate (float time_delta_fraction)
		{
			elapsedTime += 1;

			if (elapsedTime >= spriteChangeRate) {
				if (fire.GetComponent<SpriteRenderer> ().sprite != fire.normal_sprite)
					fire.GetComponent<SpriteRenderer> ().sprite = fire.normal_sprite;
				else
					fire.GetComponent<SpriteRenderer> ().sprite = fire.bright_sprite;

				elapsedTime = 0;
			}
		}
	}
}
