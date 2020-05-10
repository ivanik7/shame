using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Sprite satFull;
    public Sprite satEmpty;

    private int lastSatiety;
    private Cat cat;
    private Image[] sat = new Image[3];

    private void Awake()
    {
        cat = FindObjectOfType<Cat>();

        for (int i = 0; i < sat.Length; i++)
        {
            sat[i] = transform.GetChild(i).gameObject.GetComponent<Image>();
        }
    }

    private void Update()
    {
        if(cat.satiety != lastSatiety)
        {
            lastSatiety = cat.satiety;
            for(int i = 0; i < sat.Length; i++)
            {
                if (i < lastSatiety)
                {
                    sat[i].sprite = satFull;
                }
                else
                {
                    sat[i].sprite = satEmpty;
                }
            }
        }
    }
}
