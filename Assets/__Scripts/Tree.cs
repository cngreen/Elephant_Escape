using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Tree: MonoBehaviour {

	public int grow_time = 0;

	public bool reminder_tree = false;

	public int max_growth = 15;

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

	}
}
