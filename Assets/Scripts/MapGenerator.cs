using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
  public int width = 256;
  public int height = 256;
  public float scale = 8.0f;

  public void Generate()
  {
    float[,] map = NoiseGenerator.Generate(width, height, scale);

    MapDisplay mapDisplay = FindObjectOfType<MapDisplay>();
    mapDisplay.Draw(map);
  }
}
