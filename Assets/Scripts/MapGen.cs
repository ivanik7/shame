using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGen : MonoBehaviour
{
    const int TERRAIN_HEIGHT = 20;

    public int mapLength = 200;

    private float seed = 0.54323f;
    public TileBase groundTile;
    public TileBase waterTile;
    private Tilemap tilemap;
    private void Start()
    {
        tilemap = GetComponentInChildren<Tilemap>();

        float noise = 0f;
        int x = 0;
        while (noise < 0.7f)
        {
            noise = Mathf.PerlinNoise((x / (float)mapLength) * 8f, seed);
            x++;
        }

        for (int i = 0; i < mapLength; i++)
        {
            noise = Mathf.PerlinNoise(((i + x) / (float)mapLength) * 8f, seed);
            int height = Mathf.FloorToInt(noise * TERRAIN_HEIGHT);

            for (int j = 0; j < TERRAIN_HEIGHT; j++)
            {

                if (j <= height)
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), groundTile);
                }
                else if (j < 15)
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), waterTile);

                }
            }
        }
    }

}