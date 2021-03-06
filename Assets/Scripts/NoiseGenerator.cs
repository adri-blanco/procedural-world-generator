using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseGenerator
{
  public static float[,] Generate(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
  {
    float[,] noiseMap = new float[mapWidth, mapHeight];

    float halfWidth = mapWidth / 2;
    float halfHeight = mapHeight / 2;

    System.Random rnd = new System.Random(seed);
    Vector2[] octaveOffsets = new Vector2[octaves];
    for (int i = 0; i < octaves; i++)
    {
      float offsetX = rnd.Next(-100000, 100000) + offset.x;
      float offsetY = rnd.Next(-100000, 100000) + offset.y;
      octaveOffsets[i] = new Vector2(offsetX, offsetY);
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
          float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[octave].x;
          float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[octave].y;
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
