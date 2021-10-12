using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseGenerator
{
  public static float[,] Generate(int mapWidth, int mapHeight, float scale)
  {
    float[,] noiseMap = new float[mapWidth, mapHeight];

    if (scale <= 0)
    {
      scale = 0.00001f;
    }

    for (int x = 0; x < mapWidth; x++)
    {
      for (int y = 0; y < mapHeight; y++)
      {
        float sampleX = x / scale;
        float sampleY = y / scale;

        float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
        noiseMap[x, y] = perlinValue;
      }
    }

    return noiseMap;
  }
}
