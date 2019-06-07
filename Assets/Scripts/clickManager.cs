using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickManager : MonoBehaviour
{

    public Collider2D [,] selectedTile; 
    public int selectedRow;
    public int selectedCol;
    public playerMovement playerScript;
    public GridManager gridScript;

    public bool gameStart = false; 
    // Start is called before the first frame update
    void Start()
    {
        selectedTile = new Collider2D[40,40];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {

            //The click creates a raycast that detects any colliders below the mouse
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            //Debug.Log(hit.collider.gameObject.name);

            if (hit.collider != null){
            
            if (playerScript.prepSelect != null){
            playerScript.prepSelect.color = Color.blue;
            }
            //selectedTile[gridScript.col,gridScript.row] = hit.collider;
            for(int row = 0; row < gridScript.rows; row++){
               // Debug.Log(row);
                for(int col = 0; col < gridScript.cols; col++){
                  //  Debug.Log(col);
                    if (gridScript.tile[col, row] == hit.collider.gameObject){
                        selectedTile[col, row] = hit.collider;
                        selectedCol = col;
                        selectedRow = row;
                        Debug.Log(selectedTile[col,row]);
                        if (gameStart == false){
                        playerScript.Initiation();
                        // gameStart = true;
                        }else{
                        playerScript.TileMove();
                        }

                    }
                }
            }
           // Debug.Log(selectedTile[gridScript.col, gridScript.row] + " is now equal to " + hit.collider);

            //The first click will place the character 
            

        } else{
            Debug.Log("Nothing is there!");
        }
        }

        
    }
}
