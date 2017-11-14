using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coffeeCheck : MonoBehaviour {

 //   public GameObject coffeePowder;
    //public bool isBoxInThePowder;
    public bool isCoffeeScoop;
    public bool isTemperAction;
    public checkHandle ckHandle;

    // Use this for initialization
    void Start () {
      //  coffeePowder = GameObject.FindWithTag ("coffeePowder");
       // coffeePowder.SetActive(false);
        ckHandle = GameObject.Find ( "checkHandle" ).GetComponent<checkHandle> ();
        isCoffeeScoop = false;
        isTemperAction = false;
        //StartCoroutine ( this.potafilterCoffeeCheck () );
       
    }

    void OnTriggerEnter( Collider col )
    {
        if ( col.name == "box" )
        {
            Debug.Log ( "숟가락질 했음" );
            isCoffeeScoop = true;
        }
        else if ( col.name == "Temper" )
        {
            isTemperAction = true;
        }

    }

    void OnTriggerExit( Collider col )
    {
        if ( col.name == "box" )
        {
            isCoffeeScoop = false;
        }

    }

    //IEnumerator potafilterCoffeeCheck()
    //{
    //    yield return new WaitForSeconds ( 0.2f );
    //    isBoxInThePowder = ckHandle.checkPowder;

    //    if ( isCoffeeScoop && isBoxInThePowder)
    //    {
    //        Debug.Log ( "포타필터로 커피 담기" );
    //        coffeePowder.enabled = true;
    //    }
    //    else if ( isTemperAction || !isBoxInThePowder)
    //    {
    //        coffeePowder.enabled = false;

    //    }
    //    StartCoroutine ( potafilterCoffeeCheck ());
    //}


  
   


  
}
