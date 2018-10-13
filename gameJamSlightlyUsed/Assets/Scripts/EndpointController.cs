using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndpointController : MonoBehaviour {

    public PlayerController Owner;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() && other.GetComponent<PlayerController>() == Owner)
        {
            Debug.Log("winner");
        }
    }
}
