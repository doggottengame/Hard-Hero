using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class robot_arm_R_move : MonoBehaviour
{
    public GameObject camera_R;

    public GameObject red_vehicle;
    public GameObject blue_vehicle;
    public GameObject black_vehicle;

    public GameObject vehicle_destroyed_effect_obj;
    public GameObject vehicle_destroyed_dont_count_effect_obj;

    private GameObject vehicle_destroyed_effect;
    private GameObject vehicle_destroyed_dont_count_effect;

    GameObject fire_R_obj;
    GameObject fire_L_obj;
    GameObject hand;

    float fire_U_cnt = 0;
    float fire_D_cnt = 0;
    float fire_R_cnt = 0;
    float fire_L_cnt = 0;

    float degree_R = 0;
    float degree_L = 0;
    float degree = 0;

    float hand_cnt;

    float combine_cnt = 0;

    float velocity;

    public bool combine_motion;

    bool fire_U;
    bool fire_D;
    bool fire_R;
    bool fire_L;

    bool robot_hand_get;

    public Vector2 pos_to_rotate;

    public Text velocity_text;

    Rigidbody2D yellow_vehicle;

    red_robot_move red_vehicle_script;

    // Start is called before the first frame update
    void Start()
    {
        yellow_vehicle = gameObject.GetComponent<Rigidbody2D>();

        fire_R_obj = gameObject.transform.GetChild(0).gameObject;
        fire_L_obj = gameObject.transform.GetChild(1).gameObject;
        hand = gameObject.transform.GetChild(2).gameObject;

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
                    yellow_vehicle.AddForce((tmp - collision.contacts[0].point), ForceMode2D.Impulse);
                }
            }
        }

        if (!robot_hand_get)
        {
            if (velocity >= 50)
            {
                if (collision.gameObject != red_vehicle && collision.gameObject != blue_vehicle && collision.gameObject != black_vehicle)
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
        if (fire_U)
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

        if (fire_D)
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

        if (fire_R)
        {
            if (fire_R_cnt <= 3)
            {
                fire_R_cnt += 0.8f * Time.deltaTime;
            }
            else
            {
                fire_R_cnt = 3;
            }

            if (degree_R <= 80)
            {
                degree_R += 32 * Time.deltaTime;
            }
            else
            {
                degree_R = 80;
            }
        }
        else
        {
            if (fire_R_cnt >= 0)
            {
                fire_R_cnt -= Time.deltaTime;
            }
            else
            {
                fire_R_cnt = 0;
            }

            if (degree_R > 0)
            {
                degree_R -= 40 * Time.deltaTime;
            }
            else
            {
                degree_R = 0;
            }
        }

        if (fire_L)
        {
            if (fire_L_cnt <= 3)
            {
                fire_L_cnt += 0.8f * Time.deltaTime;
            }
            else
            {
                fire_L_cnt = 3;
            }

            if (degree_L >= -80)
            {
                degree_L -= 32 * Time.deltaTime;
            }
            else
            {
                degree_L = -80;
            }
        }
        else
        {
            if (fire_L_cnt >= 0)
            {
                fire_L_cnt -= Time.deltaTime;
            }
            else
            {
                fire_L_cnt = 0;
            }

            if (degree_L < 0)
            {
                degree_L += 40 * Time.deltaTime;
            }
            else
            {
                degree_L = 0;
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            fire_U = true;
        }
        else
        {
            fire_U = false;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            fire_D = true;
        }
        else
        {
            fire_D = false;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            fire_R = true;
        }
        else
        {
            fire_R = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            fire_L = true;
        }
        else
        {
            fire_L = false;
        }

        if (combine_motion)
        {
            combine_cnt += Time.deltaTime;
        }

        if (robot_hand_get)
        {
            if (hand.transform.localPosition.x < 1)
            {
                hand_cnt += Time.deltaTime;
                hand.transform.localPosition = new Vector3(Mathf.Sqrt(hand_cnt), -0.04f, 5);
            }
            else
            {
                hand.transform.localPosition = new Vector3(1, -0.04f, 5);
                gameObject.transform.localPosition = new Vector3(0.925f, -0.05f);
                gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
                red_vehicle_script.combine_to_yellow_complete = true;
                camera_R.SetActive(false);
                Destroy(this);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (combine_motion)
        {
            if (combine_cnt < 1)
            {
                gameObject.transform.RotateAround(pos_to_rotate, Vector3.forward, -2);
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, -90);
                red_vehicle_script.combine_to_yellow = true;
                Destroy(gameObject.GetComponent<Rigidbody2D>());
                gameObject.transform.parent = red_vehicle.transform;
                combine_motion = false;
                robot_hand_get = true;
            }

            fire_R_obj.transform.rotation = Quaternion.Euler(0, 0, -gameObject.transform.rotation.z);
            fire_L_obj.transform.rotation = Quaternion.Euler(0, 0, -gameObject.transform.rotation.z);
        }

        if (!robot_hand_get)
        {
            hand.transform.localPosition = new Vector3(0, -0.04f, 1);

            velocity = 3 * yellow_vehicle.velocity.magnitude;

            velocity_text.text = velocity.ToString("F2") + "km/h";

            yellow_vehicle.AddForce(new Vector3(5 * (fire_R_cnt - fire_L_cnt), 10 * (0.45f + fire_U_cnt - fire_D_cnt), 0));

            degree = degree_R + degree_L;

            fire_R_obj.transform.rotation = Quaternion.Euler(0, 0, -degree);
            fire_L_obj.transform.rotation = Quaternion.Euler(0, 0, -degree);
        }
    }

    private void LateUpdate()
    {
        if (Mathf.Abs(gameObject.transform.position.x - Camera.main.transform.position.x) < 10 && Mathf.Abs(gameObject.transform.position.y - Camera.main.transform.position.y) < 7.3)
        {
            camera_R.SetActive(false);
        }
        else
        {
            camera_R.SetActive(true);

            camera_R.transform.position = new Vector3(gameObject.transform.position.x - (gameObject.transform.position.x - camera_R.transform.position.x) / 4, gameObject.transform.position.y - (gameObject.transform.position.y - camera_R.transform.position.y) / 4, -15);
        }
    }
}
