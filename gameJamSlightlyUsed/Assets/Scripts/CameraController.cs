using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cinemachine;

public class CameraController : MonoBehaviour {

    public static CameraController Instance;

    public Vector3 Center = Vector3.zero;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
    }

    public void Rotate(float changeCameraDir)
    {
        CinemachineVirtualCamera vcam = GetComponent<CinemachineVirtualCamera>();
        //Debug.Log(vcam.GetCinemachineComponent<CinemachineTrackedDolly>());
        vcam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition += changeCameraDir * .025f;
    //var composer = vcam.GetCinemachineComponent<CinemachineComposer>();
    //Camera.main.transform.RotateAround(Center, Vector3.up, changeCameraDir);
    }
}
