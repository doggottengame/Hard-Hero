using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class black_combine_part : MonoBehaviour
{
    public GameObject combine_vehicle;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == combine_vehicle)
        {
            if (gameObject.transform.parent.GetComponent<black_vehicle_move>().combine_ready_start)
            {
                gameObject.transform.parent.GetComponent<black_vehicle_move>().combine_complete = true;
                collision.gameObject.GetComponent<red_robot_move>().combine_to_black_complete = true;
                Destroy(this);
            }
        }
    }
}
