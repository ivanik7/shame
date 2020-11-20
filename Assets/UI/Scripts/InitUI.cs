using UnityEngine;
using UnityEngine.UI;

public class InitUI : MonoBehaviour
{
    public GameObject levelButton;
    public TextAsset levelsList;

    void Start()
    {
        var levels = JsonUtility.FromJson<Levels>(levelsList.text).levels;

        foreach (var level in levels)
        {
            var button = Instantiate(levelButton, transform);
            Text text = button.GetComponentInChildren<Text>();
            text.text = $"{level.number}";
            SelectLevel selectLevel = button.GetComponentInChildren<SelectLevel>();
            selectLevel.levelNumber = level.number;
            selectLevel.levelSeed = level.seed;
            selectLevel.levelLength = level.length;
        }
    }

    [System.Serializable]
    public class Levels
    {
        public Level[] levels;
    }
    [System.Serializable]
    public class Level
    {
        public int number;
        public int seed;
        public int length;

    }
}