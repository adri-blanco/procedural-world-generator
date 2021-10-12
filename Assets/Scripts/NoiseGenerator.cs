using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseGenerator
{
  public static float[,] Generate(int mapWidth, int mapHeight, float scale, int octaves, float persistance, float lacunarity)
  {
    float[,] noiseMap = new float[mapWidth, mapHeight];

    if (scale <= 0)
    {
      scale = 0.00001f;
    }
    if (mapWidth <= 0)
    {
      mapWidth = 1;
    }
    if (mapHeight <= 0)
    {
      mapHeight = 1;
    }

    float minNoiseHeight = float.MaxValue;
    float maxNoiseHeight = float.MinValue;

    for (int x = 0; x < mapWidth; x++)
    {
      for (int y = 0; y < mapHeight; y++)
      {
        float amplitude = 1;
        float frequency = 1;
        float noiseHeight = 0;
        for (int octave = 0; octave < octaves; octave++)
        {
          // Multiply the frequency to make the samples more distant, so more "random"
          float sampleX = x / scale * frequency;
          float sampleY = y / scale * frequency;
          // * 2 - 1 to change the range [0,1] a [-1, 1]
          float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
          noiseHeight += perlinValue * amplitude;

          amplitude *= persistance;
          frequency *= lacunarity;
        }

        if (noiseHeight < minNoiseHeight)
        {
          minNoiseHeight = noiseHeight;
        }
        else if (noiseHeight > maxNoiseHeight)
        {
          maxNoiseHeight = noiseHeight;
        }

        noiseMap[x, y] = noiseHeight;
      }
    }

    for (int x = 0; x < mapWidth; x++)
    {
      for (int y = 0; y < mapHeight; y++)
      {
        noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
      }
    }
    return noiseMap;
  }
}
