﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Droplet: MonoBehaviour {

	// ---------------------------------------------------------
	void OnTriggerEnter(Collider other){
		print ("droplet trigger: " + other.gameObject.tag);
		Destroy (this.gameObject);
	}
		

	void OnCollisionEnter(Collision coll){
		print ("droplet collision: " + coll.gameObject.tag);
		if (coll.gameObject.tag == "Tree") {
			print (coll.gameObject.GetComponentInParent<Tree> ().grow_time);
			coll.gameObject.GetComponentInParent<Tree> ().grow_time = 3;
		}
		if (coll.gameObject.tag == "Wildfire") {
			Elephant.instance.PlaySizzleSound ();
			Destroy (coll.gameObject);
		}
		
		Destroy (this.gameObject);
	}
}
