using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCollider: MonoBehaviour {
	void Update(){
		if (Elephant.instance.jumping)
			GetComponent<BoxCollider> ().enabled = false;
		else
			GetComponent<BoxCollider> ().enabled = true;
	}
}

