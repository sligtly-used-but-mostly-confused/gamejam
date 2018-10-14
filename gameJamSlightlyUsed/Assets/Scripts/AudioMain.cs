using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioMain : MonoBehaviour {
    public AudioSource mainAudio;                   //Drag a reference to the audio source which will play the sound effects.
    public AudioSource gameAudio;
    public static AudioMain instance = null;
    bool isPlaying = false;
    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
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
   
}
