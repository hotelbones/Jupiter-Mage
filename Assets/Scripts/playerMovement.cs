using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public GridManager gridScript;
    public clickManager clickScript;
    public bool initiated = true;
    public float capacityX;
    public float capacityY;
    public float movementSpeedX;
    public float movementSpeedY;
    public SpriteRenderer prepSelect;
    public bool prepCheck = false;  



    // Start is called before the first frame update
    void Start()
    {
       // gameObject.transform.position = gridScript.tile[1,1].transform.position;
       // Debug.Log(gridScript.tile[1,1].transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown("space") && clickScript.gameStart == true){
            prepCheck = true;
            TileMove();
        }
        
    }

    public void Initiation()
    {
        

          //  the character is moved to the clicked tile based on requirements from the grid manager script
            
            gameObject.transform.position = clickScript.selectedTile[clickScript.selectedCol, clickScript.selectedRow].transform.position;
            
            clickScript.gameStart = true;

            gridScript.postOrientation();

        
    }

    public void TileMove(){

        prepSelect = clickScript.selectedTile[clickScript.selectedCol, clickScript.selectedRow].GetComponent<SpriteRenderer>();
        prepSelect.color = Color.red;

        if (prepCheck == true){

        gameObject.transform.position = clickScript.selectedTile[clickScript.selectedCol, clickScript.selectedRow].transform.position;
        gridScript.postOrientation();
        prepCheck = false;
        }

        // if (gameObject.transform.position.x <= clickScript.selectedTile[clickScript.selectedCol, clickScript.selectedRow].transform.position.x){
        //     gameObject.transform.position.x += movementSpeedX * Time.deltaTime; 
        // }else{

        // }
        //implement an animation
    }
}
