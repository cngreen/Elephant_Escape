using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRegen: MonoBehaviour {
	public bool needs_new_tree = false;
	public GameObject sapling;

	void Update(){
		if (needs_new_tree) {
			needs_new_tree = false;
			Instantiate (sapling);
		}
	}
}

