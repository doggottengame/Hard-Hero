using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class new_record : MonoBehaviour
{
	public bool new_record_time;
	public bool new_record_death;

	public int death_count;

	public float clear_time;

	int hour;
	int minute;
	float sec;

	string clear_time_text;

	Text record;

	// Start is called before the first frame update
	void Start()
	{
		record = gameObject.transform.GetChild(0).GetComponent<Text>();
	}

	// Update is called once per frame
	void Update()
	{
		if (clear_time / 60 >= 1)
		{
			if (clear_time / 3600 >= 1)
			{
				hour = (int)(clear_time / 3600);
				minute = (int)((clear_time - hour * 3600) / 60);
				sec = (int)(clear_time - hour * 3600 - minute * 60);
			}
			else
			{
				if (clear_time / 60 >= 1)
				{
					hour = 0;
					minute = (int)(clear_time / 60);
					sec = clear_time - minute * 60;
				}
				else
				{
					hour = 0;
					minute = 0;
					sec = clear_time;
				}
			}
		}
		else
		{
			hour = 0;
			minute = 0;
			sec = clear_time;
		}

		if (minute < 10)
		{
			if (sec < 10)
			{
				clear_time_text = hour + ":0" + minute + ":0" + sec.ToString("F2");
			}
			else
			{
				clear_time_text = hour + ":0" + minute + ":" + sec.ToString("F2");
			}
		}
		else
		{
			if (sec < 10)
			{
				clear_time_text = hour + ":" + minute + ":0" + sec.ToString("F2");
			}
			else
			{
				clear_time_text = hour + ":" + minute + ":" + sec.ToString("F2");
			}
		}

		if (new_record_time && !new_record_death)
        {
			record.text = "new Record! (Fastest Clear)\n\nClear Time: " + clear_time_text + "\nDeath Count: " + death_count;
        }
		else if (!new_record_time && new_record_death)
		{
			record.text = "new Record! (Least Death Count)\n\nClear Time: " + clear_time_text + "\nDeath Count: " + death_count;
		}
		else if (new_record_time && new_record_death)
		{
			record.text = "new Record! (Fastest Clear & Least Death Count)Clear Time: " + clear_time_text + "\nDeath Count: " + death_count;
		}
	}
}
