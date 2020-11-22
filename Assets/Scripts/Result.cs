using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    public static DateTime startTime = DateTime.Now;
    public static int damage = 0;
    public static int birds = 0;
    public static bool isDie = false;

    public static void Fail()
    {
        isDie = true;
        SceneManager.LoadScene("Result");
    }

    public static void Psss()
    {
        isDie = false;
        SceneManager.LoadScene("Result");
    }
}
