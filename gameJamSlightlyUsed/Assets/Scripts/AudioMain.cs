using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioMain : MonoBehaviour {
    public bool hordeOn = false;
    public AudioSource mainAudio;                 
    public AudioSource gameAudio;
    public static AudioMain instance = null;
    bool isPlaying = false;
    void Awake()
    {
      
        if (instance == null)
        instance = this;
        else if (instance != this)
        Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(this);
        mainAudio.loop = true; 



    }
    // Use this for initialization
    void Start () {
        mainAudio.volume = 0.4f;
        mainAudio.Play();
        mainAudio.loop = true;
        gameAudio.volume = 0.3f;
        gameAudio.Play();
        gameAudio.Pause();
        gameAudio.loop = true;
	}

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string currentSceneName = currentScene.name;
        if (currentSceneName != "HomeScreen"){
           
            isPlaying = true;
        }
        else
        {
            isPlaying = false;
            mainAudio.UnPause();
        }
        if(isPlaying){
            if (mainAudio.volume > 0.05)
            {
                mainAudio.volume -= Time.deltaTime * 0.1f;
            }
            else{
                mainAudio.volume = 0.0f;
                mainAudio.Pause();

            }
            gameAudio.UnPause();
        }
        else{
            if(mainAudio.volume < 0.4){
                mainAudio.volume += Time.deltaTime * 0.1f;
            }
            gameAudio.Pause();
        }
       
    }

    public void turnDownGameMusic(){
        gameAudio.volume = 0.05f;
    }

    public void turnUpGameMusic()
    {
        gameAudio.volume = 0.4f;
    }



}
