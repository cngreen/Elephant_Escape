using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera: MonoBehaviour {

	public bool show_level = true;

	public Elephant elephant;
	Camera camera;

	// Use this for initialization
	// ---------------------------------------------------------
	void Start () {
		camera = GetComponent<Camera>();
	}
		
	// Update is called once per frame
	// ---------------------------------------------------------
	//private int level_show_time = 100;
	void Update () {
		if (show_level == true && !Elephant.instance.walking) {
			return;
			//level_show_time -= 1;
		}

		//if (level_show_time <= 0 || Elephant.instance.walking)
		if (Elephant.instance.walking)
			show_level = false;

		if (!show_level) {
			transform.position = elephant.transform.TransformPoint (new Vector3 (-1.5f, 1.5f, -26f));
//			Vector3 camera_pos = camera.transform.position;
//			Vector3 elephant_pos = elephant.transform.position;
//
//			camera_pos.x = elephant_pos.x;
//			camera_pos.z = -25;
//
//			if (camera_pos.y < (elephant_pos.y - 3))
//				camera_pos.y += 0.1f;
//			else if (camera_pos.y > (elephant_pos.y + 3))
//				camera_pos.y -= 0.1f;
//
//			camera.transform.position = camera_pos;
		}
		
	}

}
