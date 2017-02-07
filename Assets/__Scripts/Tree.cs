using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Tree: MonoBehaviour {

	public int grow_time = 0;
	public bool reminder_tree = false;
	public int max_growth = 15;
	public bool knocked_down = false;

	private int growth = 0;

	void Update(){
		bool Z_Key = Input.GetKeyDown (KeyCode.Z);
		bool Z_up = Input.GetKeyUp (KeyCode.Z);

		if (Z_Key) {
			GetComponent<BoxCollider> ().enabled = true;
		}
		if (Z_up){
			GetComponent<BoxCollider> ().enabled = false;
		}

		Grow ();
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Elephant") {
			knocked_down = true;
			KnockDown ();
		}
	}

	void Grow(){
		if (grow_time > 0) {
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
		if (knocked_down) {
			while (this.transform.rotation.z >= -70) {
				Quaternion target = Quaternion.Euler (0f, 0f, -90f);
				float smooth = 2.0f;
				this.transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * smooth);

				Vector3 pos = this.transform.position;
				pos.y -= 0.1f;
				this.transform.position = pos;
			}
		}
	}
}
