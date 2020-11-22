using UnityEngine;
using UnityEngine.UI;

public class ResultTitle : MonoBehaviour
{
    public string failText;
    public string passText;
    void Start()
    {
        Text text = GetComponent<Text>();
        if (Result.isDie)
        {
            text.text = failText;
        }
        else
        {
            text.text = passText;
        }
    }
}
