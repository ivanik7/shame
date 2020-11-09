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

    public int mapLength = 200;

    private int seed = 543543456;
    public TileBase groundTile;
    public TileBase waterTile;
    public TileBase platformTile;
    private Tilemap tilemap;
    private Tilemap waterTilemap;
    private void Start()
    {
        // FIXME
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            GameObject child = transform.GetChild(0).GetChild(i).gameObject;
            if (child.name == "Tilemap") tilemap = child.GetComponent<Tilemap>();
            else if (child.name == "Water") waterTilemap = child.GetComponent<Tilemap>();
        }

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

        for (int i = 0; i < mapLength; i++)
        {
            noise = Mathf.PerlinNoise(((i + x) / (float)mapLength) * 8f, seed * 0.000001f);
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
        }
    }

}