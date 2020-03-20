using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileClick : MonoBehaviour
{
  public Tilemap tileMap;
  public TileBase swappedTile;
  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Vector3 position = Input.mousePosition;
      Vector3Int tilePos = tileMap.WorldToCell(Camera.main.ScreenToWorldPoint(position));
      tileMap.SetTile(tilePos, swappedTile);
    }
  }
}
