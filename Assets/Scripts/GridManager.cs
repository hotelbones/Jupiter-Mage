using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 5;
    public int cols = 8;
    public int initiationMinPosX;
    public int initiationMaxPosX;
    public int initiationMinPosY;
    public int initiationMaxPosY;
    public  int row;
    public  int col;
    public float tileSize = 1;
    public GameObject [,] tile;

    SpriteRenderer highlighter;
    
    


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
                Collider2D tileEnabler = tile[col,row].GetComponent<Collider2D>();
                tileEnabler.enabled = false;
                
                //If the generated tile is within the given limits, it will also generate a collider, then color those tiles blue.
                if (col >= initiationMinPosX && row >= initiationMinPosY && col <= initiationMaxPosX && row <= initiationMaxPosY){
                    Debug.Log("running!");
                    highlighter = tileEnabler.GetComponent<SpriteRenderer>();
                    highlighter.color = Color.blue;
                    tileEnabler.enabled = true;
                }

            }

        }

        Destroy(referenceTile);

        float gridW = cols * tileSize;
        float gridH = rows * tileSize;
        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);
    }

    public void postOrientation(){
         for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {   highlighter = tile[col,row].GetComponent<SpriteRenderer>();
                if (highlighter.color == Color.blue){
                    
                    highlighter.color = new Color(255, 255, 255);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
