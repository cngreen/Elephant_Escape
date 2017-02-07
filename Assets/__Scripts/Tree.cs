using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Tree: MonoBehaviour {

	private int grow_time = 5;

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
		grow_time -= 1;

		if (grow_time > 0) {
			Vector3 scale = this.transform.localScale;
			scale.x += 0.1f;
			scale.y += 0.1f;

			this.transform.localScale = scale;

			Vector3 pos = this.transform.position;
			pos.y += 0.293f;

			this.transform.position = pos;
		}
	}
}
