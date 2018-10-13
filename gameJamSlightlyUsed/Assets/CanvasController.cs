using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {

    public static CanvasController Instance;

    private void Awake()
    {
        Instance = this;
    }
}
