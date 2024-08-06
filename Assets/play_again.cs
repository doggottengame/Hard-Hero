using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class play_again : MonoBehaviour
{
    bool scene_load = false;

    float gameover_scene_change_cnt = 0;

    void Update()
    {
        gameover_scene_change_cnt += Time.deltaTime;

        if (gameover_scene_change_cnt >= 3 && !scene_load)
        {
            scene_load = true;
            SceneManager.LoadSceneAsync(1); //Playing
        }
    }
}
