using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Tree: MonoBehaviour {

	public int grow_time = 0;
	public bool reminder_tree = false;
	public int max_growth = 20;
	public bool to_knock_down = false;
	public bool knocked_down = false;

	public bool started_growing = false;

	public int growth = 0;

	public Vector3 initial_pos;
	public GameObject sapling;

	void Update(){

		if (!started_growing)
			initial_pos = this.transform.position;
		
		bool Z_Key = Input.GetKeyDown (KeyCode.Z);
		bool Z_up = Input.GetKeyUp (KeyCode.Z);

		if (Z_Key) {
			GetComponent<BoxCollider> ().enabled = true;
		}
		if (Z_up && !knocked_down){
			GetComponent<BoxCollider> ().enabled = false;
		}

		Grow ();

		if (growth >= max_growth && !knocked_down) {
			Blink ();
		}

		if (knocked_down) {
			Fade ();
			if (GetComponent<SpriteRenderer> ().color.a <= 0.002f) {
//				print ("hello dead tree");
//				print(initial_pos);
				TreeRegrow.instance.NewTree (initial_pos);
				Destroy (this.gameObject);
			}
		}


	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Elephant") {
			if (!to_knock_down && !knocked_down && growth >= max_growth) {
				Color c = this.GetComponent<SpriteRenderer> ().color;
				c.a = 1f;
				this.GetComponent<SpriteRenderer> ().color = c;
				to_knock_down = true;
				KnockDown ();
			}
		}
	}

	void Grow(){
		if (grow_time > 0) {
			started_growing = true;
			grow_time -= 1;

			if (growth < max_growth) {
				Vector3 scale = this.transform.localScale;
				scale.x += 0.1f;
				scale.y += 0.1f;

				this.transform.localScale = scale;

				Vector3 pos = this.transform.position;
				pos.y += 0.293f;

				this.transform.position = pos;

				growth += 1;
			}

			if (reminder_tree && growth >= max_growth) {
				Elephant.instance.needs_help = true;
				HUD.instance.helping_index = 4;
			}
		}
	} //Grow

	void KnockDown(){
		if (to_knock_down) {
			knocked_down = true;
			to_knock_down = false;

			if (Elephant.instance.direction == "right") {

				Quaternion target = Quaternion.Euler (0f, 0f, -90f);
				this.transform.rotation = target;

				Vector3 pos = this.transform.position;
				pos.y -= 8.14f;
				pos.x += 9.2f;

				this.transform.position = pos;
			}

			else if (Elephant.instance.direction == "left") {

				Quaternion target = Quaternion.Euler (0f, 0f, 90f);
				this.transform.rotation = target;

				Vector3 pos = this.transform.position;
				pos.y -= 8.14f;
				pos.x -= 9.2f;

				this.transform.position = pos;
			}

			this.GetComponent<BoxCollider> ().enabled = true;
		}
	}

	void Fade(){
		Color c = this.GetComponent<SpriteRenderer> ().color;
		c.a -= 0.002f;
		this.GetComponent<SpriteRenderer> ().color = c;
	}


	private int blink_time = 10;
	void Blink(){
		blink_time -= 1;

		if (blink_time <= 0) {
			blink_time = 10;

			Color c = this.GetComponent<SpriteRenderer> ().color;

			if (c.a == 1) {
				c.a = 0.5f;
				this.GetComponent<SpriteRenderer> ().color = c;
			} else {
				c.a = 1f;
				this.GetComponent<SpriteRenderer> ().color = c;
			}
		}
	}
}
