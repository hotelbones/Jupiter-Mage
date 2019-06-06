using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public GridManager gridScript;
    public clickManager clickScript;
    bool initiated = false;
    public float minZoneX;
    public float maxZoneX;
    public float minZoneY;
    public float maxZoney;

    // Start is called before the first frame update
    void Start()
    {
       // gameObject.transform.position = gridScript.tile[1,1].transform.position;
       // Debug.Log(gridScript.tile[1,1].transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        
        if (initiated == false){
            Initiation();  

        } else {

        }
        
    }

    void Initiation()
    {
        if(Input.GetMouseButtonDown(0)){

          //  if clicked tile fits within the min/max parameters, move tile to that place and end initiation

            Debug.Log(clickScript.selectedTile);
            gameObject.transform.position = clickScript.selectedTile.transform.position;
            
        }
    }
}
