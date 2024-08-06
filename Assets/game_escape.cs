using UnityEngine;
using UnityEngine.SceneManagement;

public class game_escape : MonoBehaviour
{
    public GameObject window_obj;

    private GameObject window;

    public static bool window_on;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!window_on)
            {
                Time.timeScale = 0;

                window = Instantiate(window_obj);
                window.transform.SetParent(gameObject.transform);
                window.transform.localPosition = new Vector3(0, 0, 200);
                window.transform.localScale = new Vector3(1200, 600, 105);

                window_on = true;
            }
            else
            {
                Time.timeScale = 1;

                gameObject.GetComponent<AudioSource>().Play();

                Destroy(window);

                window_on = false;
            }
        }

        if (window_on)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Time.timeScale = 1;

                Application.Quit();

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;

#endif
            }
            else if (Input.GetKeyDown(KeyCode.Backspace))
            {
                SceneManager.LoadSceneAsync(0);
            }
        }
    }
}
