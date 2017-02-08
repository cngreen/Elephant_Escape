using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Elephant: MonoBehaviour {

	// ---------------------------------------------------------
	void OnTriggerEnter(Collider other){
		print ("trigger: " + other.gameObject.tag);
		if (other.gameObject.tag == "Key") {
			has_key = true;
			Destroy (other.gameObject);
		} else if (other.gameObject.tag == "Jump Collider") {
			needs_help = true;
		} else if (other.gameObject.tag == "Help Point") {
			if (other.gameObject.name == "Help Point A") {
				HUD.instance.helping_index = 0;
				needs_help = true;
				Destroy (other.gameObject);
			} else if (other.gameObject.name == "Help Point B") {
				HUD.instance.helping_index = 2;
				needs_help = true;
				Destroy (other.gameObject);
			} else if (other.gameObject.name == "Help Point C") {
				if (water_meter > 0) {
					HUD.instance.helping_index = 3;
					needs_help = true;
					Destroy (other.gameObject);
				}
			}
		} else if (other.gameObject.tag == "Water")
			near_water = true;

	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Water")
			near_water = false;
	}

	void OnCollisionEnter(Collision coll){
		print ("collision: " + coll.gameObject.tag);

		if (coll.gameObject.tag == "Ground") {
			rb.velocity = Vector3.zero;
			jumping = false;
		} 

		else if (coll.gameObject.tag == "Baby Elephant") {
			print ("You win!");
			HUD.instance.ShowWinSequence ();
		}

		else if (coll.gameObject.tag == "Cage") {
			if (has_key) {
				Destroy (coll.gameObject);
			}
		}

		else if (coll.gameObject.tag == "Spikes") {
			transform.position = init_pos;
		}
	}
}
