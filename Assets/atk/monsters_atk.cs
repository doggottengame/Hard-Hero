using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsters_atk : MonoBehaviour
{
    public GameObject[] dont_destroy = new GameObject[3];

    public GameObject[] game_over = new GameObject[3];

    public GameObject meteor_obj;
    public GameObject obj_destroyed_obj;

    private GameObject meteor;

    float atk_start_cnt = 0;
    float atk_obj_move_cnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (atk_start_cnt < 1.5)
        {
            atk_start_cnt += Time.deltaTime;
        }
        else
        {
            if (meteor == null)
            {
                meteor = (GameObject)Instantiate(meteor_obj);
                meteor.transform.parent = gameObject.transform;
                meteor.transform.localPosition = new Vector3(0, 0.5f, -1);
                meteor.transform.localScale = new Vector3(1, 1, 1);
                meteor.transform.localRotation = Quaternion.Euler(-90, 0, 0);

                meteor.GetComponent<meteor_obj>().dont_destroy[0] = dont_destroy[0];
                meteor.GetComponent<meteor_obj>().dont_destroy[1] = dont_destroy[1];
                meteor.GetComponent<meteor_obj>().dont_destroy[2] = dont_destroy[2];
                meteor.GetComponent<meteor_obj>().dont_destroy[3] = dont_destroy[3];

                meteor.GetComponent<meteor_obj>().game_over[0] = game_over[0];
                meteor.GetComponent<meteor_obj>().game_over[1] = game_over[1];
                meteor.GetComponent<meteor_obj>().game_over[2] = game_over[2];

                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.469f, 0.469f, 0);
            }
        }

        if (meteor != null)
        {
            atk_obj_move_cnt += Time.deltaTime;

            meteor.transform.localPosition = new Vector3(0, 0.5f - Mathf.Sqrt(atk_obj_move_cnt) / 1.5f, -1);
        }
    }
}
