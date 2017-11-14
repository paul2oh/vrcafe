using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    private Transform tr;
    private Vector3 StartPosition;
    private Quaternion StartRotation;
    // Use this for initialization

    private bool reset;

    void Start()
    {
        tr = this.gameObject.GetComponent<Transform> ();
        StartPosition = tr.position;
        StartRotation.eulerAngles = tr.rotation.eulerAngles;
    }

    private void Update()
    {

        if ( reset || transform.position.y <= 0.1 )
        {
            reset = false;
            StartCoroutine ( this.OnReset () );
        }

        //수정 17.07.19
        if ( CoffeeManager.instance.coffeestate == CoffeeManager.CoffeeStatus.customer || CoffeeManager.instance.espressostate == CoffeeManager.EspressoStatus.customer ||
              CoffeeManager.instance.lattestate == CoffeeManager.CafeLatteStatus.customer || CoffeeManager.instance.mochastate == CoffeeManager.CafeMochaStatus.customer ||
              CoffeeManager.instance.caramelstate == CoffeeManager.CaramelMacchiatoStatus.customer )
        {
            Debug.Log ( "넘어가라 넘어가" );
            reset = true;

        }
    }

    IEnumerator OnReset()
    {
        yield return new WaitForSeconds ( 2.0f );
        tr.position = StartPosition;
        tr.rotation = StartRotation;

        tr.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 ( 0 , 0 , 0 );
        tr.gameObject.GetComponent<Rigidbody> ().angularVelocity = new Vector3 ( 0 , 0 , 0 );

        if ( CoffeeManager.instance.coffeestate == CoffeeManager.CoffeeStatus.customer || CoffeeManager.instance.espressostate == CoffeeManager.EspressoStatus.customer ||
              CoffeeManager.instance.lattestate == CoffeeManager.CafeLatteStatus.customer || CoffeeManager.instance.mochastate == CoffeeManager.CafeMochaStatus.customer ||
              CoffeeManager.instance.caramelstate == CoffeeManager.CaramelMacchiatoStatus.customer )
        {
            // CoffeeManager.instance.DebugCoffeeState();

        }
    }


}