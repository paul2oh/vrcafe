using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potafillter : MonoBehaviour {

    private Transform PotaPosition;

    //포타포지션과 충돌 했으면
    private bool PotaInto;

    //포타 포지션에 위치해 있으면
    private bool PotaIn;

    // Use this for initialization
    void Start () {
        PotaInto = false;
        PotaIn = false;
        PotaPosition = GameObject.Find("PotaPosition").transform;
    }

    //state 추가 2017.07.14
    void OnInto()
    {
        if (PotaInto)
        {
            if ( CoffeeManager.instance.coffeestate == CoffeeManager.CoffeeStatus.temper || CoffeeManager.instance.espressostate == CoffeeManager.EspressoStatus.temper ||
              CoffeeManager.instance.lattestate == CoffeeManager.CafeLatteStatus.temper || CoffeeManager.instance.mochastate == CoffeeManager.CafeMochaStatus.temper||
              CoffeeManager.instance.caramelstate == CoffeeManager.CaramelMacchiatoStatus.temper )
            {
                transform.GetComponent<Rigidbody> ().isKinematic = true;
                transform.GetComponent<Rigidbody> ().useGravity = false;

                transform.position = PotaPosition.position + new Vector3 ( -0.02f , -0.02f , 0 );
                transform.rotation = Quaternion.Euler ( 0f , 40f , 0f ); // 90 0 0   132 48 60

                PotaIn = true;

            }


        }
    
    }

    //커피머신과 접촉이 감지되면 Pota 장착 위치로 이동
    //추가 2017.07.14
    private void OnTriggerEnter(Collider col)
    {
        if (col.name == "PotaPosition")//&& point == 4
        {
            PotaInto = true;
        }
        else if (col.name=="box")
        {
            if ( CoffeeManager.instance.coffeestate == CoffeeManager.CoffeeStatus.grinder || CoffeeManager.instance.espressostate == CoffeeManager.EspressoStatus.grinder ||
              CoffeeManager.instance.lattestate == CoffeeManager.CafeLatteStatus.grinder || CoffeeManager.instance.mochastate == CoffeeManager.CafeMochaStatus.grinder ||
              CoffeeManager.instance.caramelstate == CoffeeManager.CaramelMacchiatoStatus.grinder )
            {
                CoffeeManager.instance.DebugCoffeeState ();

            }
        }

        else if (col.name == "Temper")
        {
            if ( CoffeeManager.instance.coffeestate == CoffeeManager.CoffeeStatus.portafilter || CoffeeManager.instance.espressostate == CoffeeManager.EspressoStatus.portafilter ||
              CoffeeManager.instance.lattestate == CoffeeManager.CafeLatteStatus.portafilter || CoffeeManager.instance.mochastate == CoffeeManager.CafeMochaStatus.portafilter ||
              CoffeeManager.instance.caramelstate == CoffeeManager.CaramelMacchiatoStatus.portafilter )
            {
                CoffeeManager.instance.DebugCoffeeState ();
            }
        }

    }

    private void OnTriggerStay(Collider col)
    {
        if (PotaIn && (col.gameObject.name == "GearVrController" || col.gameObject.name == "controller"))
        {
            transform.Rotate(Vector3.down * Time.deltaTime * 10);
            if (transform.rotation.eulerAngles.y >= 220.0f)
            {
                CoffeeManager.instance.DebugCoffeeState();
                PotaIn = false;
            }
        }
    }


    /*
        //잡았을때 위치조정을 위한 함수
    void FixPosition()
    {
        transform.position += new Vector3(-0.2f, 0.02f, -0.03f);
    }

    void ReFixPosition()
    {
        transform.position += new Vector3(0.2f, -0.02f, 0.03f);
    }
    */
    private void OnTriggerExit(Collider col)
    {
        if (col.name == "PotaPosition")
        {
            PotaInto = false;
        }
    }
}
