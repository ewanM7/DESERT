using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerSave
{
    public int Day;
    public Weekday WeekDay;
    public float TimeOfDay;

    public PlayerSave()
    {
        Day = 1;
        WeekDay = Weekday.Monday;
        TimeOfDay = 0f;
    }
}
