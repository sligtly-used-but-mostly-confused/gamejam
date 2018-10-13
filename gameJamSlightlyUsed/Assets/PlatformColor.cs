using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformColor : MonoBehaviour {
    Color cl = Color.white;
    void Start()
    {
        //Fetch the Renderer from the GameObject
        Renderer rend = GetComponent<Renderer>();
        Transform tr = GetComponent<Transform>();


        //Set the main Color of the Material to green
        rend.material.shader = Shader.Find("_Color");
        rend.material.SetColor("_Color", Color.HSVToRGB(1.0f,1.0f, tr.position.y/2.0f));
        //Find the Specular shader and change its Color to red
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_SpecColor", Color.white);
    }


}
