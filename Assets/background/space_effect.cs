using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class space_effect : MonoBehaviour
{
    public GameObject red_vehicle;
    public GameObject yellow_vehicle;
    public GameObject blue_vehicle;

    public GameObject vehicle_destroyed_effect_obj;

    private GameObject[] vehicle_destroyed_effect = new GameObject[3];

    bool[] vehicle_on_space = new bool[3];
    bool[] warning_text_effect = new bool[3];

    float[] vehicle_on_space_cnt = new float[3];
    float[] warning_text_effect_cnt = new float[3];

    public Text[] vehicle_warning_text = new Text[3];

    Vector2[] vehicle_velocity_tmp = new Vector2[3];

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == red_vehicle)
        {
            vehicle_velocity_tmp[0] = red_vehicle.GetComponent<Rigidbody2D>().velocity;

            red_vehicle.GetComponent<Rigidbody2D>().drag = 0;
            red_vehicle.GetComponent<Rigidbody2D>().gravityScale = 0;
            red_vehicle.GetComponent<Rigidbody2D>().velocity = vehicle_velocity_tmp[0];

            vehicle_on_space[0] = true;
        }

        if (collision.gameObject == yellow_vehicle)
        {
            vehicle_velocity_tmp[1] = yellow_vehicle.GetComponent<Rigidbody2D>().velocity;

            yellow_vehicle.GetComponent<Rigidbody2D>().drag = 0;
            yellow_vehicle.GetComponent<Rigidbody2D>().gravityScale = 0;
            yellow_vehicle.GetComponent<Rigidbody2D>().velocity = vehicle_velocity_tmp[1];

            vehicle_on_space[1] = true;
        }

        if (collision.gameObject == blue_vehicle)
        {
            vehicle_velocity_tmp[2] = blue_vehicle.GetComponent<Rigidbody2D>().velocity;

            blue_vehicle.GetComponent<Rigidbody2D>().drag = 0;
            blue_vehicle.GetComponent<Rigidbody2D>().gravityScale = 0;
            blue_vehicle.GetComponent<Rigidbody2D>().velocity = vehicle_velocity_tmp[2];

            vehicle_on_space[2] = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == red_vehicle)
        {
            red_vehicle.GetComponent<Rigidbody2D>().gravityScale = 2;

            vehicle_on_space[0] = false;
        }

        if (collision.gameObject == yellow_vehicle)
        {
            yellow_vehicle.GetComponent<Rigidbody2D>().gravityScale = 0.5f;

            vehicle_on_space[1] = false;
        }

        if (collision.gameObject == blue_vehicle)
        {
            blue_vehicle.GetComponent<Rigidbody2D>().gravityScale = 0.5f;

            vehicle_on_space[2] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (vehicle_on_space[0])
        {
            vehicle_on_space_cnt[0] += Time.deltaTime;

            vehicle_warning_text[0].text = (5 - vehicle_on_space_cnt[0]).ToString("F1");

            if (vehicle_on_space_cnt[0] >= 5)
            {
                vehicle_destroyed_effect[0] = (GameObject)Instantiate(vehicle_destroyed_effect_obj);
                vehicle_destroyed_effect[0].transform.position = red_vehicle.transform.position;

                Destroy(red_vehicle);
                vehicle_on_space_cnt[0] = 0;
            }

            vehicle_warning_text[0].transform.localScale = new Vector3(1.3f + 0.2f * Mathf.Sqrt(warning_text_effect_cnt[0]), 1.3f + 0.2f * Mathf.Sqrt(warning_text_effect_cnt[0]), 154);

            if (warning_text_effect[0])
            {
                warning_text_effect_cnt[0] += Time.deltaTime;

                if (warning_text_effect_cnt[0] >= 1)
                {
                    warning_text_effect_cnt[0] = 1;

                    warning_text_effect[0] = false;
                }
            }
            else
            {
                warning_text_effect_cnt[0] -= Time.deltaTime;

                if (warning_text_effect_cnt[0] <= 0)
                {
                    warning_text_effect_cnt[0] = 0;

                    warning_text_effect[0] = true;
                }
            }
        }
        else
        {
            vehicle_on_space_cnt[0] = 0;

            vehicle_warning_text[0].text = null;
        }

        if (vehicle_on_space[1])
        {
            vehicle_on_space_cnt[1] += Time.deltaTime;

            vehicle_warning_text[1].text = (5 - vehicle_on_space_cnt[1]).ToString("F1");

            if (vehicle_on_space_cnt[1] >= 5)
            {
                vehicle_destroyed_effect[1] = (GameObject)Instantiate(vehicle_destroyed_effect_obj);
                vehicle_destroyed_effect[1].transform.position = yellow_vehicle.transform.position;

                Destroy(yellow_vehicle);
                vehicle_on_space_cnt[1] = 0;
            }

            vehicle_warning_text[1].transform.localScale = new Vector3(1.3f + 0.2f * Mathf.Sqrt(warning_text_effect_cnt[1]), 1.3f + 0.2f * Mathf.Sqrt(warning_text_effect_cnt[1]), 154);

            if (warning_text_effect[1])
            {
                warning_text_effect_cnt[1] += Time.deltaTime;

                if (warning_text_effect_cnt[1] >= 1)
                {
                    warning_text_effect_cnt[1] = 1;

                    warning_text_effect[1] = false;
                }
            }
            else
            {
                warning_text_effect_cnt[1] -= Time.deltaTime;

                if (warning_text_effect_cnt[1] <= 0)
                {
                    warning_text_effect_cnt[1] = 0;

                    warning_text_effect[1] = true;
                }
            }
        }
        else
        {
            vehicle_on_space_cnt[1] = 0;

            vehicle_warning_text[1].text = null;
        }

        if (vehicle_on_space[2])
        {
            vehicle_on_space_cnt[2] += Time.deltaTime;

            vehicle_warning_text[2].text = (5 - vehicle_on_space_cnt[2]).ToString("F1");

            if (vehicle_on_space_cnt[2] >= 5)
            {
                vehicle_destroyed_effect[2] = (GameObject)Instantiate(vehicle_destroyed_effect_obj);
                vehicle_destroyed_effect[2].transform.position = blue_vehicle.transform.position;

                Destroy(blue_vehicle);
                vehicle_on_space_cnt[2] = 0;
            }

            vehicle_warning_text[2].transform.localScale = new Vector3(1.3f + 0.2f * Mathf.Sqrt(warning_text_effect_cnt[2]), 1.3f + 0.2f * Mathf.Sqrt(warning_text_effect_cnt[2]), 154);

            if (warning_text_effect[2])
            {
                warning_text_effect_cnt[2] += Time.deltaTime;

                if (warning_text_effect_cnt[2] >= 1)
                {
                    warning_text_effect_cnt[2] = 1;

                    warning_text_effect[2] = false;
                }
            }
            else
            {
                warning_text_effect_cnt[2] -= Time.deltaTime;

                if (warning_text_effect_cnt[2] <= 0)
                {
                    warning_text_effect_cnt[2] = 0;

                    warning_text_effect[2] = true;
                }
            }
        }
        else
        {
            vehicle_on_space_cnt[2] = 0;

            vehicle_warning_text[2].text = null;
        }
    }
}
