using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public Camera MainCamera;
    public CinemachineFreeLook PlayerCamera;
    public CinemachineVirtualCamera TradingCamera;
    public void FindCameras()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        PlayerCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineFreeLook>();
    }

    public void OnCameraMove(InputValue value)
    {
        PlayerCamera.m_XAxis.Value += value.Get<Vector2>().x * Time.deltaTime * 10f;
        PlayerCamera.m_YAxis.Value -= value.Get<Vector2>().y * Time.deltaTime / 10f;
    }

    public void SwitchToTradingCam()
    {
        PlayerCamera.Priority = 1;
        TradingCamera.Priority = 10;
    }
}
