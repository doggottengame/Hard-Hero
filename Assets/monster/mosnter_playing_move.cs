using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mosnter_playing_move : MonoBehaviour
{
    GameObject head;
    GameObject fire;

    float cnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        head = gameObject.transform.GetChild(0).gameObject;
        fire = gameObject.transform.GetChild(0).gameObject;

        fire.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cnt < 1)
        {
            cnt += Time.deltaTime;

            head.transform.rotation = Quaternion.Euler(0, 0, -43 * Mathf.Sqrt(cnt));
        }
        else
        {
            cnt = 1;

            head.transform.rotation = Quaternion.Euler(0, 0, -43);

            fire.SetActive(true);
        }
    }
}
