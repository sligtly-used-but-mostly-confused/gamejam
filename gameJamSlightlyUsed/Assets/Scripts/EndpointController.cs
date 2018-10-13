using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndpointController : MonoBehaviour {

    public PlayerController Owner;

    [SerializeField]
    private GameObject WinGameUI;

    private static bool _hasWon = false;

    private void Awake()
    {
        _hasWon = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() && other.GetComponent<PlayerController>() == Owner && !_hasWon)
        {
            _hasWon = true;
            var ui = Instantiate(WinGameUI);
            ui.transform.SetParent(CanvasController.Instance.transform, false);
        }
    }
}
