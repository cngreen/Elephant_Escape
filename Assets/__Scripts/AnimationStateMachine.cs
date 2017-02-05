using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Character : MonoBehaviour
{
	/* Private Member Variables */

	/* Protected Member Variables */
	protected StateMachine animation_state_machine = new StateMachine();

	/* Public Member Variables */
	public Sprite[] walking_sprites;

	/* Private Member Classes */

	/* Protected Member Classes */

	/* Public Member Classes */

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
			if (Ella.walking) {
				elapsedTime = spriteChangeRate;
				sprite_index = 0;
			}

			elapsedTime += Time.deltaTime;

			if (elapsedTime < spriteChangeRate || !Ella.walking) {
				return;
			}

			elapsedTime = 0.0f;

			Ella.GetComponent<SpriteRenderer> ().sprite = Ella.walking_sprites [sprite_index++];

			if (sprite_index >= Ella.walking_sprites.Length)
				sprite_index = 0;
		}
	}
		

	public virtual State NextAnimationState()
	{
		return null;
	}
}