using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public AudioManager _AudioManager;

    public SettingsManager _SettingsManager;

    public PlayerMovement _PlayerMovement;

    public CinemachineFreeLook PlayerCamera;
    public Camera MainCam;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //this is a singleton, so never destroy it
        DontDestroyOnLoad(gameObject);

        _SettingsManager = new SettingsManager();

        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;

        Application.targetFrameRate = 60;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _PlayerMovement.CameraForward = MainCam.transform.forward;
        _PlayerMovement.CameraRight = MainCam.transform.right;
    }

    // --------------------- INPUT MANAGEMENT ------------------------------ //


    void OnPlayerMove(InputValue value)
    {
        Debug.Log("movement: " + value.Get<Vector2>());
        _PlayerMovement.InputMovement = value.Get<Vector2>();
        
    }

    void OnCameraMove(InputValue value)
    {
        PlayerCamera.m_XAxis.Value += value.Get<Vector2>().x * Time.deltaTime * 10f;
        PlayerCamera.m_YAxis.Value -= value.Get<Vector2>().y * Time.deltaTime / 10f;
    }
}
