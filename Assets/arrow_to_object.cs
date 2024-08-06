using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_to_object : MonoBehaviour
{
    public GameObject red_vehicle;

    public Transform black_vehicle;
    public Transform monster;

    private Transform target_object;

    red_robot_move combine_confirm;

    Vector2 tmp;

    private void Start()
    {
        combine_confirm = red_vehicle.GetComponent<red_robot_move>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!combine_confirm.combine_all_complete)
        {
            target_object = black_vehicle;
        }
        else
        {
            target_object = monster;
        }

        tmp = target_object.position - gameObject.transform.position;

        float angle = Mathf.Atan2(tmp.y, tmp.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
