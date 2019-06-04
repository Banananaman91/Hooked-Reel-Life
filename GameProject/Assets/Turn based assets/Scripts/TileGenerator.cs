using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{

    [SerializeField] GameObject[] tiles;
    [SerializeField] private int width;
    [SerializeField] private int length;
    public List<GameObject> tileList;

    public void Start()
    {
        GenerateTiles();
    }

    public void GenerateTiles()
    {
        int previousTileX = 0;
        int previousTileY = 0;
        for (int i = 0; i < width; i++)
        {
            GameObject newTileX = Instantiate(tiles[0]);
            if (tileList.Count > 0)
            {
                var position = tileList[previousTileX].transform.position;
                newTileX.transform.position = new Vector3(position.x + 1, position.y, position.z);
            }
            
            tileList.Add(newTileX);
            
            for (int j = 0; j < length; j++)
            {
                GameObject newTileZ = Instantiate(tiles[0]);
                if (tileList.Count > 0)
                {
                    var position = tileList[previousTileY].transform.position;
                    newTileZ.transform.position = new Vector3(position.x, position.y, position.z + 1);
                }
            
                tileList.Add(newTileZ);
                previousTileY++;
            }

            previousTileX = previousTileY - length;
            previousTileY++;
        }
    }

    
}
