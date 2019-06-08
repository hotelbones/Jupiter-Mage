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
    int runCounter;
    int colCounter;
    int rowCounter;



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
        slimeCol = new int[40];
        slimeRow = new int[40];

        //A reference slime is created and then for loops run between the restricted coordinates to create the correct grid 
        //tiles which are placed based on grid tile positions. Then the slimeTiles are named and their
        //column and row are assigned. referenceCol is used for later algorithms to reference the furthest column. Same with row. 

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

    //

    public void MoveSlime(){
        int selectedSlimeCol = -1;
        int selectedSlimeRow = -1;
        runCounter = runCounter + 1;
        Debug.Log("I've run runCounter " + runCounter + " times");
        //Debug.Log("xMin and yMin = " + xMin + "," + yMin);
        //Debug.Log("selectedSlime positions = " + selectedSlimeCol + "," + selectedSlimeRow);
        for (int col = xMin; col <= referenceCol; col++){
           // Debug.Log("col =  " + col + " -  xMin = " + xMin + " = slimecol[col - xMin] = " + slimeCol[col - xMin]);
          
            if (selectedSlimeCol == -1){
                selectedSlimeCol = slimeCol[col - xMin];
                
//                Debug.Log(slimeCol[col - xMin]); 
            }else if ((clickScript.selectedCol - selectedSlimeCol) == 0){
                col = referenceCol;
                Debug.Log("STOPPING");
            }
            else if (Mathf.Abs (selectedSlimeCol - clickScript.selectedCol) > Mathf.Abs(slimeCol[col - xMin] - clickScript.selectedCol)){
                    Debug.Log("selectedSlime = " + (selectedSlimeCol - clickScript.selectedCol) + " new slime = " + (slimeCol[col-xMin] - clickScript.selectedCol));
                    selectedSlimeCol = slimeCol[col - xMin];
                    //Debug.Log("TRANSFERRING...");
            }
            
        }
         for (int row = yMin; row <= referenceRow; row++){
            if (selectedSlimeRow == -1){
                selectedSlimeRow = slimeRow[row - yMin];
            }else if ((clickScript.selectedRow - selectedSlimeRow) == 0){
                row = referenceRow;
                Debug.Log("STOPPING");
            }else{
                if (Mathf.Abs(selectedSlimeRow - clickScript.selectedRow) > Mathf.Abs(slimeRow[row - yMin] - clickScript.selectedRow)){
                    selectedSlimeRow = slimeRow[row - yMin];
                    Debug.Log("TRANSFERRING");
                }
            }
        }

                //This portion of the function checks if the column is closer to the player, or the row. Then depending on which one is lesser
                //checks if the direction of the player from the slime is negative or positive on the grid. Then depending on that,
                //changes the min and maximum of the direction so the array can be expanded. If this number goes outside of the grid
                //Then assigns the new slime col to the expanded
                //array. Then that new assigned col generates a new slime tile onto the new position, referencing the previous tile. 

                   Debug.Log("referring to" + selectedSlimeCol + "," + selectedSlimeRow);
                if ((clickScript.selectedCol - selectedSlimeCol) != 0){
                if (Mathf.Abs(clickScript.selectedCol - selectedSlimeCol) < Mathf.Abs(clickScript.selectedRow - selectedSlimeRow)){
                    colCounter = colCounter + 1;
                    Debug.Log("I've run colCounter " + colCounter + " times");
                    if (Mathf.Sign(clickScript.selectedCol - selectedSlimeCol) == -1){
                        newSlimeCol = selectedSlimeCol - 1;
                        xMin = xMin - 1;
                        for (int col = xMin; col <= xMax; col++){
                            slimeCol[col - xMin] = col;
                        }
                        
                    }else if(Mathf.Sign(clickScript.selectedCol - selectedSlimeCol) == 1){
                        newSlimeCol = selectedSlimeCol + 1;
                        xMax = xMax + 1;
                        for (int col = xMin; col <= xMax; col++){
                            slimeCol[col - xMin] = col;
                            //Debug.Log("slimeCol: " + slimeCol[col] + " equals " + col);
                        }
                        //Debug.Log("New SlimeCol has value of " + xMax);
                    }
//                    Debug.Log(Mathf.Sign(clickScript.selectedCol - selectedSlimeCol));
                    //Debug.Log("instantiating" + newSlimeCol + "," + selectedSlimeRow);
                    slimeTile[newSlimeCol, selectedSlimeRow] = (GameObject)Instantiate(slimeTile[selectedSlimeCol,selectedSlimeRow], new Vector2(gridScript.tile[newSlimeCol,selectedSlimeRow].transform.position.x, gridScript.tile[newSlimeCol,selectedSlimeRow].transform.position.y), Quaternion.identity);
                    slimeTile[newSlimeCol, selectedSlimeRow].transform.SetParent(gridScript.gameObject.transform, true);
                    slimeTile[newSlimeCol, selectedSlimeRow].name = "slimeTile [" + newSlimeCol + "," + selectedSlimeRow + "]";
                    slimeTile[newSlimeCol, selectedSlimeRow].transform.position = gridScript.tile[newSlimeCol, selectedSlimeRow].transform.position;
                    
                    }
                }else if ((clickScript.selectedRow - selectedSlimeRow) != 0){
                
                if (Mathf.Abs(clickScript.selectedCol - selectedSlimeCol) > Mathf.Abs(clickScript.selectedRow - selectedSlimeRow) || clickScript.selectedCol - selectedSlimeCol == 0){
                   rowCounter = rowCounter + 1;
                   Debug.Log("I've run rowCounter " + rowCounter + " times");
                   Debug.Log(clickScript.selectedRow - selectedSlimeRow);
                    if (Mathf.Sign(clickScript.selectedRow - selectedSlimeRow) == -1){
                        newSlimeRow = selectedSlimeRow - 1;
                        yMin = yMin - 1;
                        for (int row = yMin; row <= yMax; row++){
                            slimeCol[row - yMin] = row;
                        }
                    }else if(Mathf.Sign(clickScript.selectedRow - selectedSlimeRow) == 1){
                        newSlimeRow = selectedSlimeRow + 1;
                        yMax = yMax + 1;
                        for (int row = yMin; row <= yMax; row++){
                            slimeCol[row - yMin] = row;
                        }
                    }
                    Debug.Log(selectedSlimeCol + "," + newSlimeRow);
                    slimeTile[selectedSlimeCol, newSlimeRow] = (GameObject)Instantiate(slimeTile[selectedSlimeCol,selectedSlimeRow], new Vector2(gridScript.tile[selectedSlimeCol,newSlimeRow].transform.position.x, gridScript.tile[selectedSlimeCol,newSlimeRow].transform.position.y), Quaternion.identity);
                    slimeTile[selectedSlimeCol, newSlimeRow].transform.SetParent(gridScript.gameObject.transform, true);
                    slimeTile[selectedSlimeCol, newSlimeRow].name = "slimeTile [" + selectedSlimeCol + "," + newSlimeRow + "]";
                    slimeTile[selectedSlimeCol, newSlimeRow].transform.position = gridScript.tile[selectedSlimeCol, newSlimeRow].transform.position;
                    Debug.Log(selectedSlimeRow);
                }
            }
    }
}
