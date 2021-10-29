using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
  public enum DrawMode { NoiseMap, ColorMap, Mesh, Voxel };
  public DrawMode drawMode;
  public int width = 256;
  public int height = 256;
  public float scale = 8.0f;
  public int octaves = 3;
  [Range(0.0f, 1.0f)]
  public float persistance = 0.5f;
  public float lacunarity = 2.0f;
  public int seed = 1;
  public Vector2 offset;
  public float heightMultiplier;
  public AnimationCurve meshHeightCurve;
  public int maxVoxelHeight = 10;

  public TerrainType[] regions;

  Texture2D GetColorsTexture2D(float[,] map)
  {
    Color[] colors = new Color[height * width];
    for (int y = 0; y < height; y++)
    {
      for (int x = 0; x < width; x++)
      {
        foreach (TerrainType region in regions)
        {
          if (map[x, y] <= region.height)
          {
            colors[y * width + x] = region.color;
            break;
          }
        }
      }
    }
    return TextureGenerator.TextureFromColorMap(colors, width, height);
  }

  public void Generate()
  {
    float[,] map = NoiseGenerator.Generate(width, height, seed, scale, octaves, persistance, lacunarity, offset);

    MapDisplay mapDisplay = FindObjectOfType<MapDisplay>();
    if (drawMode == DrawMode.NoiseMap)
    {
      mapDisplay.Draw(TextureGenerator.TextureFromHeightMap(map));
    }
    else if (drawMode == DrawMode.ColorMap)
    {

      mapDisplay.Draw(GetColorsTexture2D(map));
    }
    else if (drawMode == DrawMode.Mesh)
    {
      mapDisplay.DrawMesh(MeshGenerator.GenerateTerrain(map, heightMultiplier, meshHeightCurve), GetColorsTexture2D(map));
    }
    else if (drawMode == DrawMode.Voxel)
    {
      mapDisplay.DrawMesh(MeshGenerator.GenerateVoxel(map, heightMultiplier, meshHeightCurve, maxVoxelHeight), GetColorsTexture2D(map));
    }
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

[System.Serializable]
public struct TerrainType
{
  public string name;
  public float height;
  public Color color;
}