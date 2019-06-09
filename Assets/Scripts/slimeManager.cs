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
                slimeRow[row - yMin] = row;
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
        Debug.Log("yMin and yMax = " + yMin + "," + yMax);
        //Debug.Log("selectedSlime positions = " + selectedSlimeCol + "," + selectedSlimeRow);
        for (int col = xMin; col <= xMax; col++){
           // Debug.Log("col =  " + col + " -  xMin = " + xMin + " = slimecol[col - xMin] = " + slimeCol[col - xMin]);
            //Debug.Log("clickScript.selectedCol = " + clickScript.selectedCol + " and selectedSlimeCol = " + selectedSlimeCol); 
            //Debug.Log(clickScript.selectedCol - selectedSlimeCol); 
            if (selectedSlimeCol == -1){
                //Debug.Log("col - xMin = " + (col - xMin));
                selectedSlimeCol = slimeCol[col - xMin];
                
                
            }else if ((clickScript.selectedCol - selectedSlimeCol) == 0){
                col = xMax;
                Debug.Log("STOPPING");
            }
            else if (Mathf.Abs (selectedSlimeCol - clickScript.selectedCol) > Mathf.Abs(slimeCol[col - xMin] - clickScript.selectedCol)){
                    //Debug.Log("selectedSlime = " + (selectedSlimeCol - clickScript.selectedCol) + " new slime = " + (slimeCol[col-xMin] - clickScript.selectedCol));
                    selectedSlimeCol = slimeCol[col - xMin];
                    //Debug.Log("TRANSFERRING...");
            }
            
        }
         for (int row = yMin; row <= yMax; row++){
            if (selectedSlimeRow == -1){
                selectedSlimeRow = slimeRow[row - yMin];
            }else if ((clickScript.selectedRow - selectedSlimeRow) == 0){
                row = yMax;
                Debug.Log("STOPPING");
            }else{
                if (Mathf.Abs(selectedSlimeRow - clickScript.selectedRow) > Mathf.Abs(slimeRow[row - yMin] - clickScript.selectedRow)){
                    selectedSlimeRow = slimeRow[row - yMin];
                    Debug.Log("TRANSFERRING");
                }
            }
        }

        Debug.Log("referring to" + selectedSlimeCol + "," + selectedSlimeRow);

          if (slimeTile[selectedSlimeCol, selectedSlimeRow] == null){
              int rowHold = selectedSlimeRow;
              int colHold = selectedSlimeCol;
              //Debug.Log("my time to shine");
              bool upCheck = false;
              bool downCheck = false;
              bool leftCheck = false;
              bool rightCheck = false;
            if (upCheck == false){
            for(int newPos = selectedSlimeRow; newPos > (selectedSlimeRow - yMin); newPos--){
                //Debug.Log(selectedSlimeRow);
                if (slimeTile[selectedSlimeCol, selectedSlimeRow] == null && newPos > 0){
                    selectedSlimeRow = selectedSlimeRow - 1;
                    Debug.Log(slimeTile[selectedSlimeCol, newPos]);
                }else{
                    //Debug.Log("reverting");
                    selectedSlimeRow = rowHold;
                    upCheck = true;
                }
            } 
            }
            // if (downCheck == false){
            // for (int newPos = selectedSlimeRow; newPos > (yMax - selectedSlimeRow); newPos++){
            //     Debug.Log("my time to shine...AGAIN");
            //     if (slimeTile[selectedSlimeCol, selectedSlimeRow] == null && newPos < gridScript.rows){
            //         selectedSlimeRow = selectedSlimeRow + 1;
            //         Debug.Log(selectedSlimeCol + "," + newPos);
            //         Debug.Log(slimeTile[selectedSlimeCol, newPos]);
            //     }else{
            //         selectedSlimeRow = rowHold;
            //         downCheck = true;
            //     }
            // }
            // }
            //if (leftCheck == false){
            // for (int newPos = colHold; newPos > (colHold - xMin); newPos--){
            //     Debug.Log("xshine");
            //     if (slimeTile[selectedSlimeCol, selectedSlimeRow] == null && newPos > 0){
            //         selectedSlimeCol = selectedSlimeCol - 1;
            //         Debug.Log(slimeTile[selectedSlimeCol, newPos]);
            //     }else{
            //         Debug.Log("you caught me");
            //         selectedSlimeCol = colHold;
            //     }
            // }
           // }
//             if (rightCheck == false){
                
//             for (int newPos = selectedSlimeCol; (newPos - 2) < (xMax - selectedSlimeCol); newPos++){
//                // Debug.Log("xshin2e");
//                 if (slimeTile[selectedSlimeCol, selectedSlimeRow] == null && selectedSlimeCol < gridScript.cols ){
//                     selectedSlimeCol = selectedSlimeCol + 1;
// //                    Debug.Log(slimeTile[selectedSlimeCol, selectedSlimeRow]);
//                 }else{
//                     selectedSlimeCol = colHold;
//                     rightCheck = true;
//                    // Debug.Log("reverting"); 
//                 }
//             }
//             }
                                
         }

         

                //This portion of the function checks if the column is closer to the player, or the row. Then depending on which one is lesser
                //checks if the direction of the player from the slime is negative or positive on the grid. Then depending on that,
                //changes the min and maximum of the direction so the array can be expanded. If this number goes outside of the grid
                //Then assigns the new slime col to the expanded
                //array. Then that new assigned col generates a new slime tile onto the new position, referencing the previous tile. 

                

                if(Mathf.Abs(clickScript.selectedCol - selectedSlimeCol) == 1 && Mathf.Abs(clickScript.selectedRow - selectedSlimeRow) == 1){
                    Debug.Log("flipping");
                    bool coinFlip = (Random.value > 0.5);
                    if (coinFlip == true){
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

                        }

                    }

                    slimeTile[newSlimeCol, selectedSlimeRow] = (GameObject)Instantiate(slimeTile[selectedSlimeCol,selectedSlimeRow], new Vector2(gridScript.tile[newSlimeCol,selectedSlimeRow].transform.position.x, gridScript.tile[newSlimeCol,selectedSlimeRow].transform.position.y), Quaternion.identity);
                    slimeTile[newSlimeCol, selectedSlimeRow].transform.SetParent(gridScript.gameObject.transform, true);
                    slimeTile[newSlimeCol, selectedSlimeRow].name = "slimeTile [" + newSlimeCol + "," + selectedSlimeRow + "]";
                    slimeTile[newSlimeCol, selectedSlimeRow].transform.position = gridScript.tile[newSlimeCol, selectedSlimeRow].transform.position;

                    }else if(coinFlip == false){
                        if (Mathf.Sign(clickScript.selectedRow - selectedSlimeRow) == -1){
                        newSlimeRow = selectedSlimeRow - 1;
                        yMin = yMin - 1;
                        for (int row = yMin; row <= yMax; row++){
                            slimeRow[row - yMin] = row;
                        }
                    }else if(Mathf.Sign(clickScript.selectedRow - selectedSlimeRow) == 1){
                        newSlimeRow = selectedSlimeRow + 1;
                        yMax = yMax + 1;
                        for (int row = yMin; row <= yMax; row++){
                            slimeRow[row - yMin] = row;
                        }
                    }
                    Debug.Log("instantiating" + selectedSlimeCol + "," + newSlimeRow);
                    slimeTile[selectedSlimeCol, newSlimeRow] = (GameObject)Instantiate(slimeTile[selectedSlimeCol,selectedSlimeRow], new Vector2(gridScript.tile[selectedSlimeCol,newSlimeRow].transform.position.x, gridScript.tile[selectedSlimeCol,newSlimeRow].transform.position.y), Quaternion.identity);
                    slimeTile[selectedSlimeCol, newSlimeRow].transform.SetParent(gridScript.gameObject.transform, true);
                    slimeTile[selectedSlimeCol, newSlimeRow].name = "slimeTile [" + selectedSlimeCol + "," + newSlimeRow + "]";
                    slimeTile[selectedSlimeCol, newSlimeRow].transform.position = gridScript.tile[selectedSlimeCol, newSlimeRow].transform.position;
                    }
                }else if ((clickScript.selectedCol - selectedSlimeCol) != 0){
                    
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
                //Debug.Log("initalizing a row");
                if (Mathf.Abs(clickScript.selectedCol - selectedSlimeCol) > Mathf.Abs(clickScript.selectedRow - selectedSlimeRow) || clickScript.selectedCol - selectedSlimeCol == 0){
                   rowCounter = rowCounter + 1;
                   Debug.Log("I've run rowCounter " + rowCounter + " times");
//                   Debug.Log(clickScript.selectedRow - selectedSlimeRow);
                    if (Mathf.Sign(clickScript.selectedRow - selectedSlimeRow) == -1){
                        Debug.Log("And I am going up right now");
                        newSlimeRow = selectedSlimeRow - 1;
                        yMin = yMin - 1;
                        for (int row = yMin; row <= yMax; row++){
                            slimeRow[row - yMin] = row;
                        }
                    }else if(Mathf.Sign(clickScript.selectedRow - selectedSlimeRow) == 1){
                        newSlimeRow = selectedSlimeRow + 1;
                        yMax = yMax + 1;
                        for (int row = yMin; row <= yMax; row++){
                            slimeRow[row - yMin] = row;
                        }
                    }
                    Debug.Log("instantiating" + selectedSlimeCol + "," + newSlimeRow);
                    slimeTile[selectedSlimeCol, newSlimeRow] = (GameObject)Instantiate(slimeTile[selectedSlimeCol,selectedSlimeRow], new Vector2(gridScript.tile[selectedSlimeCol,newSlimeRow].transform.position.x, gridScript.tile[selectedSlimeCol,newSlimeRow].transform.position.y), Quaternion.identity);
                    slimeTile[selectedSlimeCol, newSlimeRow].transform.SetParent(gridScript.gameObject.transform, true);
                    slimeTile[selectedSlimeCol, newSlimeRow].name = "slimeTile [" + selectedSlimeCol + "," + newSlimeRow + "]";
                    slimeTile[selectedSlimeCol, newSlimeRow].transform.position = gridScript.tile[selectedSlimeCol, newSlimeRow].transform.position;
//                    Debug.Log(selectedSlimeRow);
                }
            }
    }
}
