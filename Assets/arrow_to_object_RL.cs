using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_to_object_RL : MonoBehaviour
{
    public Transform target_object;

    Vector2 tmp;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target_object != null)
        {
            tmp = target_object.position - gameObject.transform.position;

            float angle = Mathf.Atan2(tmp.y, tmp.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
