using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileClick : MonoBehaviour {
  public Tilemap tileMap;
  public TileBase evilTile;
  public TileBase goodTile;
  public TileBase impassableTerrain;
  Vector3Int goodStartPosition;
  Vector3Int evilStartPosition;
  public Vector2Int minTilePosition;
  public Vector2Int maxTilePosition;

  int turnCount = 0;

  void Start() {
    goodStartPosition = new Vector3Int(-3, 8, 0);
    evilStartPosition = new Vector3Int(3, -5, 0);
    tileMap.SetTile(goodStartPosition, goodTile);
    tileMap.SetTile(evilStartPosition, evilTile);
  }

  void Update() {
    if (Input.GetKeyDown("space")) {
      doNextTurn();
    }
  }

  void doNextTurn() {
    paintCircle(evilStartPosition, turnCount + 1);
    paintCircle(goodStartPosition, turnCount + 1);
    turnCount += 1;
  }

  void paintCircle(Vector3Int center, int radius) {
    Vector3Int[] positions = getPostionsInRadiusExludingImpassableTerrain(center, radius);

    TileBase[] tileArray = new TileBase[positions.Length];
    tileArray.Fill(evilTile);
    tileMap.SetTiles(positions, tileArray);
  }

  Vector3Int[] getPostionsInRadius(Vector3Int center, int range) {
    List<Vector3Int> postions = new List<Vector3Int>();
    for (int dx = -range; dx <= range; dx++) {
      for (int dy = -range; dy <= range; dy++) {
        Vector3Int postion = new Vector3Int(center.x + dx, center.y + dy, 0);
        if (distance(postion.x, postion.y, center.x, center.y) <= range) {
          if (minTilePosition.x <= postion.x && postion.x <= maxTilePosition.x && minTilePosition.y <= postion.y && postion.y <= maxTilePosition.y) {
            postions.Add(postion);
          }
        }
      }
    }
    return postions.ToArray();
  }

  Vector3Int[] getPostionsInRadiusExludingImpassableTerrain(Vector3Int center, int range) {
    List<Vector3Int> postions = new List<Vector3Int>();
    for (int dx = -range; dx <= range; dx++) {
      for (int dy = -range; dy <= range; dy++) {
        Vector3Int postion = new Vector3Int(center.x + dx, center.y + dy, 0);
        if (distance(postion.x, postion.y, center.x, center.y) <= range) {
          if (minTilePosition.x <= postion.x && postion.x <= maxTilePosition.x && minTilePosition.y <= postion.y && postion.y <= maxTilePosition.y) {
            if (tileMap.GetTile(postion) != impassableTerrain) {
              postions.Add(postion);
            }
          }
        }
      }
    }
    return postions.ToArray();
  }

  int distance(int x0, int y0, int x1, int y1) {
    int cx0 = x0 - (y0 - (y0 & 1)) / 2;
    int cz0 = y0;
    int cy0 = -cx0 - cz0;
    int cx1 = x1 - (y1 - (y1 & 1)) / 2;
    int cz1 = y1;
    int cy1 = -cx1 - cz1;
    int dx = Mathf.Abs(cx0 - cx1);
    int dy = Mathf.Abs(cy0 - cy1);
    int dz = Mathf.Abs(cz0 - cz1);
    return Mathf.Max(dx, Mathf.Max(dy, dz));
  }

}

public static class ArrayExtensions {
  public static void Fill<T>(this T[] originalArray, T with) {
    for (int i = 0; i < originalArray.Length; i++) {
      originalArray[i] = with;
    }
  }
}