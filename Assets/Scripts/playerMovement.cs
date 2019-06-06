using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public GridManager gridScript;
    public clickManager clickScript;
    public bool initiated = true;
    public float minZoneX = 2;
    public float maxZoneX = 5;
    public float minZoneY = 4;
    public float maxZoneY = 3;


    // Start is called before the first frame update
    void Start()
    {
       // gameObject.transform.position = gridScript.tile[1,1].transform.position;
       // Debug.Log(gridScript.tile[1,1].transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        
        
        
    }

    public void Initiation()
    {
        

          //  the character is moved to the clicked tile based on requirements from the grid manager script
            Debug.Log ("its working!");
            gameObject.transform.position = clickScript.selectedTile[gridScript.col, gridScript.row].transform.position;
            
            clickScript.gameStart = true;

        
    }
}
