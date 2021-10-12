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

  public void Generate()
  {
    float[,] map = NoiseGenerator.Generate(width, height, scale, octaves, persistance, lacunarity);

    MapDisplay mapDisplay = FindObjectOfType<MapDisplay>();
    mapDisplay.Draw(map);
  }
}
