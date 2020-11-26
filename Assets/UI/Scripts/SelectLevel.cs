using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    public int levelNumber;
    public int levelSeed;
    public int levelLength;

    public void eneterLevel()
    {
        // FIXME
        MapGen.seed = levelSeed;
        MapGen.mapLength = levelLength;
        SceneManager.LoadScene("Level");

    }

}
