using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraController : MonoBehaviour {

    public static CameraController Instance;

    public Vector3 Center = Vector3.zero;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        var players = FindObjectsOfType<PlayerController>().ToList();

        float minX = players.Min(x => x.transform.position.x);
        float maxX = players.Max(x => x.transform.position.x);
        float deltaX = maxX - minX;

        float minY = players.Min(x => x.transform.position.y);
        float maxY = players.Max(x => x.transform.position.y);
        float deltaY = maxY - minY;

        float minZ = players.Min(x => x.transform.position.z);
        float maxZ = players.Max(x => x.transform.position.z);
        float deltaZ = maxZ - minZ;

        var currentOffset = transform.position - Center;


        Center = new Vector3(minX + deltaX / 2, minY + maxY / 2, minZ + maxZ / 2);

        transform.position = currentOffset + Center;
    }

    public void Rotate(float changeCameraDir)
    {
        Camera.main.transform.RotateAround(Center, Vector3.up, changeCameraDir);
    }
}
