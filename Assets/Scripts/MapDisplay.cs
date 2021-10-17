using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
  public Renderer map;

  public void Draw(Texture2D texture)
  {
    map.transform.localScale = new Vector3(texture.width, texture.height, 1);
    map.sharedMaterial.mainTexture = texture;
  }
}
