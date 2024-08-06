using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class destroyed_and_gameover : MonoBehaviour
{
    bool scene_load;

    private void Start()
    {
        data_control.death_count++;
        data_control.now_playing = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gameObject.GetComponent<ParticleSystem>().isPlaying && !scene_load)
        {
            SceneManager.LoadSceneAsync(2); //Game_over

            scene_load = true;
        }
    }
}
