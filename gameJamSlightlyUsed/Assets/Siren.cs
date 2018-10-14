using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siren : MonoBehaviour {
    [SerializeField]
    public Light lt;
    [SerializeField]
    public bool SirenOn = false;
    private Color sirenColor = Color.white;
	// Update is called once per frame
	void FixedUpdate () {
        if(SirenOn == true){

            lt.color = Color.Lerp( new Vector4(139f/255f,0.0f,0.0f,1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f), Mathf.PingPong(Time.time * 2, 1));
        }
        else{
            lt.color = Color.white;
        }

	}
}
