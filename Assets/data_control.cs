using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class data_control : MonoBehaviour
{
	public static int death_count;
	public static int number_of_clear;
	public static int least_death;
	public static int death_of_fastest_clear;

	public static float play_time;
	public static float fastest_clear_time;
	public static float clear_time_of_least_death;

	int hour;
	int minute;
	float sec;

	public static bool now_playing;
	public static bool clear_;

	public Text death_count_text;
	public Text play_time_text;

	void Start()
	{
		PlayerPrefs.SetInt("First_start", PlayerPrefs.GetInt("First_start", 0));

		if (PlayerPrefs.GetInt("First_start") == 0)
		{
			PlayerPrefs.SetInt("clear_did", 0);

			PlayerPrefs.SetInt("player_death", 0);
			PlayerPrefs.SetInt("number_of_clear", 0);
			PlayerPrefs.SetInt("least_death", 0);
			PlayerPrefs.SetInt("death_of_fastest_clear", 0);
			
			PlayerPrefs.SetFloat("play_time", 0);
			PlayerPrefs.SetFloat("fastest_clear_time", 0);
			PlayerPrefs.SetFloat("clear_time_of_least_death", 0);

			PlayerPrefs.SetInt("least_death_did", 0);
			PlayerPrefs.SetInt("fastest_clear_did", 0);

			PlayerPrefs.SetInt("First_start", 1);

			PlayerPrefs.Save();
		}

		clear_ = System.Convert.ToBoolean(PlayerPrefs.GetInt("clear_did"));

		death_count = PlayerPrefs.GetInt("player_death");
		number_of_clear = PlayerPrefs.GetInt("number_of_clear");
		least_death = PlayerPrefs.GetInt("least_death");
		death_of_fastest_clear = PlayerPrefs.GetInt("death_of_fastest_clear");

		play_time = PlayerPrefs.GetFloat("play_time");
		fastest_clear_time = PlayerPrefs.GetFloat("fastest_clear_time");
		clear_time_of_least_death = PlayerPrefs.GetFloat("clear_time_of_least_death");

		if (SceneManager.GetActiveScene().buildIndex == 1)
        {
			now_playing = true;
        }
	}

    private void Update()
    {
		if (SceneManager.GetActiveScene().buildIndex == 1)
		{
			death_count_text.text = "☠: " + death_count;

			if (now_playing)
			{
				play_time += Time.deltaTime;
			}

			if (play_time / 60 >= 1)
			{
				if (play_time / 3600 >= 1)
				{
					hour = (int)(play_time / 3600);
					minute = (int)((play_time - hour * 3600) / 60);
					sec = (int)(play_time - hour * 3600 - minute * 60);
				}
				else
				{
					if (play_time / 60 >= 1)
					{
						hour = 0;
						minute = (int)(play_time / 60);
						sec = play_time - minute * 60;
					}
					else
					{
						hour = 0;
						minute = 0;
						sec = play_time;
					}
				}
			}
			else
			{
				hour = 0;
				minute = 0;
				sec = play_time;
			}

			if (minute < 10)
			{
				if (sec < 10)
				{
					play_time_text.text = hour + ":0" + minute + ":0" + sec.ToString("F2");
				}
				else
				{
					play_time_text.text = hour + ":0" + minute + ":" + sec.ToString("F2");
				}
			}
			else
			{
				if (sec < 10)
				{
					play_time_text.text = hour + ":" + minute + ":0" + sec.ToString("F2");
				}
				else
				{
					play_time_text.text = hour + ":" + minute + ":" + sec.ToString("F2");
				}
			}
		}
    }

	private void LateUpdate()
	{
		PlayerPrefs.SetInt("clear_did", System.Convert.ToInt16(clear_));

		PlayerPrefs.SetInt("player_death", death_count);
		PlayerPrefs.SetInt("number_of_clear", number_of_clear);
		PlayerPrefs.SetInt("least_death", least_death);
		PlayerPrefs.SetInt("death_of_fastest_clear", death_of_fastest_clear);

		PlayerPrefs.SetFloat("play_time", play_time);
		PlayerPrefs.SetFloat("fastest_clear_time", fastest_clear_time);
		PlayerPrefs.SetFloat("clear_time_of_least_death", clear_time_of_least_death);
	}
}
