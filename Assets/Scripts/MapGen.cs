// using System;
// using System.Collections.Generic;
// using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGen : MonoBehaviour
{
    const int TERRAIN_HEIGHT = 20;
    const int WATER_HEIGHT = 15;
    const int PLATFORM_MAX_WIDTH = 10;
    const int PLATFORM_MAX_SPACE = 3;

    public int mapLength = 200;

    private int seed = 543543456;
    public TileBase groundTile;
    public TileBase waterTile;
    public TileBase platformTile;
    private Tilemap tilemap;
    private void Start()
    {
        tilemap = GetComponentInChildren<Tilemap>();

        Random.InitState(seed);

        int platformWidth = 0;
        int platformHeight = 0;
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
                    tilemap.SetTile(new Vector3Int(i, j, 0), waterTile);
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

                tilemap.SetTile(new Vector3Int(i, 0, 0), platformTile);

                platformHeight = Mathf.FloorToInt(Random.Range(WATER_HEIGHT + 3, platformHeight < WATER_HEIGHT ? WATER_HEIGHT + 5 : platformHeight + 6));
                platformWidth = Mathf.FloorToInt(Random.Range(2, isSpace ? PLATFORM_MAX_SPACE : PLATFORM_MAX_WIDTH));
            }
        }
    }

}