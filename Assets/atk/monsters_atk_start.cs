using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsters_atk_start : MonoBehaviour
{
    public GameObject[] dont_destroy = new GameObject[3];

    public GameObject[] game_over = new GameObject[3];

    public GameObject red_vehicle;

    public GameObject atk_obj;

    private GameObject atk;

    float atk_cnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (atk == null && red_vehicle != null)
        {
            if (atk_cnt < 6)
            {
                atk_cnt += Time.deltaTime;
            }
            else
            {
                gameObject.GetComponent<AudioSource>().Play();
                atk = (GameObject)Instantiate(atk_obj);
                atk.transform.rotation = Quaternion.Euler(0, 0, -35);
                atk.transform.localScale = new Vector3(2, 800, 1);
                atk.transform.position = new Vector3(red_vehicle.transform.position.x + 140, red_vehicle.transform.position.y + 200, -2);

                atk.GetComponent<monsters_atk>().dont_destroy[0] = dont_destroy[0];
                atk.GetComponent<monsters_atk>().dont_destroy[1] = dont_destroy[1];
                atk.GetComponent<monsters_atk>().dont_destroy[2] = dont_destroy[2];
                atk.GetComponent<monsters_atk>().dont_destroy[3] = dont_destroy[3];

                atk.GetComponent<monsters_atk>().game_over[0] = game_over[0];
                atk.GetComponent<monsters_atk>().game_over[1] = game_over[1];
                atk.GetComponent<monsters_atk>().game_over[2] = game_over[2];

                atk_cnt = 0;
            }
        }
    }
}
