using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspressoCupPosition : MonoBehaviour
{
    private Transform EesoCupPosition;

    private bool CupInto;
    // Use this for initialization
    void Start()
    {
        CupInto = false;
        EesoCupPosition = GameObject.Find("EspressoPosition").transform;
    }

    void OnInto()
    {
        if (CupInto)
        {

            if ( CoffeeManager.instance.coffeestate == CoffeeManager.CoffeeStatus.potaposition || CoffeeManager.instance.espressostate == CoffeeManager.EspressoStatus.potaposition ||
              CoffeeManager.instance.lattestate == CoffeeManager.CafeLatteStatus.potaposition || CoffeeManager.instance.mochastate == CoffeeManager.CafeMochaStatus.potaposition ||
              CoffeeManager.instance.caramelstate == CoffeeManager.CaramelMacchiatoStatus.potaposition )
            {
                CoffeeManager.instance.DebugCoffeeState();
                transform.GetComponent<Rigidbody>().isKinematic = true;
                transform.GetComponent<Rigidbody>().useGravity = false;

                //잡힌 오브젝트가 충돌하지 않도록 
                BoxCollider[] BCobj = GetComponentsInChildren<BoxCollider>();
                if (BCobj != null)
                {
                    for (int a = 0; a < BCobj.Length; a++)
                    {
                        BCobj[a].enabled = false;
                    }
                }

                SphereCollider[] SCobj = GetComponentsInChildren<SphereCollider>();
                if (SCobj != null)
                {
                    for (int a = 1; a < SCobj.Length; a++)
                    {
                        SCobj[a].enabled = false;
                    }
                }

                CapsuleCollider[] CCobj = GetComponentsInChildren<CapsuleCollider>();
                if (CCobj != null)
                {
                    for (int a = 0; a < CCobj.Length; a++)
                    {
                        CCobj[a].enabled = false;
                    }
                }


                //커피머신의 충돌 박스 위치로 이동
                transform.position = EesoCupPosition.position + new Vector3(-0.06f, -0.01f, 0);// new Vector3(-1.172f, 1.595f, 0.605f);
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                CupInto = false;
            }
        }
    }

    //잡았을때 위치조정을 위한 함수
    void FixPosition()
    {
        
    }

    //커피머신과 접촉이 감지되면 에스프레소 컵 장착 위치로 이동
    private void OnTriggerEnter(Collider col)
    {
        if (col.name == "EspressoPosition")
        {
            CupInto = true;
        }
        else if (col.name == "TakeOutCup")
        {
            if ( CoffeeManager.instance.coffeestate == CoffeeManager.CoffeeStatus.espresso || CoffeeManager.instance.espressostate == CoffeeManager.EspressoStatus.espresso ||
              CoffeeManager.instance.lattestate == CoffeeManager.CafeLatteStatus.espresso || CoffeeManager.instance.mochastate == CoffeeManager.CafeMochaStatus.espresso ||
              CoffeeManager.instance.caramelstate == CoffeeManager.CaramelMacchiatoStatus.espresso )
            {
                CoffeeManager.instance.DebugCoffeeState ();
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.name == "EspressoPosition")
        {
            CupInto = false;
        }
    }
}
