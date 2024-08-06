using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clear_this_game : MonoBehaviour
{
    public GameObject record_window_obj;
    public GameObject new_record_window_obj;

    private GameObject record_window;
    private GameObject new_record_window;

    bool new_record_least_death;
    bool new_record_fastest_clear;

    public static int death_count;

    public static float clear_time;

    // Start is called before the first frame update
    void Start()
    {
        new_record_least_death = System.Convert.ToBoolean(PlayerPrefs.GetInt("least_death_did"));
        new_record_fastest_clear = System.Convert.ToBoolean(PlayerPrefs.GetInt("fastest_clear_did"));

        death_count = PlayerPrefs.GetInt("death_count_on_this");
        clear_time = PlayerPrefs.GetFloat("clear_time_on_this");

        PlayerPrefs.SetInt("least_death_did",0);
        PlayerPrefs.SetInt("fastest_clear_did", 0);
        PlayerPrefs.SetInt("death_count_on_this", 0);
        PlayerPrefs.SetFloat("clear_time_on_this", 0);

        StartCoroutine(Record_window_create());
    }

    // Update is called once per frame
    void Update()
    {
        if (record_window != null)
        {
            if (new_record_fastest_clear || new_record_least_death)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Destroy(record_window);

                    new_record_window = (GameObject)Instantiate(new_record_window_obj);
                    new_record_window.transform.SetParent(gameObject.transform);
                    new_record_window.transform.localPosition = new Vector3(0, 0, 400);
                    new_record_window.transform.localScale = new Vector3(1443, 1298, 144);

                    if (new_record_fastest_clear)
                    {
                        new_record_window.GetComponent<new_record>().new_record_time = true;
                    }

                    if (new_record_least_death)
                    {
                        new_record_window.GetComponent<new_record>().new_record_death = true;
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Destroy(record_window);
                    SceneManager.LoadSceneAsync(0); //start
                }
            }
        }
        else if (new_record_window != null)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Destroy(new_record_window);
                SceneManager.LoadSceneAsync(0); //start
            }
        }
    }

    IEnumerator Record_window_create()
    {
        yield return new WaitForSeconds(2);

        record_window = (GameObject)Instantiate(record_window_obj);
        record_window.transform.SetParent(gameObject.transform);
        record_window.transform.localPosition = new Vector3(0, 0, 400);
        record_window.transform.localScale = new Vector3(1443, 1298, 144);

        record_window.GetComponent<clear_record_text>().death_count = death_count;
        record_window.GetComponent<clear_record_text>().clear_time = clear_time;
    }
}
