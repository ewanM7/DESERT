using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainUI : MonoBehaviour
{
    public TextMeshProUGUI DayCountText;
    public TextMeshProUGUI WeekdayText;
    public TextMeshProUGUI TimeOfDayText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        TimeOfDayText.text = GameManager.Instance.TimeOfDay.ToString("F1");
    }
}
