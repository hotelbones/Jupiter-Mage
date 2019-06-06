using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 5;
    public int cols = 8;
    int row;
    int col;
    public float tileSize = 1;
    public GameObject [,] tile;
    
    


    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
        
    }

    public void GenerateGrid()
    {
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("cavefloortile"));
       // Debug.Log(referenceTile.transform);
        tile = new GameObject[40, 40];
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                tile[col,row] = (GameObject)Instantiate(referenceTile, transform);
                tile[col,row].name = "cavefloortile[" + col + "," + row + "]";
                //Debug.Log(tile[col, row]);

                float posX = col * tileSize;
                float posY = row * -tileSize;

                tile[col,row].transform.position = new Vector2(posX, posY);
            }

        }

      //  Debug.Log(tile[1,1].transform.position);
        Destroy(referenceTile);

        float gridW = cols * tileSize;
        float gridH = rows * tileSize;
        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
