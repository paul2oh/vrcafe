using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collCustomer : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (CoffeeManager.instance.coffeestate == CoffeeManager.CoffeeStatus.americano || CoffeeManager.instance.espressostate == CoffeeManager.EspressoStatus.takeoutcup ||
             CoffeeManager.instance.lattestate == CoffeeManager.CafeLatteStatus.steammilk || CoffeeManager.instance.mochastate == CoffeeManager.CafeMochaStatus.whippedcream ||
             CoffeeManager.instance.caramelstate == CoffeeManager.CaramelMacchiatoStatus.caramelsauce)
        {
            CoffeeManager.instance.DebugCoffeeState();
            other.gameObject.SendMessage("collCustomer", SendMessageOptions.DontRequireReceiver);
        }
    }
}
