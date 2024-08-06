using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class red_combine_part_L : MonoBehaviour
{
    public GameObject combine_vehicle;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == combine_vehicle)
        {
            if (gameObject.transform.parent.GetComponent<red_robot_move>().combine_L_ready)
            {
                collision.gameObject.GetComponent<robot_arm_L_move>().pos_to_rotate = collision.contacts[0].point;
                gameObject.transform.parent.GetComponent<red_robot_move>().combine_to_blue = true;
                collision.gameObject.GetComponent<robot_arm_L_move>().vehicle_bot_get = true;
                Destroy(this);
            }
        }
    }
}
