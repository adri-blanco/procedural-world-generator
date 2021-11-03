using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
  public Renderer map;
  public MeshFilter meshFilter;
  public MeshRenderer meshRenderer;
  public MeshCollider meshCollider;
  public MeshFilter voxelMeshFilter;
  public MeshRenderer voxelMeshRenderer;
  public MeshCollider voxelMeshCollider;

  public void Draw(Texture2D texture)
  {
    map.transform.localScale = new Vector3(texture.width, texture.height, 1);
    map.sharedMaterial.mainTexture = texture;
  }

  public void DrawMesh(MeshData meshData, Texture2D texture)
  {
    voxelMeshFilter.sharedMesh.Clear();
    voxelMeshCollider.sharedMesh.Clear();

    Mesh mesh = meshData.createMesh();
    meshFilter.sharedMesh = mesh;
    meshCollider.sharedMesh = mesh;
    meshRenderer.sharedMaterial.mainTexture = texture;
  }

  public void DrawVoxelMesh(MeshData meshData)
  {
    meshFilter.sharedMesh.Clear();
    meshCollider.sharedMesh.Clear();

    Mesh mesh = meshData.createMesh();
    voxelMeshFilter.sharedMesh = mesh;
    voxelMeshCollider.sharedMesh = mesh;
  }
}
