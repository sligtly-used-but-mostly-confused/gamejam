using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

    public void Move(Vector3 dir)
    {
        transform.position += dir;
    }
}
