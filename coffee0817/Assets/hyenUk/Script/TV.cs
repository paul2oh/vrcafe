using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnEnter(object[] _params)
    {
        GameManager.instance.OnTV = true;
    }

    void OnExit(object[] _params)
    {
        GameManager.instance.OnTV = false;
    }

    // Update is called once per frame
    void Update () {
      //  this.transform.forward = -Camera.main.transform.forward;
    }
}
