﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {
	public static LevelSelect instance;

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
					print ("1");
					SceneManager.LoadScene ("Level_one", LoadSceneMode.Single);
				} else if (selector_position == 1) {
					print ("2");
					SceneManager.LoadScene ("Level_two", LoadSceneMode.Single);
				} else if (selector_position == 2) {
					print ("3");
					//SceneManager.LoadScene ("Level_one", LoadSceneMode.Single);
				}
			} else {
				if (selector_position == 0) {
					print ("4");
					//SceneManager.LoadScene ("Level_one", LoadSceneMode.Single);
				} else if (selector_position == 1) {
					print ("5");
					//SceneManager.LoadScene ("Level_one", LoadSceneMode.Single);
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
			selector.transform.position += new Vector3 (0, -130, 0);
		} else if (Up && !top) {
			selector.transform.position += new Vector3 (0, 130, 0);
			top = true;
		}
		else if (Right && selector_position < 2) {
			selector_position += 1;
			selector.transform.position += new Vector3 (180, 0, 0);
		}
		else if(Left && selector_position > 0){
			selector_position -= 1;
			selector.transform.position += new Vector3 (-180, 0, 0);
		}	
	}
}