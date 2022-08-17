using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    public ScreenResolution screenResolution;
    public bool vSyncOn;
    public QualityLevel qualityLevel;

    public float masterVol;
    public float musicVol;

    public Language language;

    public Settings()
    {
        screenResolution = ScreenResolution._1920x1080;
        vSyncOn = false;
        qualityLevel = QualityLevel.high;

        masterVol = 1f;
        musicVol = 1f;

        language = Language.English;
    }
}

public enum ScreenResolution
{
    _1920x1080,
    _1600x900,
    _1440x900,
    _1366x768,
    _1280x720,
}

public enum Language
{
    English,

}

public static class ScreenResolutionExtensions
{
    public static Vector2Int enumToWH(this ScreenResolution screenRes)
    {
        switch (screenRes)
        {
            case ScreenResolution._1920x1080:
                return new Vector2Int(1920, 1080);

            case ScreenResolution._1600x900:
                return new Vector2Int(1600, 900);

            case ScreenResolution._1440x900:
                return new Vector2Int(1440, 900);

            case ScreenResolution._1366x768:
                return new Vector2Int(1366, 768);

            case ScreenResolution._1280x720:
                return new Vector2Int(1280, 720);

            default:
                return new Vector2Int(-1, -1);
        }
    }
}

public enum QualityLevel
{
    low,
    medium,
    high
}