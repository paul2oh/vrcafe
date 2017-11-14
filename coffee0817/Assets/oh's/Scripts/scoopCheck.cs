using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//그라인더 박스에 커피생성했는지 확인하고 포타필터를 박스에 집어 넣으면 포타필터에도 커피생성하기
public class scoopCheck : MonoBehaviour {
    public checkHandle ckHandle;
    public coffeeCheck coCheck;
    public MeshRenderer coffeePowder;
    public ActivePowder activePowder;

    // Use this for initialization
    void Start () {
        ckHandle = GameObject.Find ( "checkHandle" ).GetComponent<checkHandle> ();
        coCheck = GameObject.Find ( "Potafillter" ).GetComponent<coffeeCheck> ();
        coffeePowder = GameObject.FindWithTag ( "coffeePowder" ).GetComponent<MeshRenderer> ();
        coffeePowder.enabled = false;
        activePowder = GameObject.FindWithTag ("boxIntheCoffee").GetComponent<ActivePowder> ();
        StartCoroutine ( this.potafilterCoffeeCheck () );

    }
    IEnumerator potafilterCoffeeCheck()
    {
        yield return new WaitForSeconds ( 0.2f );
        //포타필터의 커피 메쉬렌더러 켜기
        if ( activePowder.powderBool == true && coCheck.isCoffeeScoop == true )
        {
            coffeePowder.enabled = true;
        }
        //템퍼와 포터필터가 trigger 하면 포타필터의 메쉬렌더러 끄기
        else if ( coCheck.isTemperAction == true )
        {
            coffeePowder.enabled = false;
        }
        StartCoroutine ( potafilterCoffeeCheck () );
    }

}
