using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGen : MonoBehaviour
{
    const int TERRAIN_HEIGHT = 20;
    const int WATER_HEIGHT = 15;
    const int JUMP_HEIGHT = 3;
    const int PLATFORM_MAX_WIDTH = 10;
    const int PLATFORM_MAX_SPACE = 3;
    const int BIRD_DISTANTION = 15;

    const int SAVE_LENGTH = 50;
    static public int mapLength = 256;
    static public int seed = 311931;

    public TileBase groundTile;
    public TileBase waterTile;
    public TileBase platformTile;
    public Tilemap tilemap;
    public Tilemap waterTilemap;
    public Transform endFlag;
    public GameObject bird;
    private void Start()
    {
        Random.InitState(seed);

        Result.Reset();

        int platformWidth = 0;
        int platformHeight = 0;
        bool isFirstPlatform = true;
        bool isSpace = false;

        int birdPosition = SAVE_LENGTH + BIRD_DISTANTION;

        bool flagInstalled = false;

        float noise = 0f;
        int x = 0;
        while (noise * TERRAIN_HEIGHT < WATER_HEIGHT + 2)
        {
            noise = Mathf.PerlinNoise((x / (float)mapLength) * 8f, seed * 0.000001f);
            x++;
        }

        for (int i = 0; i < mapLength + 2 * SAVE_LENGTH; i++)
        {
            noise = Mathf.PerlinNoise(((i + (x - SAVE_LENGTH)) / (float)mapLength) * 8f, seed * 0.000001f);
            int height = Mathf.FloorToInt(noise * TERRAIN_HEIGHT);

            // Поверхность
            for (int j = 0; j < TERRAIN_HEIGHT; j++)
            {
                // Земля
                if (j <= height)
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), groundTile);
                }
                // Вода
                else if (j < WATER_HEIGHT)
                {
                    waterTilemap.SetTile(new Vector3Int(i, j, 0), waterTile);
                }
            }

            // Если плафторма закончилась делаем пробел
            if (platformWidth == 0)
            {
                isSpace = !isSpace;
            }

            // Если платформа не закончилась, то рисуем блоки
            if (platformWidth > 0)
            {
                if (!isSpace)
                {
                    tilemap.SetTile(new Vector3Int(i, platformHeight, 0), platformTile);
                }
                platformWidth--;
            }
            // Если начинается новая платформа, то мы гененриум высоту и длинну
            else if (height < WATER_HEIGHT - 1 && i > SAVE_LENGTH + 3)
            {
                if (isFirstPlatform)
                {
                    isFirstPlatform = false;
                    isSpace = false;
                    platformHeight = 0;
                }

                platformHeight = Mathf.FloorToInt(Random.Range(WATER_HEIGHT + JUMP_HEIGHT - 2, platformHeight < WATER_HEIGHT ? WATER_HEIGHT + JUMP_HEIGHT : platformHeight + JUMP_HEIGHT));
                platformWidth = Mathf.FloorToInt(Random.Range(2, isSpace ? PLATFORM_MAX_SPACE : PLATFORM_MAX_WIDTH));

                if (i - birdPosition > BIRD_DISTANTION)
                {
                    float birdRandom = Random.Range(0f, 10f);
                    if (birdRandom < 1f)
                    {
                        birdPosition = i + Mathf.FloorToInt(birdRandom * (platformWidth - 1));
                    }
                }
            }
            // Если начинаетьбся вода
            else if (height <= WATER_HEIGHT - 1)
            {
                isFirstPlatform = true;
            }

            // Птички
            if (i == birdPosition)
            {
                Instantiate(bird, new Vector3(i, platformHeight + JUMP_HEIGHT - 1, 0), Quaternion.identity, transform);
            }

            // Установка флага
            if (!flagInstalled && i > mapLength + SAVE_LENGTH && (height > WATER_HEIGHT || (!isSpace && platformWidth < 4)))
            {
                endFlag.position = new Vector3(i + 1f, isSpace ? height : platformHeight + 1f, 0f);
                flagInstalled = true;
            }

        }
    }

}