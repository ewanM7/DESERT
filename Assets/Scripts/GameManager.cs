using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.IO;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public AudioManager _AudioManager;

    public SettingsManager _SettingsManager;

    public PlayerMovement _PlayerMovement;

    public PlayerSave playerSave;

    public CinemachineFreeLook PlayerCamera;
    public Camera MainCam;

    public ItemDatabase _ItemDatabase;

    public Weekday WeekDay;
    public int Day;

    //value to multiply Timed.deltaTime by - should be 100f / length of the day in real time seconds (eg. a day lasts 15 minutes of real time = 900 seconds)
    public const float DayTimeScale = 100f / 900f;
    //time of day goes from 0 to 100
    public float TimeOfDay;

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

    private IEnumerator DayTimeCoroutine()
    {
        while(TimeOfDay < 100f)
        {
            TimeOfDay += Time.deltaTime * DayTimeScale;
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        _PlayerMovement.CameraForward = MainCam.transform.forward;
        _PlayerMovement.CameraRight = MainCam.transform.right;
    }

    /// <summary>
    /// Saves player data to file
    /// </summary>
    public void saveGame()
    {
        Debug.Log("Saving game...");

        string saveAsJson = JsonUtility.ToJson(playerSave);

        try
        {
            File.WriteAllText(Application.persistentDataPath + "/playerSave.desert", saveAsJson);

            Debug.Log("Save game successful");
        }
        catch (IOException)
        {
            Debug.Log("Failed to save game");
        }
    }

    /// <summary>
    /// Loads player data from file
    /// </summary>
    /// <returns></returns>
    public bool loadGame()
    {
        string saveAsJson;
        Debug.Log("Loading player save...");

        try
        {
            saveAsJson = File.ReadAllText(Application.persistentDataPath + "/playerSave.desert");

            playerSave = JsonUtility.FromJson<PlayerSave>(saveAsJson);


            Debug.Log("Load successful");

            //check should be here for achievements based on player data, incase they need to be awarded retroactively

            return true;
        }
        catch (IOException)
        {
            Debug.Log("Failed to load player save");
            return false;
        }
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

public enum Weekday
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday,
}

static class WeekdayExtensions
{
    public static bool IsTradingDay(this Weekday day)
    {
        switch (day)
        {
            case Weekday.Saturday:
            case Weekday.Sunday:
                return false;

            default:
                return true;
        }
    }
}