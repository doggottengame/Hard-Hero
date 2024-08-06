using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class black_vehicle_move : MonoBehaviour
{
    public GameObject red_vehicle;
    public GameObject yellow_vehicle;
    public GameObject blue_vehicle;

    GameObject combine_part;
    GameObject combine_part_R;
    GameObject combine_part_L;
    GameObject leg_R;
    GameObject leg_L;

    float combine_part_cnt = 0;
    float combine_part_complete_cnt = 0;
    float leg_motion_cnt = 0;

    public bool combine_ready_start;
    public bool combine_complete;

    red_robot_move combine_confirm;

    // Start is called before the first frame update
    void Start()
    {
        combine_part = gameObject.transform.GetChild(0).gameObject;
        combine_part_R = gameObject.transform.GetChild(1).gameObject;
        combine_part_L = gameObject.transform.GetChild(2).gameObject;
        leg_R = gameObject.transform.GetChild(3).gameObject;
        leg_L = gameObject.transform.GetChild(4).gameObject;

        combine_confirm = red_vehicle.GetComponent<red_robot_move>();
    }

    //166
    // Update is called once per frame
    void Update()
    {
        if (combine_confirm.combine_D_ready)
        {
            if (Mathf.Abs(red_vehicle.transform.position.x - gameObject.transform.position.x) < 25 && Mathf.Abs(red_vehicle.transform.position.y - gameObject.transform.position.y) < 14.5)
            {
                combine_ready_start = true;
            }
        }

        if (combine_ready_start && !combine_complete)
        {
            gameObject.transform.position = new Vector3(166, -2.5f + 1.7f * Mathf.Sqrt(leg_motion_cnt), -5);
            leg_R.transform.localPosition = new Vector3(0, -0.73f * Mathf.Sqrt(leg_motion_cnt), 0.1f);
            leg_L.transform.localPosition = new Vector3(0, -0.73f * Mathf.Sqrt(leg_motion_cnt), 0.1f);
            leg_R.transform.localScale = new Vector3(1, 1.2f * Mathf.Sqrt(leg_motion_cnt), 1);
            leg_L.transform.localScale = new Vector3(1, 1.2f * Mathf.Sqrt(leg_motion_cnt), 1);

            combine_part_R.transform.localPosition = new Vector3(0.14f * Mathf.Sqrt(combine_part_cnt), 0, -1);
            combine_part_L.transform.localPosition = new Vector3(-0.14f * Mathf.Sqrt(combine_part_cnt), 0, -1); //-0.8

            if (leg_motion_cnt < 1)
            {
                combine_part_cnt += Time.deltaTime;
                leg_motion_cnt += Time.deltaTime;
            }
            else
            {
                combine_part_cnt = 1;
                leg_motion_cnt = 1;
            }
        }

        if (combine_complete)
        {
            gameObject.transform.position = new Vector3(166, -0.8f, -5);
            combine_part_R.transform.localPosition = new Vector3(0.04f - 0.1f * Mathf.Sqrt(combine_part_complete_cnt), 0, -1);
            combine_part_L.transform.localPosition = new Vector3(0.04f + 0.1f * Mathf.Sqrt(combine_part_complete_cnt), 0, -1);
            leg_R.transform.localPosition = new Vector3(0, -0.73f, 0.1f);
            leg_L.transform.localPosition = new Vector3(0, -0.73f, 0.1f);
            leg_R.transform.localScale = new Vector3(1, 1.2f, 1);
            leg_L.transform.localScale = new Vector3(1, 1.2f, 1);

            if (combine_part_complete_cnt <1)
            {
                combine_part_complete_cnt += Time.deltaTime;
            }
            else
            {
                gameObject.transform.parent = red_vehicle.transform.GetChild(2).transform;

                Destroy(gameObject.transform.GetChild(0).GetComponent<Rigidbody2D>());
                gameObject.transform.localPosition = new Vector3(0, -0.86f, -0.1f);
                combine_confirm.combine_to_black_complete = true;
                Destroy(this);
            }
        }
    }
}
