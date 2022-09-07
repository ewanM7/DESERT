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

    private Vector2 CurrentMouseDelta;
    private Vector2 NewMouseDelta;

    public void FindCameras()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        PlayerCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineFreeLook>();
        TradingCamera = GameObject.FindGameObjectWithTag("TradingCamera").GetComponent<CinemachineVirtualCamera>();
    }

    public void OnCameraMove(InputValue value)
    {
        if (PlayerCameraMovementEnabled)
        {
            NewMouseDelta = value.Get<Vector2>();

            PlayerCamera.m_XAxis.Value += Mathf.Clamp(CurrentMouseDelta.x * Time.deltaTime * 8f, -5f, 5f);
            PlayerCamera.m_YAxis.Value -= Mathf.Clamp(CurrentMouseDelta.y * Time.deltaTime / 8f, -5f, 5f);
        }
    }

    private void Update()
    {
        //smooth the mouse delta every frame, so that the camera rotation is also smoothed slightly
        if (PlayerCameraMovementEnabled)
        {
            CurrentMouseDelta = Vector2.Lerp(CurrentMouseDelta, NewMouseDelta, Time.deltaTime * 20f);
        }
    }

    public void SwitchToTradingCam()
    {
        PlayerCameraMovementEnabled = false;
        PlayerCamera.Priority = 1;
        TradingCamera.Priority = 10;

    }

    public void SwitchToPlayerCam()
    {
        PlayerCamera.Priority = 10;
        TradingCamera.Priority = 1;

        //need a coroutine to wait for the blend to finish before re-enabling player camera movement controls
        //PlayerCameraMovementEnabled = false;
    }
}
