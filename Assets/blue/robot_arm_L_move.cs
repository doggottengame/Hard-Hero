using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class robot_arm_L_move : MonoBehaviour
{
    public GameObject camera_L;

    public GameObject red_vehicle;
    public GameObject yellow_vehicle;
    public GameObject black_vehicle;

    public GameObject vehicle_destroyed_effect_obj;
    public GameObject vehicle_destroyed_dont_count_effect_obj;

    private GameObject vehicle_destroyed_effect;
    private GameObject vehicle_destroyed_dont_count_effect;

    GameObject hand;
    GameObject bot;
    GameObject fire_B_obj;
    GameObject fire_F_obj;
    GameObject fire_L;
    GameObject fire_R;

    float fire_U_cnt = 0;
    float fire_D_cnt = 0;
    float fire_R_cnt = 0;
    float fire_L_cnt = 0;

    float bot_cnt;
    float hand_cnt = 0;
    float combine_cnt = 0;

    float velocity;

    public bool vehicle_bot_get;

    bool go_U;
    bool go_R;
    bool go_L;
    bool go_D;

    bool combine_motion;
    bool robot_hand_get;

    public Vector2 pos_to_rotate;

    public Text velocity_text;

    Rigidbody2D blue_vehicle;

    red_robot_move red_vehicle_script;

    // Start is called before the first frame update
    void Start()
    {
        blue_vehicle = gameObject.GetComponent<Rigidbody2D>();

        bot = gameObject.transform.GetChild(0).gameObject;
        hand = gameObject.transform.GetChild(1).gameObject;
        fire_B_obj = gameObject.transform.GetChild(2).gameObject;
        fire_R = gameObject.transform.GetChild(3).gameObject;
        fire_L = gameObject.transform.GetChild(4).gameObject;
        fire_F_obj = gameObject.transform.GetChild(5).gameObject;

        red_vehicle_script = red_vehicle.GetComponent<red_robot_move>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (red_vehicle != null)
        {
            if (collision.gameObject == red_vehicle)
            {
                if (!red_vehicle_script.combine_R_ready)
                {
                    Vector2 tmp = gameObject.transform.position;
                    blue_vehicle.AddForce((tmp - collision.contacts[0].point), ForceMode2D.Impulse);
                }
            }
        }

        if (!vehicle_bot_get)
        {
            if (velocity >= 50)
            {
                if (collision.gameObject != red_vehicle && collision.gameObject != yellow_vehicle && collision.gameObject != black_vehicle)
                {
                    vehicle_destroyed_effect = (GameObject)Instantiate(vehicle_destroyed_effect_obj);
                    vehicle_destroyed_effect.transform.position = gameObject.transform.position;

                    Destroy(gameObject);
                }
                else
                {
                    vehicle_destroyed_effect = (GameObject)Instantiate(vehicle_destroyed_effect_obj);
                    vehicle_destroyed_effect.transform.position = gameObject.transform.position;

                    vehicle_destroyed_dont_count_effect = (GameObject)Instantiate(vehicle_destroyed_dont_count_effect_obj);
                    vehicle_destroyed_dont_count_effect.transform.position = collision.gameObject.transform.position;

                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
    
    private void Update()
    {
        if (go_U)
        {
            if (fire_U_cnt <= 1)
            {
                fire_U_cnt += Time.deltaTime;
            }
            else
            {
                fire_U_cnt = 1;
            }
        }
        else
        {
            if (fire_U_cnt >= 0)
            {
                fire_U_cnt -= Time.deltaTime;
            }
            else
            {
                fire_U_cnt = 0;
            }
        }

        if (go_D)
        {
            if (fire_D_cnt <= 1)
            {
                fire_D_cnt += Time.deltaTime;
            }
            else
            {
                fire_D_cnt = 1;
            }
        }
        else
        {
            if (fire_D_cnt >= 0)
            {
                fire_D_cnt -= Time.deltaTime;
            }
            else
            {
                fire_D_cnt = 0;
            }
        }

        if (go_R)
        {
            if (!vehicle_bot_get && fire_R_cnt > 0)
            {
                fire_B_obj.SetActive(true);
                fire_B_obj.transform.localScale = new Vector3(1.2f, 0.5f + 2.5f * fire_R_cnt, 1);
                fire_B_obj.transform.localPosition = new Vector3(-0.92f - 1.13f * fire_R_cnt, 0, 1);
            }
            else
            {
                fire_B_obj.SetActive(false);
            }

            if (fire_R_cnt <= 3)
            {
                fire_R_cnt += 0.8f * Time.deltaTime;
            }
            else
            {
                fire_R_cnt = 3;
            }
        }
        else
        {
            fire_B_obj.transform.localScale = new Vector3(1.2f, 0.5f + 2.5f * fire_R_cnt, 1);
            fire_B_obj.transform.localPosition = new Vector3(-0.92f - 1.13f * fire_R_cnt, 0, 1);

            if (vehicle_bot_get)
            {
                fire_B_obj.SetActive(false);
            }

            if (fire_R_cnt > 0)
            {
                fire_R_cnt -= Time.deltaTime;
            }
            else
            {
                fire_R_cnt = 0;

                fire_B_obj.SetActive(false);
            }
        }

        if (go_L)
        {
            if (!vehicle_bot_get)
            {
                fire_F_obj.SetActive(true);
            }
            else
            {
                fire_F_obj.SetActive(false);
            }

            if (fire_L_cnt <= 3)
            {
                fire_L_cnt += 0.8f * Time.deltaTime;
            }
            else
            {
                fire_L_cnt = 3;
            }
        }
        else
        {
            fire_F_obj.SetActive(false);

            if (fire_L_cnt >= 0)
            {
                fire_L_cnt -= Time.deltaTime;
            }
            else
            {
                fire_L_cnt = 0;
            }
        }

        if (Input.GetKey("w"))
        {
            go_U = true;
        }
        else
        {
            go_U = false;
        }

        if (Input.GetKey("s"))
        {
            go_D = true;
        }
        else
        {
            go_D = false;
        }

        if (Input.GetKey("d"))
        {
            go_R = true;
        }
        else
        {
            go_R = false;
        }

        if (Input.GetKey("a"))
        {
            go_L = true;
        }
        else
        {
            go_L = false;
        }

        if (combine_motion)
        {
            combine_cnt += Time.deltaTime;
        }

        if (vehicle_bot_get)
        {
            try
            {
                red_vehicle_script.combine_to_blue = true;
            }
            finally
            {
                red_vehicle_script.combine_to_blue = true;
            }
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            gameObject.transform.parent = red_vehicle.transform;

            if (bot_cnt < 1)
            {
                bot_cnt += Time.deltaTime;
                bot.transform.GetChild(0).transform.localPosition = new Vector3(0.48f * Mathf.Sqrt(bot_cnt), 0, -0.1f);
            }
            else
            {
                bot.transform.GetChild(0).transform.localPosition = new Vector3(0.48f, 0, -0.1f);

                robot_hand_get = true;
            }
        }

        if (robot_hand_get)
        {
            if (hand_cnt < 1)
            {
                hand_cnt += Time.deltaTime;
                hand.transform.localRotation = Quaternion.Euler(0, 0, -90 * hand_cnt);
                bot.transform.localPosition = new Vector3(0, 0.2f * Mathf.Sqrt(bot_cnt), 1);
            }
            else
            {
                hand.transform.localRotation = Quaternion.Euler(0, 0, -90);
                bot.transform.localPosition = new Vector3(0, 0.2f, 1);
                combine_motion = true;
            }
        }
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        hand.transform.localPosition = new Vector3(-0.67f, 0.15f, 1);

        if (combine_motion)
        {
            if (combine_cnt < 1)
            {
                gameObject.transform.RotateAround(pos_to_rotate, Vector3.forward, 2);
            }
            else
            {
                gameObject.transform.localPosition = new Vector3(-0.825f, -0.12f, 0);
                gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
                hand.transform.localRotation = Quaternion.Euler(0, 0, -90);
                combine_motion = false;
                fire_R.SetActive(false);
                fire_L.SetActive(false);
                red_vehicle_script.combine_to_blue_complete = true;
                camera_L.SetActive(false);
                Destroy(this);
            }
        }

        if (!vehicle_bot_get)
        {
            velocity = 3 * blue_vehicle.velocity.magnitude;

            velocity_text.text = velocity.ToString("F2") + "km/h";

            blue_vehicle.AddForce(new Vector3(5 * (fire_R_cnt - fire_L_cnt), 10 * (0.45f + fire_U_cnt - fire_D_cnt), 0));
        }
    }

    private void LateUpdate()
    {
        if (Mathf.Abs(gameObject.transform.position.x - Camera.main.transform.position.x) < 10 && Mathf.Abs(gameObject.transform.position.y - Camera.main.transform.position.y) < 7.3)
        {
            camera_L.SetActive(false);
        }
        else
        {
            camera_L.SetActive(true);

            camera_L.transform.position = new Vector3(gameObject.transform.position.x - (gameObject.transform.position.x - camera_L.transform.position.x) / 4, gameObject.transform.position.y - (gameObject.transform.position.y - camera_L.transform.position.y) / 4, -15);
        }
    }
}
