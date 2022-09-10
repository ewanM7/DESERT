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

    public bool PlayerCameraMovementEnabled;

    public void FindCameras()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        PlayerCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineFreeLook>();
        TradingCamera = GameObject.FindGameObjectWithTag("TradingCamera").GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {

    }

    public void SetPlayerCameraMovementEnabled(bool enabled)
    {
        PlayerCameraMovementEnabled = enabled;

        if(enabled)
        {
            PlayerCamera.m_YAxis.m_MaxSpeed = 0.005f;
            PlayerCamera.m_XAxis.m_MaxSpeed = 0.5f;
        }
        else
        {
            PlayerCamera.m_YAxis.m_MaxSpeed = 0f;
            PlayerCamera.m_XAxis.m_MaxSpeed = 0f;
        }
    }

    public void SwitchToTradingCam()
    {
        SetPlayerCameraMovementEnabled(false);
        PlayerCamera.Priority = 1;
        TradingCamera.Priority = 10;

    }

    public void SwitchToPlayerCam()
    {
        PlayerCamera.Priority = 10;
        TradingCamera.Priority = 1;

        StartCoroutine(WaitThenEnableCameraControls());
    }

    private IEnumerator WaitThenEnableCameraControls()
    {
        yield return new WaitForSeconds(2f);

        SetPlayerCameraMovementEnabled(true);
    }
}
