using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chocoSauceCheck : MonoBehaviour
{

    private void OnTriggerEnter( Collider col )
    {
        if ( col.name == "TakeOutCup" )
        {
            if ( CoffeeManager.instance.mochastate == CoffeeManager.CafeMochaStatus.Beans )
            {
                CoffeeManager.instance.DebugCoffeeState ();
            }
        }
    }
}
