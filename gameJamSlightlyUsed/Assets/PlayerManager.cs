using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager Instance;
    public List<Material> PlayerMaterials;
    public List<EndpointController> Endpoints;

    Dictionary<InputDevice, Material> playerColorMap = new Dictionary<InputDevice, Material>();

    private void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadNewScene();
    }

    public void LoadNewScene()
    {
        Endpoints = FindObjectsOfType<EndpointController>().ToList();
    }

    public EndpointController GetEndpoint()
    {
        var endpoint = Endpoints[0];
        Endpoints.Remove(endpoint);
        return endpoint;
    }

    public Material GetPlayerMaterial(InputDevice input)
    {
        if(playerColorMap.ContainsKey(input))
        {
            return playerColorMap[input];
        }

        var mat = PlayerMaterials[0];
        PlayerMaterials.Remove(mat);
        playerColorMap.Add(input, mat);

        return mat;
    }
}
