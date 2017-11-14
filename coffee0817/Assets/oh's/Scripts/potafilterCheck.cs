using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potafilterCheck : MonoBehaviour
{

    public bool isPotafilterScoop;
    public checkHandle ckHandle;
    public bool isBoxInThePowder;
    public MeshRenderer showPotaCoffee;

    // Use this for initialization
    void Start()
    {
        isPotafilterScoop = false;
        ckHandle = GameObject.Find ( "checkHandle" ).GetComponent<checkHandle> ();
        //StartCoroutine ( this.potafilterCoffeeCheck () );
        showPotaCoffee = GameObject.FindWithTag ( "coffeePowder" ).GetComponent<MeshRenderer> ();
    }

    void showCoffee()
    {
        if ( isPotafilterScoop )
        {
            Debug.Log ( "포타필터에 커피생성 호출" );
            showPotaCoffee.SendMessage ( "showPowder" , SendMessageOptions.DontRequireReceiver );
        }
    }


    void OnTriggerEnter( Collider col )
    {
        if ( col.name == "Potafillter" )
        {
            Debug.Log ( "포타필터 들어옴" );
            isPotafilterScoop = true;
        }
    }
    //IEnumerator potafilterCoffeeCheck()
    //{
    //    yield return new WaitForSeconds ( 0.2f );
    //    isBoxInThePowder = ckHandle.checkPowder;
    //    Debug.Log ( isBoxInThePowder );
    //    if ( isBoxInThePowder && isPotafilterScoop )
    //    {
    //        Debug.Log ( "포타필터에 커피생성 하도록 호출" );
    //        powder.SendMessage ( "showPowder" , SendMessageOptions.DontRequireReceiver );
    //    }
    //    StartCoroutine ( potafilterCoffeeCheck () );

    //}
}
