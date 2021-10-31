using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
  public Renderer map;
  public MeshFilter meshFilter;
  public MeshRenderer meshRenderer;
  public MeshCollider meshCollider;

  public void Draw(Texture2D texture)
  {
    map.transform.localScale = new Vector3(texture.width, texture.height, 1);
    map.sharedMaterial.mainTexture = texture;
  }

  public void DrawMesh(MeshData meshData, Texture2D texture)
  {
    Mesh mesh = meshData.createMesh();
    meshFilter.sharedMesh = mesh;
    meshCollider.sharedMesh = mesh;
    map.transform.localScale = new Vector3(texture.width, texture.height, 1);
    meshRenderer.sharedMaterial.mainTexture = texture;
  }
}
