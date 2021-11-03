using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
  public static MeshData GenerateTerrain(float[,] heightMap, float heightMultiplier, AnimationCurve meshHeightCurve)
  {
    int width = heightMap.GetLength(0);
    int height = heightMap.GetLength(1);
    // To have the vertices centered in the screen. This var set the left corner
    float topLeftX = (width - 1) / -2f;
    // And this one, the top corner
    float topLeftZ = (height - 1) / 2f;

    MeshData meshData = new MeshData(width, height);
    int vertexIndex = 0;

    for (int y = 0; y < height; y++)
    {
      for (int x = 0; x < width; x++)
      {
        meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, meshHeightCurve.Evaluate(heightMap[x, y]) * heightMultiplier, topLeftZ - y);
        // UV's are represented from 0 to 1
        meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);

        // We are creating the triangles from left to right, so, in the last position of the rows
        // and the last row, there is no triangles formed at his right.
        if (y < height - 1 && x < width - 1)
        {
          // The order is important. Should be in clockwise to make the triangle face the camera
          meshData.AddTriangle(vertexIndex, vertexIndex + 1, vertexIndex + width + 1);
          meshData.AddTriangle(vertexIndex + width + 1, vertexIndex + width, vertexIndex);
        }

        vertexIndex++;
      }
    }

    return meshData;
  }

  public static MeshData GenerateVoxel(float[,] heightMap, float heightMultiplier, AnimationCurve meshHeightCurve, int maxVoxelHeight)
  {
    int width = heightMap.GetLength(0);
    int height = heightMap.GetLength(1);
    // To have the vertices centered in the screen. This var set the left corner
    float topLeftX = (width - 1) / -2f;
    // And this one, the top corner
    float topLeftZ = (height - 1) / 2f;

    MeshData meshData = new MeshData(width, height, true);
    int vertexIndex = 0;
    float padding = 0.5f;

    int[,] normalizedHeights = new int[width, height];
    for (int y = 0; y < height; y++)
    {
      for (int x = 0; x < width; x++)
      {
        float evaluatedHeight = meshHeightCurve.Evaluate(heightMap[x, y]) * heightMultiplier;
        normalizedHeights[x, y] = NormalizeHeight(evaluatedHeight, heightMultiplier, maxVoxelHeight);
      }
    }

    int textureWidth = 64;
    int textureHeight = 64;
    int spriteSize = 16;
    int selectionX = 4;
    int selectionY = 1;

    for (int y = 0; y < height; y++)
    {
      for (int x = 0; x < width; x++)
      {
        int pointHeight = normalizedHeights[x, y];
        // top left vertice
        meshData.vertices[vertexIndex] = new Vector3(topLeftX + x - padding, pointHeight, topLeftZ - y + padding);
        meshData.uvs[vertexIndex + 3] = new Vector2((spriteSize * selectionX) / (float)textureWidth, (spriteSize * selectionY) / (float)textureHeight);
        // top right vertice
        meshData.vertices[vertexIndex + 1] = new Vector3(topLeftX + x + padding, pointHeight, topLeftZ - y + padding);
        meshData.uvs[vertexIndex + 2] = new Vector2((spriteSize * selectionX - spriteSize) / (float)textureWidth, (spriteSize * selectionY) / (float)textureHeight);
        // bottom right vertice
        meshData.vertices[vertexIndex + 2] = new Vector3(topLeftX + x + padding, pointHeight, topLeftZ - y - padding);
        meshData.uvs[vertexIndex + 1] = new Vector2((spriteSize * selectionX - spriteSize) / (float)textureWidth, (spriteSize * selectionY - spriteSize) / (float)textureHeight);
        // bottom left vertice
        meshData.vertices[vertexIndex + 3] = new Vector3(topLeftX + x - padding, pointHeight, topLeftZ - y - padding);
        meshData.uvs[vertexIndex] = new Vector2((spriteSize * selectionX) / (float)textureWidth, (spriteSize * selectionY - spriteSize) / (float)textureHeight);

        meshData.AddTriangle(vertexIndex, vertexIndex + 1, vertexIndex + 2);
        meshData.AddTriangle(vertexIndex + 2, vertexIndex + 3, vertexIndex);
        if (x != 0 && normalizedHeights[x, y] != normalizedHeights[x - 1, y])
        {
          meshData.AddTriangle(vertexIndex, vertexIndex - 2, vertexIndex - 3);
          meshData.AddTriangle(vertexIndex, vertexIndex + 3, vertexIndex - 2);
        }

        if (y != 0 && normalizedHeights[x, y] != normalizedHeights[x, y - 1])
        {
          meshData.AddTriangle(vertexIndex, vertexIndex - (width * 4) + 3, vertexIndex - (width * 4) + 2);
          meshData.AddTriangle(vertexIndex, vertexIndex - (width * 4) + 2, vertexIndex + 1);
        }

        vertexIndex += 4;
      }
    }

    return meshData;

  }

  // Transform values betwean [0,1] * heightMultiplier to integers [0, maxVoxelHeight]
  private static int NormalizeHeight(float height, float maxHeight, int maxVoxelHeight)
  {
    return (int)((height * maxVoxelHeight) / maxHeight);
  }
}

public class MeshData
{
  public Vector3[] vertices;
  public int[] triangles;
  // Normal vectors to know which part of the texture is applied to each vertice
  public Vector2[] uvs;
  int triangleIndex;
  public MeshData(int width, int height, bool isVoxel = false)
  {
    int verticesLength = width * height;
    int trianglesLength = (width - 1) * (height - 1) * 6;

    if (isVoxel)
    {
      verticesLength *= 4;
      trianglesLength = (width) * (height) * 18;
    }

    vertices = new Vector3[verticesLength];
    uvs = new Vector2[verticesLength];
    triangles = new int[trianglesLength];
  }

  public void AddTriangle(int a, int b, int c)
  {
    triangles[triangleIndex] = a;
    triangles[triangleIndex + 1] = b;
    triangles[triangleIndex + 2] = c;
    triangleIndex += 3;
  }

  public Mesh createMesh()
  {
    Mesh mesh = new Mesh();
    mesh.vertices = vertices;
    mesh.uv = uvs;
    mesh.triangles = triangles;
    // To make the lights work properly
    mesh.RecalculateNormals();

    return mesh;
  }
}