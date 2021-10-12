using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
  public int width = 256;
  public int height = 256;
  public float scale = 8.0f;
  public int octaves = 3;
  [Range(0.0f, 1.0f)]
  public float persistance = 0.5f;
  public float lacunarity = 2.0f;
  public int seed = 1;
  public Vector2 offset;

  public void Generate()
  {
    float[,] map = NoiseGenerator.Generate(width, height, seed, scale, octaves, persistance, lacunarity, offset);

    MapDisplay mapDisplay = FindObjectOfType<MapDisplay>();
    mapDisplay.Draw(map);
  }

  void OnValidate()
  {
    if (width < 1)
    {
      width = 1;
    }
    if (height < 1)
    {
      height = 1;
    }
    if (scale <= 0)
    {
      scale = 0.001f;
    }
    if (lacunarity < 1)
    {
      lacunarity = 1;
    }
    if (octaves < 1)
    {
      octaves = 1;
    }
  }
}
