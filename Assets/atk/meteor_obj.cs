using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteor_obj : MonoBehaviour
{
    public GameObject[] dont_destroy = new GameObject[3];

    public GameObject[] game_over = new GameObject[3];

    public GameObject atk_destroy_obj;
    public GameObject atk_game_over_destroy_obj;

    private GameObject atk_destroy;

    bool obj_null = true;

    Vector2 meteor_pos;
    Vector2 red_vehicle_pos;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != dont_destroy[0] && collision.gameObject != dont_destroy[1] && collision.gameObject != dont_destroy[2] && collision.gameObject != dont_destroy[3])
        {
            if (collision.gameObject == game_over[0] || collision.gameObject == game_over[1] || collision.gameObject == game_over[2])
            {
                if (obj_null)
                {
                    atk_destroy = (GameObject)Instantiate(atk_destroy_obj);
                    atk_destroy.transform.position = gameObject.transform.position;
                    atk_destroy.transform.GetChild(0).GetComponent<AudioSource>().volume = Mathf.Pow(4, -(meteor_pos - red_vehicle_pos).sqrMagnitude) / 4;

                    atk_destroy = (GameObject)Instantiate(atk_game_over_destroy_obj);
                    atk_destroy.transform.position = collision.gameObject.transform.position;

                    Destroy(collision.gameObject);
                    Destroy(gameObject.transform.parent.gameObject);

                    obj_null = false;
                }
            }
            else
            {
                if (obj_null)
                {
                    atk_destroy = (GameObject)Instantiate(atk_destroy_obj);
                    atk_destroy.transform.position = gameObject.transform.position;
                    atk_destroy.transform.GetChild(0).GetComponent<AudioSource>().volume = Mathf.Pow(4, -(meteor_pos - red_vehicle_pos).sqrMagnitude) / 4;

                    atk_destroy = (GameObject)Instantiate(atk_destroy_obj);
                    atk_destroy.transform.position = collision.gameObject.transform.position;
                    atk_destroy.transform.GetChild(0).GetComponent<AudioSource>().volume = Mathf.Pow(4, -(meteor_pos - red_vehicle_pos).sqrMagnitude) / 4;

                    Destroy(collision.gameObject);
                    Destroy(gameObject.transform.parent.gameObject);

                    obj_null = false;
                }
            }
        }
        else
        {
            atk_destroy = (GameObject)Instantiate(atk_destroy_obj);
            atk_destroy.transform.position = gameObject.transform.position;
            atk_destroy.transform.GetChild(0).GetComponent<AudioSource>().volume = Mathf.Pow(4, -(meteor_pos - red_vehicle_pos).sqrMagnitude) / 4;

            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    private void Update()
    {
        meteor_pos = gameObject.transform.position;

        if (game_over[0] != null)
        {
            red_vehicle_pos = game_over[0].transform.position;
        }

        if ((meteor_pos - red_vehicle_pos).sqrMagnitude < 40 && !gameObject.GetComponent<AudioSource>().isPlaying)
        {
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<AudioSource>().volume = Mathf.Pow((meteor_pos - red_vehicle_pos).sqrMagnitude, -4);
        }
    }
}
