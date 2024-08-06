using UnityEngine;

public class tutorial_scripts : MonoBehaviour
{
    public GameObject first_animation_scene;

    public bool tutorial_on = false;

    GameObject[] tutorial_scene = new GameObject[4];

    int tutorial_num = 0;

    // Start is called before the first frame update
    void Start()
    {
        tutorial_scene[0] = gameObject.transform.GetChild(0).gameObject;
        tutorial_scene[1] = gameObject.transform.GetChild(1).gameObject;
        tutorial_scene[2] = gameObject.transform.GetChild(2).gameObject;
        tutorial_scene[3] = gameObject.transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!tutorial_on)
        {
            tutorial_scene[0].SetActive(false);
            tutorial_scene[1].SetActive(false);
            tutorial_scene[2].SetActive(false);
            tutorial_scene[3].SetActive(false);
        }
        else
        {
            tutorial_scene[tutorial_num].SetActive(true);

            if (Input.anyKeyDown)
            {
                if (tutorial_num == 0)
                {
                    tutorial_num++;
                    tutorial_scene[0].SetActive(false);
                }
                else if (tutorial_num == 1)
                {
                    tutorial_num++;
                    tutorial_scene[1].SetActive(false);
                }
                else if (tutorial_num == 2)
                {
                    tutorial_num++;
                    tutorial_scene[2].SetActive(false);
                }
                else if (tutorial_num == 3)
                {
                    first_animation_scene.GetComponent<first_animation_control>().start = true;

                    Destroy(gameObject);
                }
            }
        }
    }
}
