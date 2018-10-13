using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ObjectController {
    InputDevice input;
    protected readonly Vector3Int[] _moveDirs = { new Vector3Int(1,0,0), new Vector3Int(-1, 0, 0), new Vector3Int(0, 0, 1), new Vector3Int(0, 0, -1) };

    [SerializeField]
    private float _moveSpeed = 5;

    void Update () {
        input = MappedInput.InputDevices[3];
        Vector3 leftStick = input.GetAxis2DCircleClamp(MappedAxis.Horizontal, MappedAxis.Vertical);

        Move(new Vector3(leftStick.x, 0, leftStick.y) * Time.deltaTime * _moveSpeed);


    }

    private static Vector3Int ClosestDirection(Vector3 v, Vector3Int[] compass)
    {
        var maxDot = -Mathf.Infinity;
        var ret = Vector3Int.zero;

        foreach (var dir in compass)
        {
            var t = Vector3.Dot(v, dir);
            if (t > maxDot)
            {
                ret = dir;
                maxDot = t;
            }
        }

        return ret;
    }
}
