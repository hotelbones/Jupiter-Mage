using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public GridManager gridScript;

     

    // Start is called before the first frame update
    void Start()
    {
       // gameObject.transform.position = gridScript.tile[1,1].transform.position;
        Debug.Log(gridScript.tile[1,1].transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
