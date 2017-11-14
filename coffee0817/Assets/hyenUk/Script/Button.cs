using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour {

    private Material met;
    private Material metTemp;
    private bool CheckBool;

    private bool CheckEnter;
    // 사운드
    private AudioSource audioSource;
    private AudioClip audioClip;


    //오른손인지 왼손인지
    private string CheckRight;

    public GameObject objects;
    // Use this for initialization
    void Start () {
        met = (Material)Resources.Load("Materials/GreenButton");
        CheckBool = false;
        metTemp = this.gameObject.GetComponent<MeshRenderer>().material;
        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioClip = (AudioClip)Resources.Load("Sounds/water");
        objects.SetActive(false);
    }

    private void Update()
    {
        //17.08.18 nolo 추가
        if ( (Input.GetButtonDown("Fire1") && CheckRight == "Left") || (Input.GetButtonDown("Fire2") && CheckRight == "Right") || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)|| NoloVR_Controller.GetDevice ( NoloDeviceType.RightController ).GetNoloButtonDown ( NoloButtonID.Trigger ))
        {
            if (CheckEnter)
            {
                if (CheckBool==false)
                {
                    if(!audioSource.isPlaying)
                    {
                        audioSource.PlayOneShot(audioClip);
                    }
                
                    isWaterBool();
                    Debug.Log("켰다");
                    objects.SetActive(true);
                    this.gameObject.GetComponentInChildren<MeshRenderer>().material = met;
                }
                else
                {
                    isWaterBool();
                    Debug.Log("껏다");
                    objects.SetActive(false);
                    this.gameObject.GetComponentInChildren<MeshRenderer>().material = metTemp;
                }
            }
        }
    }

    void OnEnter(object[] _params)
    {
        GetComponentInChildren<Text>().color = Color.green;
        CheckEnter = true;
        CheckRight = (string)_params[0];
    }

    void OnExit(object[] _params)
    {
        GetComponentInChildren<Text>().color = Color.black;
        CheckEnter = false;
    }

    void isWaterBool()
    {
        CheckBool = !CheckBool;
    }
}
