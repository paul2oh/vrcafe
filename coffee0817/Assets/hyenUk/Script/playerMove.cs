using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour {


    int rotSpeed = 220;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        float playerRot = rotSpeed * Time.smoothDeltaTime;

        //플레이어 좌우 회전
           float RotRight = Input.GetAxis("Mouse X");


#if UNITY_EDITOR_WIN
            transform.Rotate(Vector3.up * playerRot * RotRight);
#endif
    }
}
