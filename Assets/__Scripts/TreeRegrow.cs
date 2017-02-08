using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRegrow: MonoBehaviour {
	public static TreeRegrow instance;
	public GameObject sapling;

	void Start(){
		instance = this;
	}

	public void NewTree(Vector3 position){
		Instantiate (sapling);
		sapling.transform.position = position;
	}
}

