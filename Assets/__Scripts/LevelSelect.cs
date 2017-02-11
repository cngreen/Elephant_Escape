using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {
	public static LevelSelect instance;

	public GameObject[] upper_levels;
	public GameObject[] lower_levels;

	public GameObject selector;

	void Awake() {
		LevelSelect.instance = this;
	}

	// Use this for initialization
	void Start () {

	}

	private bool Right;
	private bool Left;
	private bool Space;
	private bool Down;
	private bool Up;

	void Update(){
		Right = Input.GetKeyDown (KeyCode.RightArrow);
		Left = Input.GetKeyDown (KeyCode.LeftArrow);
		Down = Input.GetKeyDown (KeyCode.DownArrow);
		Up = Input.GetKeyDown (KeyCode.UpArrow);

		moveSelector (Right, Left, Up, Down);

		Space = Input.GetKeyDown (KeyCode.Space);

		if (Space) {
			if (top) {
				if (selector_position == 0) {
					SceneManager.LoadScene ("Level_one", LoadSceneMode.Single);
				} else if (selector_position == 1) {
					SceneManager.LoadScene ("Level_two", LoadSceneMode.Single);
				} else if (selector_position == 2) {
					SceneManager.LoadScene ("Level_three", LoadSceneMode.Single);
				}
			} else {
				if (selector_position == 0) {
					SceneManager.LoadScene ("Level_four", LoadSceneMode.Single);
				} else if (selector_position == 1) {
					SceneManager.LoadScene ("Level_five", LoadSceneMode.Single);
				} else if (selector_position == 2) {
					print ("6");
					//SceneManager.LoadScene ("Level_one", LoadSceneMode.Single);
			}
		}
	}
	}


	private int selector_position = 0;

	private bool top = true;
	void moveSelector(bool Right, bool Left, bool Up, bool Down){
		if (Down && top) {
			top = false;

			GameObject level = upper_levels [selector_position];
			float adjust = selector.transform.position.y;
			adjust -= level.transform.position.y;

			level = lower_levels [selector_position];
			//print (level.transform.position.y);
			adjust += level.transform.position.y;

			adjust -= selector.transform.position.y;

			selector.transform.position += new Vector3 (0, adjust, 0);
		} else if (Up && !top) {
			
			GameObject level = upper_levels [selector_position];
			float adjust = selector.transform.position.y;
			adjust -= level.transform.position.y;

			level = lower_levels [selector_position];
			//print (level.transform.position.y);
			adjust += level.transform.position.y;

			adjust -= selector.transform.position.y;

			selector.transform.position += new Vector3 (0, -1 * adjust, 0);
			top = true;
		}
		else if (Right && selector_position < 2) {
			selector_position += 1;
			GameObject level = upper_levels [selector_position];
			float adjust = level.transform.position.x;
			adjust -= selector.transform.position.x;
			//print (level.transform.position);
			//print (level.name);
			selector.transform.position += new Vector3 (adjust, 0, 0);
		}
		else if(Left && selector_position > 0){
			selector_position -= 1;
			GameObject level = upper_levels [selector_position];
			//print (level.name);
			float adjust = level.transform.position.x;
			adjust -= selector.transform.position.x;
			selector.transform.position += new Vector3 (adjust, 0, 0);
		}	
	}
}