using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caramelSauceCheck : MonoBehaviour {

    private void OnTriggerEnter( Collider col )
    {
        if ( col.name == "TakeOutCup" )
        {
            if ( CoffeeManager.instance.caramelstate == CoffeeManager.CaramelMacchiatoStatus.takeoutcup )
            {
                CoffeeManager.instance.DebugCoffeeState ();
            }

        }
    }
}
