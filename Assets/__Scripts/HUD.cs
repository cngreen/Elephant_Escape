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
	public Image win_screen_background;
	public Image win_screen_background_black;
	public Text win_screen_continue;

	public GameObject selector;

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
	private int helping_index = 0;

	static string[] helping_text_arr = { "take the key to the cage to free the baby elephant", 
		"use the left & right arrow keys to explore the level", 
		"click space to jump" };

	private int text_display_time = 130;


	void Update () {

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
		}



		if (game_over) {
			return;
		}

		if (win) {
			Color c;
			bool Start = Input.GetKeyDown (KeyCode.Return);
			if (!finished_flashing) {
				flash_delay -= 1;
				if (flash_delay <= 0) {
					flash_delay = 10;
					// change screen color
					c = win_screen_background.color;
					if (c.a == 0.75f) {
						c.a = 0.0f;
						win_screen_background.color = c;
					} else {
						c.a = 0.75f;
						number_flashes += 1;
						if (number_flashes >= 6) {
							finished_flashing = true;
							flash_delay = 100;
						}
						win_screen_background.color = c;
					}
				}
			} else {//fnished flashing
				c = win_screen_background.color;
				c.a = 0.0f;
				win_screen_background.color = c;

				flash_delay -= 1;

				if (flash_delay <= 0) {
					c = win_screen_background_black.color;
					c.a = 1.0f;
					win_screen_background_black.color = c;
					win_screen_continue.enabled = true;

					if (Start) {
						//replace with correct level
						int scene_index = SceneManager.GetActiveScene ().buildIndex;
						if (scene_index == 0)
							SceneManager.LoadScene (1, LoadSceneMode.Single);
						else
							SceneManager.LoadScene (0, LoadSceneMode.Single);
						return;
					}
				}
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

	private int selector_position = 0;

	void moveSelector(bool Right, bool Left){
		if (Right && selector_position < 2) {
			selector_position += 1;
			selector.transform.position += new Vector3 (30, 0, 0);
		}
		else if(Left && selector_position > 0){
			selector_position -= 1;
			selector.transform.position += new Vector3 (-30, 0, 0);
			}	
	}
}