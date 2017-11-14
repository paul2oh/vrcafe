using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePowder : MonoBehaviour {

    public MeshRenderer powder;
    public bool powderBool;



    // Use this for initialization
    void Start () {
        powder = GetComponent<MeshRenderer> ();
        powderBool = false;

        //powderBool = coffeePowder.enabled;

    }



    void Powder()
    {
        Debug.Log ( "powder active !!" );
        powder.enabled= true ;
        //StartCoroutine ( showCoffee () );
        powderBool = true ;
    }

    //IEnumerator showCoffee()
    //{
    //    yield return new WaitForSeconds ( 0.2f );
    //    Debug.Log ( "showCoffee 코루틴" );
    //    coffeePowder.SendMessage ( "showCoffee" , powderBool , SendMessageOptions.DontRequireReceiver );
    //    StartCoroutine ( showCoffee () );

    //}
}
