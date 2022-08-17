using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Handles settings changes and saving / loading them
/// </summary>
public class SettingsManager
{
    public Settings settings;

    public SettingsManager()
    {
        loadSettings();
    }

    /// <summary>
    /// Applies resolution and vSync settings
    /// </summary>
    public void applyVisualSettings()
    {
        Vector2Int resWH = settings.screenResolution.enumToWH();
        Screen.SetResolution(resWH.x, resWH.y, true);

        if (settings.vSyncOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        QualitySettings.SetQualityLevel((int)settings.qualityLevel);
    }

    /// <summary>
    /// Applies audio volume settings
    /// </summary>
    public void applyAudioSettings()
    {
        //GameManager.Instance.audioManager.setMasterVol(settings.masterVol);
        //GameManager.Instance.audioManager.setMusicVol(settings.musicVol, settings.masterVol);
    }

    /// <summary>
    /// Save settings to file
    /// </summary>
    /// <returns></returns>
    public bool saveSettings()
    {
        string settingsAsJson = JsonUtility.ToJson(settings);
        Debug.Log("Saving player settings...");
        try
        {
            File.WriteAllText(Application.persistentDataPath + "/settings.desert", settingsAsJson);
            Debug.Log("Save successful");
            return true;
        }
        catch (IOException)
        {
            Debug.Log("Settings failed to save");
            return false;
        }

    }

    /// <summary>
    /// Load settings from file
    /// </summary>
    /// <returns></returns>
    public bool loadSettings()
    {
        string settingsAsJson;
        Debug.Log("Loading player settings...");
        try
        {
            settingsAsJson = File.ReadAllText(Application.persistentDataPath + "/settings.desert");
            settings = JsonUtility.FromJson<Settings>(settingsAsJson);
            Debug.Log("Load successful");
            return true;
        }
        catch (IOException)
        {
            Debug.Log("Settings failed to load. Creating new default settings...");
            settings = new Settings();
            return saveSettings();
        }

    }
}
