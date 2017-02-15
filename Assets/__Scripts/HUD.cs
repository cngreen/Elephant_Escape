using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	public AudioSource gameSound;
	public AudioSource loseSound;

	public static HUD instance;

	public Text helping_text;

	private bool game_over = false;
	public GameObject game_over_hud;

	public bool win = false;
	public GameObject win_screen_hud;

	public GameObject[] drips;
	public GameObject[] life_images;

	public GameObject selector;

	public int current_level = 0;

	public Image key_image;

	protected bool return_key;

	void Awake() {
		HUD.instance = this;
	}

	// Use this for initialization
	void Start () {
		if (SceneManager.GetActiveScene ().name == "Level_one") {
			toDisplay.Enqueue (0);
			toDisplay.Enqueue (1);
			toDisplay.Enqueue (2);
		} else if (SceneManager.GetActiveScene ().name == "Level_three") {
			toDisplay.Enqueue (0);
		} else if (SceneManager.GetActiveScene ().name == "Level_four") {
			toDisplay.Enqueue (0);
			toDisplay.Enqueue (1);
		}
	}

	// Update is called once per frame
	private int flash_delay = 10;
	private int number_flashes = 0;
	private bool finished_flashing = false;

	public string[] helping_text_arr;

	public Text time_text;
	private float time  = 0;
	private int display_time = 0;

	//	public string[] helping_text_arr = { "take the key to the cage to free the baby elephant", 
	//		"use the left & right arrow keys to explore the level", 
	//		"click space to jump" };




	void Update () {

		return_key = Input.GetKeyDown (KeyCode.Return);

		if (return_key) {
			SceneManager.LoadScene ("Level_Select", LoadSceneMode.Single);
		}

		DisplayMessage ();

		if (time_text != null && !win && !game_over) {
			time += Time.deltaTime;
			if (time >= 1) {
				display_time += 1;
				time = 0;
			}

			time_text.text = "time: " + display_time.ToString ();
		}

		if (Elephant.instance.has_key) {
			key_image.enabled = true;
		} else {
			key_image.enabled = false;
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

		if (Elephant.instance.lives == 0) {
			Elephant.instance.gameObject.SetActive (false);
			if (!game_over) {
				ShowGameOver ();
			}
			game_over = true;
			print ("he's dead jim");
		}
		if (life_images != null) {
			int i = 0;
			for (; i < Elephant.instance.lives && i < life_images.Length; ++i) {
				GameObject life = life_images [i];
				life.SetActive (true);
			}

			while (i < life_images.Length) {
				GameObject life = life_images [i];
				life.SetActive (false);
				i++;
			}
		}


		if (game_over) {
			if (Input.GetKeyDown (KeyCode.Space))
				SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
		}

		if (win) {
			bool Start = Input.GetKeyDown (KeyCode.Space);

			if (Start) {
				//replace with correct level
				if (current_level == 1)
					SceneManager.LoadScene ("level_two", LoadSceneMode.Single);
				else if (current_level == 2)
					SceneManager.LoadScene ("level_three", LoadSceneMode.Single);
				else if (current_level == 3)
					SceneManager.LoadScene ("level_four", LoadSceneMode.Single);
				else if (current_level == 4)
					SceneManager.LoadScene ("level_five", LoadSceneMode.Single);
				else if (current_level == 5)
					SceneManager.LoadScene ("level_six", LoadSceneMode.Single);
				else if (current_level == 6)
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
		gameSound.Pause ();
		loseSound.Play ();
		game_over_hud.SetActive (true);
	}

	private int text_display_time = 300;
	private bool message_finished = true;

	public Queue<int> toDisplay = new Queue<int>();

	private int displayed_index = -1;
	private int helping_index = -1;

	public void DisplayMessage()
	{
		if (Elephant.instance.needs_help || toDisplay.Count > 0) {
			if (message_finished) {
				if (displayed_index == helping_index && toDisplay.Count > 0) {
					print ("change message");
					helping_index = toDisplay.Dequeue ();

					if (helping_index != displayed_index) {
						Elephant.instance.needs_help = false;
						helping_text.enabled = true;
						helping_text.text = helping_text_arr [helping_index];
						displayed_index = helping_index;
						message_finished = false;
					}
				} else {
					if (helping_index < helping_text_arr.Length) {
						Elephant.instance.needs_help = false;
						helping_text.enabled = true;
						helping_text.text = helping_text_arr [helping_index];
						displayed_index = helping_index;
						message_finished = false;
					}
				}

			} else {
				if (displayed_index != helping_index) {
					toDisplay.Enqueue (helping_index);
					print (toDisplay);
				}
			}
		}


		if (helping_text.enabled) {
			text_display_time -= 1;
			if (text_display_time <= 0) {
				message_finished = true;
				helping_text.enabled = false;
				Elephant.instance.needs_help = false;
				text_display_time = 300;
			}
		}

	}
}