using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//boxcheck의 isbox가 true일때만 powder 생성
public class checkHandle : MonoBehaviour
{
    public Transform CoffeeBoxPosition;
    private GameObject handle;
    private int handleIntCheck;
    public MeshRenderer powder;
    bool checkPowder;
    public bool boxcheck;
    public boxCheck isbox;

    void Awake()
    {
        isbox = GameObject.Find("forCheckBox").GetComponent<boxCheck>();
        powder = GameObject.Find("Landscape").GetComponent<MeshRenderer>();
        powder.enabled = false;
        checkPowder = powder.enabled;
        handle = GameObject.Find("Sphere02").GetComponent<GameObject>();
        handleIntCheck = 0;
    }

    void Update()
    {
        boxcheck = isbox.isBox;

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Sphere02")
        {
            if ( CoffeeManager.instance.coffeestate == CoffeeManager.CoffeeStatus.Beans || CoffeeManager.instance.espressostate == CoffeeManager.EspressoStatus.Beans ||
              CoffeeManager.instance.lattestate == CoffeeManager.CafeLatteStatus.Beans || CoffeeManager.instance.mochastate == CoffeeManager.CafeMochaStatus.chocolatesauce ||
              CoffeeManager.instance.caramelstate == CoffeeManager.CaramelMacchiatoStatus.caramelsyrup )
            {
                handleIntCheck += 1;
            }

            if (handleIntCheck >= 1 && boxcheck)
            {
                powder.SendMessage("Powder", checkPowder, SendMessageOptions.DontRequireReceiver);
                Debug.Log("커피가루생성");
                //수정 2017.07.17
               
                CoffeeManager.instance.DebugCoffeeState();
                handleIntCheck = 0;

            }
        }
    }
}