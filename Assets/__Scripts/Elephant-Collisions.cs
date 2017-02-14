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
		}else if (other.gameObject.tag == "Help Point") {
			if (other.gameObject.name == "Help Point A") {
				HUD.instance.toDisplay.Enqueue (0);
				needs_help = true;
				Destroy (other.gameObject);
			} else if (other.gameObject.name == "Help Point B") {
				HUD.instance.toDisplay.Enqueue (1);
				needs_help = true;
				Destroy (other.gameObject);
			} else if (other.gameObject.name == "Help Point C") {
				HUD.instance.toDisplay.Enqueue (2);
				needs_help = true;
				Destroy (other.gameObject);
			} else if (other.gameObject.name == "Help Point D") {
				HUD.instance.toDisplay.Enqueue (3);
				needs_help = true;
				Destroy (other.gameObject);
			} else if (other.gameObject.name == "Help Point Tree") {
				if (water_meter > 0) {
					HUD.instance.toDisplay.Enqueue (2);
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
			PlayElephantSound ();
			print ("You win!");
			HUD.instance.ShowWinSequence ();
		}

		else if (coll.gameObject.tag == "Cage") {
			if (has_key) {
				has_key = false;
				Destroy (coll.gameObject);
			}
		}

		else if (coll.gameObject.tag == "Spikes") {
			transform.position = init_pos;
			spiked = true;
			GetComponent<SpriteRenderer> ().enabled = false;
			GetComponent<Rigidbody> ().isKinematic = true;
			if (direction == "right") {
				pause_elephant.GetComponent<SpriteRenderer> ().flipX = false;	
			} else {
				pause_elephant.GetComponent<SpriteRenderer> ().flipX = true;
			}
		}
	}

	void RespawnElephant(){
		pause_elephant.SetActive (true);

		Color c = pause_elephant.GetComponent<SpriteRenderer> ().color;

		if (c.a == 0f) {
			lives -= 1;
		}
		
		c.a += 0.01f;
		pause_elephant.GetComponent<SpriteRenderer> ().color = c;

		if (pause_elephant.GetComponent<SpriteRenderer> ().color.a >= 0.9f) {
			c = pause_elephant.GetComponent<SpriteRenderer> ().color;
			c.a = 0f;
			pause_elephant.GetComponent<SpriteRenderer> ().color = c;
			pause_elephant.SetActive (false);

			spiked = false;
			GetComponent<SpriteRenderer> ().enabled = true;
			GetComponent<Rigidbody> ().isKinematic = false;

		}
	}
}
