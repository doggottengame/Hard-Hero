using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class record_text : MonoBehaviour
{
	int fastest_hour;
	int fastest_minute;
	float fastest_sec;

	int least_death_hour;
	int least_death_minute;
	float least_death_sec;

	string fastest_clear_time;
	string clear_time_of_least_death;

	Text record;

    // Start is called before the first frame update
    void Start()
    {
        record = gameObject.transform.GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
		if (data_control.fastest_clear_time / 60 >= 1)
		{
			if (data_control.fastest_clear_time / 3600 >= 1)
			{
				fastest_hour = (int)(data_control.fastest_clear_time / 3600);
				fastest_minute = (int)((data_control.fastest_clear_time - fastest_hour * 3600) / 60);
				fastest_sec = (int)(data_control.fastest_clear_time - fastest_hour * 3600 - fastest_minute * 60);
			}
			else
			{
				if (data_control.fastest_clear_time / 60 >= 1)
				{
					fastest_hour = 0;
					fastest_minute = (int)(data_control.fastest_clear_time / 60);
					fastest_sec = data_control.fastest_clear_time - fastest_minute * 60;
				}
				else
				{
					fastest_hour = 0;
					fastest_minute = 0;
					fastest_sec = data_control.fastest_clear_time;
				}
			}
		}
		else
		{
			fastest_hour = 0;
			fastest_minute = 0;
			fastest_sec = data_control.fastest_clear_time;
		}

		if (fastest_minute < 10)
		{
			if (fastest_sec < 10)
			{
				fastest_clear_time = fastest_hour + ":0" + fastest_minute + ":0" + fastest_sec.ToString("F2");
			}
			else
			{
				fastest_clear_time = fastest_hour + ":0" + fastest_minute + ":" + fastest_sec.ToString("F2");
			}
		}
		else
		{
			if (fastest_sec < 10)
			{
				fastest_clear_time = fastest_hour + ":" + fastest_minute + ":0" + fastest_sec.ToString("F2");
			}
			else
			{
				fastest_clear_time = fastest_hour + ":" + fastest_minute + ":" + fastest_sec.ToString("F2");
			}
		}

		if (data_control.clear_time_of_least_death / 60 >= 1)
		{
			if (data_control.clear_time_of_least_death / 3600 >= 1)
			{
				least_death_hour = (int)(data_control.clear_time_of_least_death / 3600);
				least_death_minute = (int)((data_control.clear_time_of_least_death - least_death_hour * 3600) / 60);
				least_death_sec = (int)(data_control.clear_time_of_least_death - least_death_hour * 3600 - least_death_minute * 60);
			}
			else
			{
				if (data_control.clear_time_of_least_death / 60 >= 1)
				{
					least_death_hour = 0;
					least_death_minute = (int)(data_control.clear_time_of_least_death / 60);
					least_death_sec = data_control.clear_time_of_least_death - least_death_minute * 60;
				}
				else
				{
					least_death_hour = 0;
					least_death_minute = 0;
					least_death_sec = data_control.clear_time_of_least_death;
				}
			}
		}
		else
		{
			least_death_hour = 0;
			least_death_minute = 0;
			least_death_sec = data_control.clear_time_of_least_death;
		}

		if (least_death_minute < 10)
		{
			if (least_death_sec < 10)
			{
				clear_time_of_least_death = least_death_hour + ":0" + least_death_minute + ":0" + least_death_sec.ToString("F2");
			}
			else
			{
				clear_time_of_least_death = least_death_hour + ":0" + least_death_minute + ":" + least_death_sec.ToString("F2");
			}
		}
		else
		{
			if (least_death_sec < 10)
			{
				clear_time_of_least_death = least_death_hour + ":" + least_death_minute + ":0" + least_death_sec.ToString("F2");
			}
			else
			{
				clear_time_of_least_death = least_death_hour + ":" + least_death_minute + ":" + least_death_sec.ToString("F2");
			}
		}

		record.text = "Number of Clear: " + data_control.number_of_clear + "\n\nFastest Clear Time: " + fastest_clear_time + "\nDeath Count of Fastest Clear Time: " + data_control.death_of_fastest_clear + "\n\nLeast Death Count: " + data_control.least_death + "\nClear Time of Least Death: " + clear_time_of_least_death;
    }
}
