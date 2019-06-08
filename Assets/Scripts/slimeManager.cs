using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeManager : MonoBehaviour
{
    public GridManager gridScript;
    public clickManager clickScript;
    int yMin;
    int yMax;
    int xMin;
    int xMax;
    int [] slimeCol;
    int [] slimeRow;
    public int referenceCol;
    public int referenceRow;
    public GameObject [,] slimeTile;

    int newSlimeCol;
    int newSlimeRow;



    // Update is called once per frame
    void Update()
    {
        
    }

    //spawns the slime looking at the minimum and maximum size it should be on the grid
    public void GenerateSlime(){
        yMin = gridScript.myxoMinPosY;
        yMax = gridScript.myxoMaxPosY;
        xMin = gridScript.myxoMinPosX;
        xMax = gridScript.myxoMaxPosX;

        //the number of slime columns/rows in the array are the difference between the max and min. If it is zero, it becomes a 1.
        slimeCol = new int[(xMax - xMin + 1)];
        slimeRow = new int[(yMax - yMin + 1)];

        //A reference slime is created and then for loops run between the restricted coordinates to create the correct grid 
        //tiles which are placed based on grid tile positions. Then the slimeTiles are named and their
        //column and row are assigned.

        slimeTile = new GameObject[40, 40];
        GameObject referenceSlime = (GameObject)Instantiate(Resources.Load("myxoTile"));
        for (int col = xMin; col <= xMax; col++){
           
            for (int row = yMin; row <= yMax; row++){
                
                slimeTile[col,row] = (GameObject)Instantiate(referenceSlime, new Vector2(gridScript.tile[col,row].transform.position.x,gridScript.tile[col,row].transform.position.y), Quaternion.identity);
                slimeTile[col,row].transform.SetParent(gridScript.gameObject.transform, true);
                slimeTile[col,row].name = "slimeTile [" + col + "," + row + "]";
                slimeCol[col - xMin] = col;
                referenceCol = col;
                slimeRow[row - yMin] = row;
                referenceRow = row; 
                Debug.Log(gridScript.tile[col,row]);
                slimeTile[col,row].transform.position = gridScript.tile[col,row].transform.position;
            }
        }
        Destroy(referenceSlime);
    }

    //you need to figure out why the initial for loops in MoveSlime() always assign the same square.

    public void MoveSlime(){
        int selectedSlimeCol = -1;
        int selectedSlimeRow = -1;
        for (int col = xMin; col <= referenceCol; col++){
            if (selectedSlimeCol == -1){
                selectedSlimeCol = slimeCol[col - xMin];
                Debug.Log(slimeCol[col - xMin]); 
            }else{
                if (selectedSlimeCol < slimeCol[col - xMin]){
                    selectedSlimeCol = slimeCol[col - xMin];
                    Debug.Log("running second colcheck");
                }
            }
        }
         for (int row = yMin; row <= referenceRow; row++){
            if (selectedSlimeRow == -1){
                selectedSlimeRow = slimeRow[row - yMin];
            }else{
                if (selectedSlimeRow < slimeRow[row - yMin]){
                    selectedSlimeRow = slimeRow[row - yMin];
                }
            }
        }

                if (Mathf.Abs(clickScript.selectedCol - selectedSlimeCol) < Mathf.Abs(clickScript.selectedRow - selectedSlimeRow)){
                    if (Mathf.Sign(clickScript.selectedCol - selectedSlimeCol) == -1){
                        newSlimeCol = selectedSlimeCol - 1;
                    }else if(Mathf.Sign(clickScript.selectedCol - selectedSlimeCol) == 1){
                        newSlimeCol = selectedSlimeCol + 1;
                    }
//                    Debug.Log(Mathf.Sign(clickScript.selectedCol - selectedSlimeCol));
                    Debug.Log(newSlimeCol + "," + selectedSlimeRow);
                    slimeTile[newSlimeCol, selectedSlimeRow] = (GameObject)Instantiate(slimeTile[selectedSlimeCol,selectedSlimeRow], new Vector2(gridScript.tile[newSlimeCol,selectedSlimeRow].transform.position.x, gridScript.tile[newSlimeCol,selectedSlimeRow].transform.position.y), Quaternion.identity);
                    slimeTile[newSlimeCol, selectedSlimeRow].transform.SetParent(gridScript.gameObject.transform, true);
                    slimeTile[newSlimeCol, selectedSlimeRow].name = "slimeTile [" + newSlimeCol + "," + selectedSlimeRow + "]";
                    slimeTile[newSlimeCol, selectedSlimeRow].transform.position = gridScript.tile[newSlimeCol, selectedSlimeRow].transform.position;
                    
                }
                if (Mathf.Abs(clickScript.selectedCol - selectedSlimeCol) > Mathf.Abs(clickScript.selectedRow - selectedSlimeRow)){
                    if (Mathf.Sign(clickScript.selectedRow - selectedSlimeRow) == -1){
                        newSlimeRow = selectedSlimeRow - 1;
                    }else if(Mathf.Sign(clickScript.selectedRow - selectedSlimeRow) == 1){
                        newSlimeRow = selectedSlimeRow + 1;
                    }
                    
                    slimeTile[selectedSlimeCol, newSlimeRow] = (GameObject)Instantiate(slimeTile[selectedSlimeCol,selectedSlimeRow], new Vector2(gridScript.tile[selectedSlimeCol,newSlimeRow].transform.position.x, gridScript.tile[selectedSlimeCol,newSlimeRow].transform.position.y), Quaternion.identity);
                    slimeTile[selectedSlimeCol, newSlimeRow].transform.SetParent(gridScript.gameObject.transform, true);
                    slimeTile[selectedSlimeCol, newSlimeRow].name = "slimeTile [" + selectedSlimeCol + "," + newSlimeRow + "]";
                    slimeTile[selectedSlimeCol, newSlimeRow].transform.position = gridScript.tile[selectedSlimeCol, newSlimeCol].transform.position;
                    Debug.Log(selectedSlimeRow);
                }
    }
}
