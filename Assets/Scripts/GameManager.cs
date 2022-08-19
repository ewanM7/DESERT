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

    public Player _Player;
    public PlayerMovement _PlayerMovement;

    public CameraManager _CameraManager;

    public MainUI _MainUI;

    public PlayerSave playerSave;

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

        _CameraManager.FindCameras();

        Day = 1;
        WeekDay = Weekday.Monday;
        TimeOfDay = 0f;

        StartCoroutine(TimeOfDayCoroutine());
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _PlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        _MainUI = GameObject.FindGameObjectWithTag("MainUI").GetComponent<MainUI>();
    }

    private IEnumerator TimeOfDayCoroutine()
    {
        while(TimeOfDay < 100f)
        {
            TimeOfDay += Time.deltaTime * DayTimeScale;
            yield return null;
        }
    }

    public void IncrementDay()
    {
        WeekDay = WeekDay.NextDay();
        Day++;
        TimeOfDay = 0f;
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
        _PlayerMovement.MovementInput = value.Get<Vector2>();
    }

    public void OnPlayerInteract()
    {
        _Player.OnPlayerInteract();
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

    public static Weekday NextDay(this Weekday day)
    {
        if(day == Weekday.Sunday)
        {
            return Weekday.Monday;
        }
        else
        {
            return day + 1;
        }
    }
}