using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1.오브젝트에 손이 들어왔는지 확인해보고 
//2.Liquid 오브젝트를 찾아서 
//3.현재 오브젝트의 자식으로 붙이기

public class actionLiquid : MonoBehaviour {
    //파티클 오브젝트 위치
    public Transform liquid;

    public ParticleSystem liquidAction;
    //현재 오브젝트 위치
    public Transform presentObj;

    //빈 오브젝트 위치
    public Transform emptyObj;

    //손안에 있는지 확인
    public bool isHandIn;
    //손의 각도가 60 이나 -60인지 확인
    public bool isDegreeRightAngle;

	// Use this for initialization
	void Start () {
        isHandIn = false;
        liquid = GameObject.Find ( "Liquid" ).GetComponent<Transform> ();
        presentObj = this.gameObject.transform.Find ( "LiquidPoint" ).GetComponent<Transform>();
        emptyObj = GameObject.Find ( "EmptyPoint" ).GetComponent<Transform> ();
        liquidAction = liquid.gameObject.GetComponent<ParticleSystem> ();
        StartCoroutine ( this.checkBool () );
        StartCoroutine ( activeLiquid () );
    }


    IEnumerator checkBool()
    {
        yield return new WaitForSeconds ( 0.2f );
        //Debug.Log ( "checkbool" );
        checkDegree chkDegree = GameObject.FindWithTag ( "RightHand" ).GetComponent<checkDegree> ();
        isDegreeRightAngle = chkDegree.isDegreeRight;
        StartCoroutine ( checkBool () );

    }
    //오브젝트에 손이 들어왔는지 확인
    void OnTriggerEnter(Collider col )
    {
        if ( col.tag == "RightHand" ||col.tag == "LeftHand" )
        {
           // Debug.Log ( "손 감지됨" );
            isHandIn = true;
           
        }
    }

    void OnTriggerExit(Collider col )
    {
        if ( col.tag == "RightHand" || col.tag == "LeftHand" )
        {
           // Debug.Log ( "손 뗀다" );
            isHandIn = false;

        }
    }
    //파티클을 현재 오브젝트의 하위에 붙이기
    IEnumerator activeLiquid() {
        yield return new WaitForSeconds ( 0.2f );
        if ( isHandIn && isDegreeRightAngle )
        {
            liquid.transform.SetParent ( presentObj );
            liquid.transform.localPosition = new Vector3 ( 0 , 0 , 0 );

        }
        else if ( !isHandIn && !isDegreeRightAngle )
        {
            liquid.transform.SetParent ( emptyObj );
            liquid.transform.localPosition = new Vector3 ( 0 , 0 , 0 );


        }
        StartCoroutine ( activeLiquid () );

    }
}
