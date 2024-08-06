using UnityEngine;

public class cursor_follow : MonoBehaviour
{
    public GameObject record_canvas;

    public GameObject start_button;
    public GameObject record_button;

    public GameObject btnCanvas;

    public GameObject scene_load;
    public GameObject tutorial_load;

    public GameObject record_window_obj;

    private GameObject record_window;

    AudioSource startAudio, recordAudio;

    bool button_select;

    Vector2 cursor_pos;

    private void Start()
    {
        startAudio = start_button.GetComponent<AudioSource>();
        recordAudio = record_button.GetComponent<AudioSource>();
    }

    public void EnterStartBtn()
    {
        button_select = true;
        startAudio.Play();
    }

    public void StartBtn()
    {
        tutorial_load.GetComponent<tutorial_scripts>().tutorial_on = true;
        Destroy(gameObject.transform.parent.gameObject);
        Destroy(btnCanvas);
    }

    public void OutStartBtn()
    {
        button_select = false;
    }

    public void EnterRecordBtn()
    {
        button_select = true;

        recordAudio.Play();
    }

    public void RecordBtn()
    {
        record_window = (GameObject)Instantiate(record_window_obj);
        record_window.transform.SetParent(record_canvas.transform);
        record_window.transform.localPosition = new Vector3(0, 0, 400);
        record_window.transform.localScale = new Vector3(2160, 1944, 216);
    }

    public void OutRecordBtn()
    {
        button_select = false;
    }

    // Update is called once per frame
    void Update()
    {
        cursor_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Cursor.visible = false;

        transform.position = new Vector3(cursor_pos.x + 0.35f, cursor_pos.y - 0.26f, -9);

        if (cursor_pos.x <= -8.5)
        {
            transform.position = new Vector3(-8.5f, cursor_pos.y - 0.26f, -9);
        }
        else if (cursor_pos.x >= 9)
        {
            transform.position = new Vector3(9, cursor_pos.y - 0.26f, -9);
        }

        if (cursor_pos.y >= 4.7)
        {
            transform.position = new Vector3(cursor_pos.x + 0.35f, 4.7f, -9);
        }
        else if (cursor_pos.y <= -5)
        {
            transform.position = new Vector3(cursor_pos.x + 0.35f, -5, -9);
        }

        if (Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<AudioSource>().Play();

            if (!button_select && record_window != null)
            {
                Destroy(record_window);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!game_escape.window_on)
            {
                if (record_window != null)
                {
                    Destroy(record_window);
                }

                scene_load.GetComponent<first_animation_control>().start = true;
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }
}
