using UnityEngine;
using UnityEngine.SceneManagement;

public class first_animation_control : MonoBehaviour
{
    public GameObject scene_obj_0;
    public GameObject scene_obj_1;
    public GameObject scene_obj_2;

    public bool start = false;

    bool scene_load = false;
    bool scene_load_complete = false;

    float first_scene_cnt = 0;
    float second_scene_cnt = 0;
    float third_scene_cnt = 0;

    float scene_load_cnt = 0;

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            if (first_scene_cnt < 1)
            {
                scene_obj_0.SetActive(true);

                first_scene_cnt += Time.deltaTime;

                scene_obj_0.transform.position = new Vector3(11 - 10.43f * Mathf.Sqrt(first_scene_cnt), -0.1141668f, -30.89056f);
            }
            else
            {
                scene_obj_0.transform.position = new Vector3(0.57f, -0.1141668f, -30.89056f);

                if (second_scene_cnt < 1)
                {
                    scene_obj_1.SetActive(true);

                    second_scene_cnt += Time.deltaTime;

                    scene_obj_1.transform.position = new Vector3(0.4405998f, 8.1f - 7.9858332f * Mathf.Sqrt(second_scene_cnt), -30.89056f);
                }
                else
                {
                    scene_obj_1.transform.position = new Vector3(0.4405998f, -0.1141668f, -30.89056f);

                    if (third_scene_cnt < 1)
                    {
                        scene_obj_2.SetActive(true);

                        third_scene_cnt += Time.deltaTime;

                        scene_obj_2.transform.position = new Vector3(-7 + 7.6017848f * Mathf.Sqrt(third_scene_cnt), 0.3276866f, -36.03739f);
                    }
                    else
                    {
                        scene_obj_2.transform.position = new Vector3(0.6017848f, 0.3276866f, -36.03739f);

                        scene_load = true;
                    }
                }
            }
        }
        else
        {
            scene_obj_0.SetActive(false);
            scene_obj_1.SetActive(false);
            scene_obj_2.SetActive(false);
        }

        if (scene_load && !scene_load_complete)
        {
            scene_load_cnt += Time.deltaTime;

            if (scene_load_cnt > 1)
            {
                scene_load_complete = true;
                SceneManager.LoadSceneAsync(1); //Playing
            }
        }
    }
}