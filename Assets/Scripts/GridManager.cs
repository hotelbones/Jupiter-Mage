using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows;
    public int cols;
    public int initiationMinPosX;
    public int initiationMaxPosX;
    public int initiationMinPosY;
    public int initiationMaxPosY;
    public int myxoMinPosX;
    public int myxoMaxPosX;
    public int myxoMinPosY;
    public int myxoMaxPosY;
    
    public  int row;
    public  int col;

    //tileSize determines how far apart the tiles are from one another. It may also be possible for bigger sprites to make up the grid. 
    public float tileSize = 1;
    public GameObject [,] tile;

    SpriteRenderer highlighter;

    public playerMovement playerScript;

    public clickManager clickScript;

    public slimeManager slimeScript;
    Collider2D tileEnabler;
    
    


    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
       
        
    }

    //Generates the grid and then determines which tiles are navigatable based on public integers. 

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
                tileEnabler = tile[col,row].GetComponent<Collider2D>();
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
        slimeScript.GenerateSlime();

        Destroy(referenceTile);

        float gridW = cols * tileSize;
        float gridH = rows * tileSize;
        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);


    }

    //Resets all the grid blocks after the initial placement
    public void postOrientation(){
         
         for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {   highlighter = tile[col,row].GetComponent<SpriteRenderer>();
                if (highlighter.color == Color.blue){
                    tileEnabler = tile[col,row].GetComponent<Collider2D>();
                    
                    highlighter.color = new Color(255, 255, 255, 0);
                    tileEnabler.enabled = false;
                }
            }
        }
        playerCapacity();
    }

    //Visualizes the places the player can move next. 
    public void playerCapacity(){
        

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            { 
                //Debug.Log(col + "," + row);
                if(col >= clickScript.selectedCol - playerScript.capacityX && col <= clickScript.selectedCol + playerScript.capacityX 
                && row >= clickScript.selectedRow - playerScript.capacityY && row <= clickScript.selectedRow + playerScript.capacityY
                ){
                    //Debug.Log("running tile enabler!");
                    tileEnabler = tile[col,row].GetComponent<Collider2D>();
                    highlighter = tileEnabler.GetComponent<SpriteRenderer>();
                    highlighter.color = Color.blue;
                    tileEnabler.enabled = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
