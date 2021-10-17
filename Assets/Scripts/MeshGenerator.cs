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
}

public class MeshData
{
  public Vector3[] vertices;
  public int[] triangles;
  // Normal vectors to know which part of the texture is applied to each vertice
  public Vector2[] uvs;
  int triangleIndex;
  public MeshData(int width, int height)
  {
    vertices = new Vector3[width * height];
    uvs = new Vector2[width * height];
    triangles = new int[(width - 1) * (height - 1) * 6];
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