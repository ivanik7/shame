using System;
using UnityEngine;
using UnityEngine.UI;

public class ResultStats : MonoBehaviour
{
    public string timeText = "Время: ";
    public string damageText = "Урон: ";
    void Start()
    {
        Text text = GetComponent<Text>();
        text.text = $"{timeText}{(DateTime.Now - Result.startTime).TotalSeconds.ToString("F")}c\n{damageText}{Result.damage}";
    }

}
