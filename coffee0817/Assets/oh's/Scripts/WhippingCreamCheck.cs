using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhippingCreamCheck : MonoBehaviour {

    private void OnTriggerEnter( Collider col )
    {
        if ( col.name == "TakeOutCup" )
        {
            if ( CoffeeManager.instance.mochastate == CoffeeManager.CafeMochaStatus.steammilk )
            {
                CoffeeManager.instance.DebugCoffeeState ();
            }
        }
    }
}
