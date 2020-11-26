using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) DoExit();
    }

    public void DoExit()
    {
        SceneManager.LoadScene("Start");
    }
}
