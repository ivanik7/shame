using System;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    void Start()
    {
        Text text = GetComponent<Text>();
        text.text = $"Время: {(DateTime.Now - Stats.startTime).TotalSeconds.ToString("F")}\nСмертей: {Stats.dies}\nПтиц: {Stats.birds}\nУрон: {Stats.damage}";
    }
}
