using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkDegree : MonoBehaviour {

    public Transform tr;
    public bool isDegreeRight;
	// Use this for initialization
	void Awake () {
        tr = this.gameObject.GetComponent<Transform> ();
        StartCoroutine ( this.DegreeCheck () );
        isDegreeRight = false;
    }

    


    IEnumerator DegreeCheck()
    {
        yield return new WaitForSeconds ( 0.2f );

        if ( (tr.localRotation.eulerAngles.x <= 300 && tr.localRotation.eulerAngles.x >= 60) || (tr.localRotation.eulerAngles.z <= 300 && tr.localRotation.eulerAngles.z >= 60) )
        {
            Debug.Log(tr.localRotation.eulerAngles.x);
            Debug.Log(tr.localRotation.eulerAngles.z);
           
            Debug.Log ( "이제 액체를 쏟을 차례" );
            isDegreeRight = true;
            //SendMessage ("activeLiquid",SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            isDegreeRight = false;

        }
        StartCoroutine ( DegreeCheck () );
    }

 
}
