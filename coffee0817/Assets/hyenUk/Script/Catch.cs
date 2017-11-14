using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catch : MonoBehaviour {
    //손과의 거리를 구하기 위한 손의 위치 
    public Transform handPosition;

    public Material met;

    public Material metTemp;

    //오른손인지 왼손인지
    private string CheckRight;

    private Vector3 TemperPosition;

    // Use this for initialization
    void Start () {
        handPosition = GameObject.FindGameObjectWithTag("RightHand").transform;
        met = (Material)Resources.Load("Materials/RightHandNear");
        metTemp = this.gameObject.GetComponentInChildren<MeshRenderer>().material;
    }
	
    void OnEnter(object[] _params)
    {
        GameManager.instance.catchObject.Add(this.gameObject);

     
        CheckRight =(string)_params[0];
    }

    void OnExit(object[] _params)
    {
        this.gameObject.GetComponentInChildren<MeshRenderer>().material = metTemp;
        GameManager.instance.catchObject.Remove(this.gameObject);
    }
}
