// using System;
// using System.Collections.Generic;
// using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGen : MonoBehaviour
{
    const int TERRAIN_HEIGHT = 20;
    const int WATER_HEIGHT = 15;
    const int JUMP_HEIGHT = 4;
    const int PLATFORM_MAX_WIDTH = 10;
    const int PLATFORM_MAX_SPACE = 3;

    const int SAVE_LENGTH = 50;

    static public int mapLength = 200;

    static public int seed = 543543456;

    public TileBase groundTile;
    public TileBase waterTile;
    public TileBase platformTile;
    public Tilemap tilemap;
    public Tilemap waterTilemap;
    public Transform endFlag;
    private void Start()
    {
        Random.InitState(seed);

        int platformWidth = 0;
        int platformHeight = 0;
        bool isFirstPlatform = true;
        bool isSpace = false;

        float noise = 0f;
        int x = 0;
        while (noise < 0.7f)
        {
            noise = Mathf.PerlinNoise((x / (float)mapLength) * 8f, seed * 0.000001f);
            x++;
        }

        for (int i = 0; i < mapLength + 2 * SAVE_LENGTH; i++)
        {
            noise = Mathf.PerlinNoise(((i + x + SAVE_LENGTH) / (float)mapLength) * 8f, seed * 0.000001f);
            int height = Mathf.FloorToInt(noise * TERRAIN_HEIGHT);

            for (int j = 0; j < TERRAIN_HEIGHT; j++)
            {

                if (j <= height)
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), groundTile);
                }
                else if (j < WATER_HEIGHT)
                {
                    waterTilemap.SetTile(new Vector3Int(i, j, 0), waterTile);
                }
            }

            if (platformWidth == 0)
            {
                isSpace = !isSpace;
            }

            if (platformWidth > 0)
            {
                if (!isSpace)
                {
                    tilemap.SetTile(new Vector3Int(i, platformHeight, 0), platformTile);
                }
                platformWidth--;
            }
            else if (height < WATER_HEIGHT - 1)
            {
                if (isFirstPlatform)
                {
                    isFirstPlatform = false;
                    isSpace = false;
                    platformHeight = 0;
                }

                platformHeight = Mathf.FloorToInt(Random.Range(WATER_HEIGHT + JUMP_HEIGHT - 2, platformHeight < WATER_HEIGHT ? WATER_HEIGHT + JUMP_HEIGHT : platformHeight + JUMP_HEIGHT));
                platformWidth = Mathf.FloorToInt(Random.Range(2, isSpace ? PLATFORM_MAX_SPACE : PLATFORM_MAX_WIDTH));
            }
            else if (height <= WATER_HEIGHT - 1)
            {
                isFirstPlatform = true;
            }

            if (i == mapLength + SAVE_LENGTH)
            {
                endFlag.position = new Vector3(i - 0.5f, isSpace ? height : platformHeight - 0.5f, 0f);
            }

        }
    }

}