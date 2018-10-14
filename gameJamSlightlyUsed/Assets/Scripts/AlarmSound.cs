using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AlarmSound : MonoBehaviour
{
    public static bool hordeOn = false;
    public AudioSource hordeStart;
    public static AlarmSound instance = null;
    public AudioMain audioMain;
    public float currentTime = 0.0f;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        hordeStart.loop = true;
        hordeStart.Play();
        hordeStart.Pause();
        hordeStart.volume = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (hordeOn)
        {

            AudioMain.instance.turnDownGameMusic();
            hordeStart.UnPause();
            if (hordeStart.volume < 0.4f)
            {
                hordeStart.volume += Time.deltaTime * 0.5f;
            }
        }
        else
        {

            if (hordeStart.volume > 0.1f)
            {
                hordeStart.volume -= Time.deltaTime * 0.3f;
            }
            else
            {
                AudioMain.instance.turnUpGameMusic();
                hordeStart.Pause();
            }




        }
    }
}