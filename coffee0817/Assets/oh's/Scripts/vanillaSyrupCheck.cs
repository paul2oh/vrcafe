using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vanillaSyrupCheck : MonoBehaviour {

    private void OnTriggerEnter( Collider col )
    {
        if ( col.name == "TakeOutCup" )
        {
            if ( CoffeeManager.instance.caramelstate == CoffeeManager.CaramelMacchiatoStatus.Beans )
            {
                CoffeeManager.instance.DebugCoffeeState ();
            }

        }
    }
}
