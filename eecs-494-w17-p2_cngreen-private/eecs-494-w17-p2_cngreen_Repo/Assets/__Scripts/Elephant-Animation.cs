using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Elephant: MonoBehaviour {
	
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

		public override void OnFinish() {
			Ella.GetComponent<SpriteRenderer> ().sprite = Ella.normal_sprite;
		}

		public override void OnUpdate (float time_delta_fraction)
		{
			if (Ella.walking)
				elapsedTime += 1;

			if (!Ella.walking || Ella.jumping) {
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


	public class State_Animation_Drinking : State
	{
		private float elapsedTime = 0.0f;
		private uint sprite_index = 0;
		private float spriteChangeRate;
		private Elephant Ella;

		public State_Animation_Drinking(float spriteChangeRate, Elephant Ella)
		{
			this.spriteChangeRate = spriteChangeRate;
			elapsedTime = spriteChangeRate;
			this.Ella = Ella;
		}

		public override void OnFinish() {
			Ella.GetComponent<SpriteRenderer> ().sprite = Ella.normal_sprite;
		}

		public override void OnUpdate (float time_delta_fraction)
		{
			if (Ella.drinking)
				elapsedTime += 1;

			if (!Ella.drinking || Ella.walking || Ella.jumping) {
				Ella.GetComponent<SpriteRenderer> ().sprite = Ella.normal_sprite;
				ConcludeState();
			}

			if (elapsedTime >= spriteChangeRate) {
				Ella.GetComponent<SpriteRenderer> ().sprite = Ella.drinking_sprites [sprite_index];

				sprite_index += 1;

				if (sprite_index >= Ella.drinking_sprites.Length)
					sprite_index = 0;

				elapsedTime = 0;
			}	
		}
	}

	public virtual State NextAnimationState()
	{
		if (Elephant.instance.drinking)
			return new State_Animation_Drinking (8, instance);
		return new State_Animation_Movement(8, instance);
	}
}
