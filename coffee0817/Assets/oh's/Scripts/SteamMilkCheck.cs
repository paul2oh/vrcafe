using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamMilkCheck : MonoBehaviour
{



    private void OnTriggerEnter( Collider col )
    {
        if ( col.gameObject.name == "TakeOutCup" )
        {
            Debug.Log ( "잔들어옴" );
            if (CoffeeManager.instance.lattestate == CoffeeManager.CafeLatteStatus.takeoutcup || CoffeeManager.instance.mochastate == CoffeeManager.CafeMochaStatus.takeoutcup ||
              CoffeeManager.instance.caramelstate == CoffeeManager.CaramelMacchiatoStatus.vanillasyrup )
            {
                CoffeeManager.instance.DebugCoffeeState ();
            }
        }
    }

}
