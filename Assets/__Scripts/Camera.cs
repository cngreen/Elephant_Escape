using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera: MonoBehaviour {

	public Elephant elephant;
	Camera camera;

	// Use this for initialization
	// ---------------------------------------------------------
	void Start () {
		camera = GetComponent<Camera>();
	}
		
	// Update is called once per frame
	// ---------------------------------------------------------
	void Update () {
		Vector3 camera_pos = camera.transform.position;
		Vector3 elephant_pos = elephant.transform.position;

		camera_pos.x = elephant_pos.x;

		camera.transform.position = camera_pos;
		
	}

}
