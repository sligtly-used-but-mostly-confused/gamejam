using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlatformColor : MonoBehaviour {
    Color cl = Color.white;
    private static float _minY, _maxY;

    void Start()
    {
        //Fetch the Renderer from the GameObject
        Renderer rend = GetComponent<Renderer>();
        Transform tr = GetComponent<Transform>();


        _minY = FindObjectsOfType<PlatformColor>().Min(x => x.transform.position.y);
        _maxY = FindObjectsOfType<PlatformColor>().Max(x => x.transform.position.y);

        FindObjectsOfType<PlatformColor>().ToList().ForEach(x => x.UpdateColor());

        //Set the main Color of the Material to green
        rend.material.shader = Shader.Find("_Color");
        
        //Find the Specular shader and change its Color to red
        rend.material.shader = Shader.Find("Specular");
        
    }

    public void UpdateColor()
    {
        Renderer rend = GetComponent<Renderer>();
        Transform tr = GetComponent<Transform>();

        var deltaFromMin = tr.position.y - _minY;
        var delta = _maxY - _minY;
        var percent = deltaFromMin / delta;
        rend.material.SetColor("_Color", Color.HSVToRGB(1.0f, 1.0f, percent));
        rend.material.SetColor("_SpecColor", Color.white);
    }

}
