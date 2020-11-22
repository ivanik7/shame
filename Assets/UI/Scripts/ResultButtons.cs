using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButtons : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene("Start");
    }
    public void Repeat()
    {
        SceneManager.LoadScene("Level");
    }
}
