using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour {


    int rotSpeed = 100;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float cameraRot = rotSpeed * Time.smoothDeltaTime;

        //카메라 상하 회전
        float RotUp = Input.GetAxis("Mouse Y");

#if UNITY_EDITOR_WIN

            transform.Rotate(Vector3.left * cameraRot * RotUp);
#endif



    }
}
