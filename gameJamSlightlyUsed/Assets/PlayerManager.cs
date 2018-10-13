using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager Instance;
    public List<Material> PlayerMaterials;
    public List<EndpointController> Endpoints;
    private void Awake()
    {
        Instance = this;
        Endpoints = FindObjectsOfType<EndpointController>().ToList();
    }

    public EndpointController GetEndpoint()
    {
        var endpoint = Endpoints[0];
        Endpoints.Remove(endpoint);
        return endpoint;
    }

    public Material GetPlayerMaterial()
    {
        var mat = PlayerMaterials[0];
        PlayerMaterials.Remove(mat);
        return mat;
    }
}
