using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
  public Renderer map;

  public void Draw(float[,] noiseMap)
  {
    int width = noiseMap.GetLength(0);
    int height = noiseMap.GetLength(1);

    Texture2D texture = new Texture2D(width, height);

    Color[] colorMap = new Color[width * height];
    for (int y = 0; y < height; y++)
    {
      for (int x = 0; x < width; x++)
      {
        colorMap[y * width + x] = new Color(noiseMap[x, y], noiseMap[x, y], noiseMap[x, y]);
      }
    }

    texture.SetPixels(colorMap);
    texture.Apply();

    map.transform.localScale = new Vector3(width, height, 1);
    map.material.mainTexture = texture;
  }
}
