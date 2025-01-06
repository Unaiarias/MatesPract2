using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GenerarTerreno : MonoBehaviour
{
    public int depth = 20;

    public int width = 256;
    public int height = 256;

    public float offsetX = 200f;
    public float offsetY = 200f;

    public float scale = 20f;

    public float velocity = 5f;
    void Start()
    {
        offsetX = Random.Range (0, 99999f);
        offsetY = Random.Range(0, 99999f);
    }

    
    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        offsetX += velocity * Time.deltaTime;
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;

        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());

        return terrainData;
    }

    float [,]GenerateHeights()
    {
        float[,]heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x,y);
            }
        }
        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }

}
