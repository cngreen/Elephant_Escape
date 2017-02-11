using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera: MonoBehaviour {

	public bool show_level = true;
	public float max_zoom_out;

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
	private bool start_zoom_out = false;
	private bool start_zoom_in = false;
	private float min_zoom = -16f;

	void Update () {

		if (show_level) {
			if (Elephant.instance.walking || Elephant.instance.GetComponent<Rigidbody> ().velocity.y > 0.2f) {
				show_level = false;
				Vector3 position = transform.position;
				position.z = -26f;
				transform.position = position;
			}
		}

		if (!show_level) {
			Vector3 position = elephant.transform.TransformPoint (new Vector3 (-3f, 1.5f, -26f));
			Vector3 camera_position = transform.position;
			camera_position.x = position.x;
			camera_position.y = position.y;

			transform.position = camera_position;
		}

		bool Up_arrow = Input.GetKeyDown (KeyCode.UpArrow);
		bool Up_arrow_up = Input.GetKeyUp (KeyCode.UpArrow);

		bool Down_arrow = Input.GetKeyDown (KeyCode.DownArrow);
		bool Down_arrow_up = Input.GetKeyUp (KeyCode.DownArrow);

		if (Up_arrow)
			start_zoom_in = true;
		else if (Up_arrow_up)
			start_zoom_in = false;

		if (Down_arrow)
			start_zoom_out = true;
		else if (Down_arrow_up)
			start_zoom_out = false;

		if (start_zoom_out)
			ZoomOut ();

		if (start_zoom_in)
			ZoomIn ();
	}

	void ZoomOut(){
		if (transform.position.z > max_zoom_out) {
			Vector3 pos = transform.position;
			pos.z -= 0.5f;
			transform.position = pos;
		}
	}

	void ZoomIn(){
		if (transform.position.z < min_zoom) {
			Vector3 pos = transform.position;
			pos.z += 0.5f;
			transform.position = pos;
		}
	}

}
