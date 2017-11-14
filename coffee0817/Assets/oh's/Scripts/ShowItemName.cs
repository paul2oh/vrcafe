using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//trigger check되면 미리 생성해둔 itemName canvas의 transform위치를 스크립트가 붙은 오브젝트의 transform으로 변경
public class ShowItemName : MonoBehaviour
{

    private string CheckRight;
    private Transform target;
    private bool enter;
    public Canvas can;
    // Use this for initialization
    void Start()
    {
        target = GameObject.FindWithTag ( "MainCamera" ).GetComponent<Transform> ();
        can = GameObject.Find ( "ItemName" ).GetComponent<Canvas> ();
    }

    // itemname을 가져와서 현재 오브젝트의 하위에 넣기
    void OnEnter( object [] _params )
    {
        can.GetComponentInChildren<Text> ().text = this.gameObject.name;
        can.transform.SetParent (this.gameObject.transform);
        //can.transform.parent = this.gameObject.transform;
        if ( this.gameObject.name == "Temper" )
        {
            can.transform.localPosition = new Vector3 ( 0f , 0.15f , 0f );
            can.transform.localScale = new Vector3 ( 0.5f , 0.3f , 1f );

        }
        else
        {
            can.transform.localPosition = new Vector3 ( 0f , 0.81f , 0f );
            can.transform.localScale = new Vector3 ( 0.5f , 0.5f , 1f );
        }

        can.GetComponent<CanvasScaler> ().dynamicPixelsPerUnit = 35;
        can.transform.LookAt ( target );
        CheckRight = (string) _params [ 0 ];
    }


}