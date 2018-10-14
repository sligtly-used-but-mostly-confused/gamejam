using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlatformColor : MonoBehaviour {
    Color cl = Color.white;
    private static float _minY, _maxY;
    private static List<PlatformColor> Platforms = new List<PlatformColor>();
    private void Awake()
    {
        Platforms.Add(this);
    }
    private void OnDestroy()
    {
        Platforms.Remove(this);
    }

    void Start()
    {
        //Fetch the Renderer from the GameObject
        Renderer rend = GetComponent<Renderer>();
        Transform tr = GetComponent<Transform>();

        _minY = Platforms.Min(x => x.transform.position.y);
        _maxY = Platforms.Max(x => x.transform.position.y);
        //Platforms.ForEach(x => x.UpdateColor());
        
        //Set the main Color of the Material to green
        //rend.material.shader = Shader.Find("_Color");
        
        //Find the Specular shader and change its Color to red
        //rend.material.shader = Shader.Find("Specular");
        
    }

    public static void UpdateAllColors()
    {
        foreach (var platformColor in Platforms)
        {
            platformColor.UpdateColor();
        }
    }

    public void UpdateColor()
    {
        Renderer rend = GetComponent<Renderer>();
        Transform tr = GetComponent<Transform>();

        var deltaFromMin = tr.position.y - _minY;
        var delta = _maxY - _minY;
        var percent = Mathf.Clamp(deltaFromMin / delta, .2f, 1f);
        //GetComponent<Renderer>().material.color = Color.HSVToRGB(1.0f, 1.0f, percent);
        GetComponent<Renderer>().material.SetColor("_Color", Color.HSVToRGB(1.0f, 1.0f, percent));
    }

}
