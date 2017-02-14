using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Elephant: MonoBehaviour {
	public AudioSource jump_sound;
	public AudioSource water_sound;
	public AudioSource slurp_sound;
	public AudioSource elephant_sound;
	public AudioSource fall_sound;
	public AudioSource spike_sound;

	void PlayJumpSound(){
		if (rb.isKinematic == false)
			jump_sound.Play ();
	}

	void PlayWaterSound(){
		water_sound.Play ();
	}

	void PlaySlurpSound(){
		slurp_sound.Play ();
	}

	void PlayElephantSound(){
		elephant_sound.Play ();
	}

	public void PlayTreeSound(){
		fall_sound.Play ();
	}

	void PlaySpikeSound(){
		spike_sound.Play ();
	}
}
