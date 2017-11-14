using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOutCupPosition : MonoBehaviour {

    public Transform TakeOutPosition;

    private bool CupInto;
    
    // Use this for initialization
    void Start()
    {
        CupInto = false;
    }

    void OnInto()
    {
        if (CupInto)
        {
            Debug.Log("테이크아웃컵 위치 확인");
            Debug.Log(CoffeeManager.instance.coffeestate);
            //수정 2017.07.14
            if ( CoffeeManager.instance.coffeestate == CoffeeManager.CoffeeStatus.takeoutcup)
            {
                Debug.Log("테이크아웃컵 위치 확인");
             //   CoffeeManager.instance.DebugCoffeeState();
                transform.GetComponent<Rigidbody>().isKinematic = true;
                transform.GetComponent<Rigidbody>().useGravity = false;

                transform.position = TakeOutPosition.position;
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }

    //커피머신과 접촉이 감지되면 에스프레소 컵 장착 위치로 이동
    private void OnTriggerEnter(Collider col)
    {
        if (col.name == "TakeOutPosition")
        {
            Debug.Log("들어감");
            CupInto = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.name == "TakeOutPosition")
        {
            CupInto = false;
        }
    }
}