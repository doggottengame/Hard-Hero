using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class red_robot_move : MonoBehaviour
{
    public GameObject main_camera;

    public GameObject ground;

    public GameObject before_space_obj;

    public GameObject burning_obj;

    public GameObject yellow_vehicle;
    public GameObject blue_vehicle;
    public GameObject black_vehicle;

    public GameObject clear_range;

    public GameObject vehicle_destroyed_effect_obj;
    public GameObject vehicle_destroyed_dont_count_effect_obj;

    private GameObject vehicle_destroyed_effect;
    private GameObject vehicle_destroyed_dont_count_effect;

    GameObject combine_R;
    GameObject combine_L;

    GameObject body;

    GameObject fire_U_obj;
    GameObject fire_L_obj;
    GameObject fire_R_obj;

    float combine_R_cnt = 0;
    float combine_L_cnt = 0;

    float fire_up_cnt = 0;
    float fire_L_cnt = 0;
    float fire_R_cnt = 0;

    float combine_D_ready_camera_effect = 0;

    float combine_black_motion_cnt = 0;

    float velocity;

    float burning_cnt;

    float game_clear_effect_cnt = 0;
    float game_clear_cnt = 0;

    public bool combine_to_yellow = false;
    public bool combine_to_blue = false;
    public bool combine_to_yellow_complete = false;
    public bool combine_to_blue_complete = false;
    public bool combine_to_black_complete = false;
    public bool combine_all_complete = false;

    public bool combine_R_ready;
    public bool combine_L_ready;
    public bool combine_D_ready;

    bool fire_up = false;
    bool fire_L = false;
    bool fire_R = false;

    bool ready_to_combine_R = false;
    bool ready_to_combine_L = false;

    bool on_escape_earth;

    bool game_clear = false;
    bool scene_load = false;

    bool burning_ = false;

    public Slider engine_power_show;

    public Text velocity_text;

    Rigidbody2D red_vehicle;

    AudioSource vehicle_col_to_air;

    // Start is called before the first frame update
    void Start()
    {
        red_vehicle = gameObject.GetComponent<Rigidbody2D>();

        combine_L = gameObject.transform.GetChild(0).gameObject;
        combine_R = gameObject.transform.GetChild(1).gameObject;

        body = gameObject.transform.GetChild(2).gameObject;

        fire_U_obj = gameObject.transform.GetChild(3).gameObject;
        fire_R_obj = gameObject.transform.GetChild(4).gameObject;
        fire_L_obj = gameObject.transform.GetChild(5).gameObject;

        fire_U_obj.SetActive(false);
        fire_R_obj.SetActive(false);
        fire_L_obj.SetActive(false);

        vehicle_col_to_air = gameObject.GetComponent<AudioSource>();

        engine_power_show.maxValue = 20;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (velocity >= 50 && !game_clear)
        {
            if (collision.gameObject != yellow_vehicle && collision.gameObject != blue_vehicle && collision.gameObject != black_vehicle)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (combine_all_complete && collision.gameObject == clear_range && !game_clear)
        {
            data_control.now_playing = false;
            game_clear = true;

            data_control.number_of_clear++;

            PlayerPrefs.SetInt("death_count_on_this", data_control.death_count);
            PlayerPrefs.SetFloat("clear_time_on_this", data_control.play_time);

            if (!data_control.clear_)
            {
                data_control.least_death = data_control.death_count;
                data_control.clear_time_of_least_death = data_control.play_time;

                data_control.fastest_clear_time = data_control.play_time;
                data_control.death_of_fastest_clear = data_control.death_count;

                data_control.least_death = data_control.death_count;
                data_control.clear_time_of_least_death = data_control.play_time;

                data_control.fastest_clear_time = data_control.play_time;
                data_control.death_of_fastest_clear = data_control.death_count;

                PlayerPrefs.SetInt("least_death_did", 1);
                PlayerPrefs.SetInt("fastest_clear_did", 1);

                data_control.clear_ = true;
            }
            else
            {
                if (data_control.least_death > data_control.death_count)
                {
                    data_control.least_death = data_control.death_count;
                    data_control.clear_time_of_least_death = data_control.play_time;

                    PlayerPrefs.SetInt("least_death_did", 1);
                }
                else if (data_control.least_death == data_control.death_count)
                {
                    if (data_control.clear_time_of_least_death > data_control.play_time)
                    {
                        data_control.clear_time_of_least_death = data_control.play_time;

                        PlayerPrefs.SetInt("least_death_did", 1);
                    }
                }

                if (data_control.fastest_clear_time > data_control.play_time)
                {
                    data_control.fastest_clear_time = data_control.play_time;
                    data_control.death_of_fastest_clear = data_control.death_count;

                    PlayerPrefs.SetInt("fastest_clear_did", 1);
                }
                else if (data_control.fastest_clear_time == data_control.play_time)
                {
                    if (data_control.death_of_fastest_clear > data_control.death_count)
                    {
                        data_control.death_of_fastest_clear = data_control.death_count;

                        PlayerPrefs.SetInt("fastest_clear_did", 1);
                    }
                }
            }

            Debug.Log(PlayerPrefs.GetInt("least_death_did"));
            Debug.Log(PlayerPrefs.GetFloat("clear_time_on_this"));

            Debug.Log(PlayerPrefs.GetInt("least_death_did"));
            Debug.Log(PlayerPrefs.GetInt("fastest_clear_did"));

            data_control.death_count = 0;
            data_control.play_time = 0;
        }

        if (collision.gameObject == before_space_obj)
        {
            on_escape_earth = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == before_space_obj)
        {
            on_escape_earth = false;
        }
    }

    private void Update()
    {
        engine_power_show.value = fire_up_cnt;

        vehicle_col_to_air.volume = velocity / 400;

        if (!game_clear)
        {
            velocity = 3 * red_vehicle.velocity.magnitude;

            velocity_text.text = velocity.ToString("F2") + "km/h";

            combine_R.transform.localPosition = new Vector3((combine_R_cnt - 1) * 0.2f, 0, 0.5f);
            combine_L.transform.localPosition = new Vector3((1 - combine_L_cnt) * 0.2f, 0, 0.5f);

            if (combine_to_yellow_complete)
            {
                yellow_vehicle.transform.GetChild(2).transform.localPosition = new Vector3(1, -0.04f, 1);
                yellow_vehicle.transform.localPosition = new Vector3(0.94f, -0.05f);
                yellow_vehicle.transform.localRotation = Quaternion.Euler(0, 0, -80);
            }

            if (combine_to_blue_complete)
            {
                blue_vehicle.transform.localPosition = new Vector3(-0.84f, -0.12f, 0);
                blue_vehicle.transform.localRotation = Quaternion.Euler(0, 0, 80);
                blue_vehicle.transform.GetChild(1).transform.localRotation = Quaternion.Euler(0, 0, -90);
                blue_vehicle.transform.GetChild(1).transform.localPosition = new Vector3(-0.67f, 0.15f, 1);
            }

            if (combine_to_yellow_complete && combine_to_blue_complete)
            {
                combine_D_ready = true;

                if (main_camera.GetComponent<Camera>().orthographicSize < 14)
                {
                    combine_D_ready_camera_effect += Time.deltaTime;

                    main_camera.GetComponent<Camera>().orthographicSize = 8 + 6 * Mathf.Sqrt(combine_D_ready_camera_effect);
                }
                else
                {
                    main_camera.GetComponent<Camera>().orthographicSize = 14;
                }
            }

            if (combine_to_black_complete)
            {
                red_vehicle.constraints = RigidbodyConstraints2D.FreezeRotation;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                black_vehicle.transform.localPosition = new Vector3(0, -0.86f, -0.1f);
                black_vehicle.transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 0);
                black_vehicle.transform.GetChild(1).transform.localPosition = new Vector3(0.04f, 0, -1);
                black_vehicle.transform.GetChild(2).transform.localPosition = new Vector3(-0.04f, 0, -1);
                black_vehicle.transform.GetChild(3).transform.localPosition = new Vector3(0, -0.73f, 0.1f);
                black_vehicle.transform.GetChild(4).transform.localPosition = new Vector3(0, -0.73f, 0.1f);

                if (combine_black_motion_cnt < 1)
                {
                    combine_black_motion_cnt += Time.deltaTime;
                    body.transform.localPosition = new Vector3(0, 0.55f * Mathf.Sqrt(combine_black_motion_cnt), 0.1f);
                }
                else
                {
                    body.transform.localPosition = new Vector3(0, 0.55f, 0.1f);

                    game_clear_effect_cnt += Time.deltaTime;

                    if (game_clear_effect_cnt >= 1.5)
                    {
                        combine_all_complete = true;
                    }
                }
            }

            if (!combine_to_yellow)
            {
                if (combine_R.transform.localPosition.x > -0.1)
                {
                    combine_R_ready = true;
                }
                else
                {
                    combine_R_ready = false;
                }

                if (ready_to_combine_R)
                {
                    if (combine_R_cnt < 1)
                    {
                        combine_R_cnt += Time.deltaTime;
                    }
                    else
                    {
                        combine_R_cnt = 1;
                    }
                }
                else
                {
                    if (combine_R_cnt > 0)
                    {
                        combine_R_cnt -= Time.deltaTime;
                    }
                    else
                    {
                        combine_R_cnt = 0;
                    }
                }
            }
            else
            {
                combine_R.transform.localPosition = new Vector3(0, 0, 0.5f);
            }

            if (!combine_to_blue)
            {
                if (combine_L.transform.localPosition.x < 0.1)
                {
                    combine_L_ready = true;
                }
                else
                {
                    combine_L_ready = false;
                }

                if (ready_to_combine_L)
                {
                    if (combine_L_cnt < 1)
                    {
                        combine_L_cnt += Time.deltaTime;
                    }
                    else
                    {
                        combine_L_cnt = 1;
                    }
                }
                else
                {
                    if (combine_L_cnt > 0)
                    {
                        combine_L_cnt -= Time.deltaTime;
                    }
                    else
                    {
                        combine_L_cnt = 0;
                    }
                }
            }
            else
            {
                combine_L.transform.localPosition = new Vector3(0, 0, 0.5f);
            }

            if (fire_up)
            {
                if (fire_up_cnt < 20)
                {
                    fire_up_cnt += 5 * Time.deltaTime;

                    fire_U_obj.SetActive(true);
                    fire_U_obj.transform.localScale = new Vector3(3, 0.5f + 4 * fire_up_cnt / 20, 1);
                    fire_U_obj.transform.localPosition = new Vector3(0, -0.95f - 1.7f * fire_up_cnt / 20, 1);
                }
                else
                {
                    fire_up_cnt = 20;
                }

                fire_U_obj.GetComponent<AudioSource>().volume = fire_up_cnt / 20;
            }
            else
            {
                if (fire_up_cnt >= 0)
                {
                    fire_up_cnt -= 10 * Time.deltaTime;

                    fire_U_obj.GetComponent<AudioSource>().volume = fire_up_cnt / 20;
                }
                else
                {
                    fire_up_cnt = 0;

                    fire_U_obj.SetActive(false);
                }

                fire_U_obj.transform.localScale = new Vector3(3, 0.5f + 4 * fire_up_cnt / 20, 1);
                fire_U_obj.transform.localPosition = new Vector3(0, -0.95f - 1.7f * fire_up_cnt / 20, 1);
            }

            if (fire_R)
            {
                if (fire_R_cnt >= -1)
                {
                    fire_R_cnt -= 0.4f * Time.deltaTime;
                }
                else
                {
                    fire_R_cnt = -1;
                }

                fire_R_obj.GetComponent<AudioSource>().volume = -fire_R_cnt / 2;

                fire_R_obj.SetActive(true);
            }
            else
            {
                if (fire_R_cnt <= 0)
                {
                    fire_R_cnt += 0.5f * Time.deltaTime;

                    fire_R_obj.GetComponent<AudioSource>().volume = -fire_R_cnt / 2;
                }
                else
                {
                    fire_R_cnt = 0;

                    fire_R_obj.SetActive(false);
                }
            }

            if (fire_L)
            {
                if (fire_L_cnt <= 1)
                {
                    fire_L_cnt += 0.4f * Time.deltaTime;
                }
                else
                {
                    fire_L_cnt = 1;
                }

                fire_L_obj.GetComponent<AudioSource>().volume = fire_L_cnt / 2;

                fire_L_obj.SetActive(true);
            }
            else
            {
                if (fire_L_cnt >= 0)
                {
                    fire_L_cnt -= 0.5f * Time.deltaTime;

                    fire_L_obj.GetComponent<AudioSource>().volume = fire_L_cnt / 2;
                }
                else
                {
                    fire_L_cnt = 0;

                    fire_L_obj.SetActive(false);
                }
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                ready_to_combine_L = true;
            }
            else
            {
                ready_to_combine_L = false;
            }

            if (Input.GetKey(KeyCode.RightShift))
            {
                ready_to_combine_R = true;
            }
            else
            {
                ready_to_combine_R = false;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                fire_up = true;
            }
            else
            {
                fire_up = false;
            }

            if (Input.GetKey("q"))
            {
                fire_R = true;
            }
            else
            {
                fire_R = false;
            }

            if (Input.GetKey("e"))
            {
                fire_L = true;
            }
            else
            {
                fire_L = false;
            }
        }
        else
        {
            game_clear_cnt += Time.deltaTime;

            if (game_clear_cnt > 1 && ! scene_load)
            {
                scene_load = true;

                SceneManager.LoadSceneAsync(3);
            }
        }

        if (burning_obj != null)
        {
            burning_cnt += Time.deltaTime;
            
            if (burning_cnt > 2 && !burning_)
            {
                burning_ = true;
                data_control.death_count++;
                data_control.now_playing = false;
                Destroy(gameObject);
                SceneManager.LoadSceneAsync(2);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!game_clear)
        {
            red_vehicle.AddRelativeForce(new Vector2(0, 10) * fire_up_cnt);

            if (!combine_to_yellow_complete && !combine_to_blue_complete)
            {
                red_vehicle.AddTorque(-4 * (fire_L_cnt + fire_R_cnt));
                red_vehicle.AddRelativeForce(new Vector2(6, 0) * (fire_L_cnt + fire_R_cnt));
            }
            else
            {
                red_vehicle.AddRelativeForce(new Vector2(50, 0) * (fire_L_cnt + fire_R_cnt));

                red_vehicle.constraints = RigidbodyConstraints2D.FreezeRotation;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (on_escape_earth)
            {
                red_vehicle.AddForce(new Vector2(0, -300));
            }
        }
    }

    private void LateUpdate()
    {
        if (!combine_D_ready)
        {
            main_camera.transform.GetChild(0).gameObject.SetActive(false);

            if (gameObject.transform.position.y > 1.6)
            {
                main_camera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
            }
            else
            {
                main_camera.transform.position = new Vector3(gameObject.transform.position.x, 1.6f, -10);
            }
        }
        else
        {
            main_camera.transform.GetChild(0).gameObject.SetActive(true);

            if (gameObject.transform.position.y > 7)
            {
                main_camera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
            }
            else
            {
                main_camera.transform.position = new Vector3(gameObject.transform.position.x, 7, -10);
            }
        }
    }
}
