using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	public static HUD instance;

	public Text helping_text;

	private bool game_over = false;
	public GameObject game_over_hud;
	public Image game_over_background;

	public bool win = false;
	public GameObject win_screen_hud;

	public GameObject[] drips;

	public GameObject selector;

	public int current_level = 0;

	void Awake() {
		HUD.instance = this;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	private int flash_delay = 10;
	private int number_flashes = 0;
	private bool finished_flashing = false;
	public int helping_index = 0;

	public string[] helping_text_arr;

	public Text time_text;
	private float time  = 0;
	private int display_time = 0;

//	public string[] helping_text_arr = { "take the key to the cage to free the baby elephant", 
//		"use the left & right arrow keys to explore the level", 
//		"click space to jump" };

	private int text_display_time = 130;


	void Update () {

		if (time_text != null && !win) {
			time += Time.deltaTime;
			if (time >= 1) {
				display_time += 1;
				time = 0;
			}

			time_text.text = "time: " + display_time.ToString ();
		}

		if (Elephant.instance.needs_help) {
			if (helping_index < helping_text_arr.Length) {
				helping_text.enabled = true;
				helping_text.text = helping_text_arr [helping_index];
				text_display_time -= 1;
				if (text_display_time <= 0) {
					helping_text.enabled = false;
					if (helping_index == 0)
						Elephant.instance.needs_help = true;
					else
						Elephant.instance.needs_help = false;
					text_display_time = 130;
					helping_index++;
				}
			}
		} else if (helping_text.enabled) {
			text_display_time -= 1;
			if (text_display_time <= 0) {
				helping_text.enabled = false;
				text_display_time = 130;
			}
		}

		if (drips != null) {
			int i = 0;
			for (; i < Elephant.instance.water_meter; ++i) {
				GameObject drip = drips [i];
				drip.SetActive (true);
			}

			while (i < drips.Length) {
				GameObject drip = drips [i];
				drip.SetActive (false);
				i++;
			}

		}
			

		if (game_over) {
			return;
		}

		if (win) {
			bool Start = Input.GetKeyDown (KeyCode.Space);

			if (Start) {
				//replace with correct level
				SceneManager.LoadScene (0, LoadSceneMode.Single);
				return;
			}
		}
	}
				

	public void ShowWinSequence(){
		win = true;
		Elephant.instance.gameObject.SetActive (false);
		win_screen_hud.SetActive (true);
		//		print ("You win!");
	}

	public void ShowGameOver()
	{
		game_over = true;
		game_over_hud.SetActive (true);
	}
}