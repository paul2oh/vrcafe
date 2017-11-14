using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxCheck : MonoBehaviour {

    public bool isBox;

    void Start()
    {
        isBox = true;
    }

    void OnTriggerExit(Collider col )
    {
        if ( col.gameObject.name == "box" )
        {
            isBox = false;
        }
      
    }

    void OnTriggerEnter( Collider col )
    {
        if ( col.gameObject.name == "box" )
        {
            isBox = true;
        }

    }

   
}
