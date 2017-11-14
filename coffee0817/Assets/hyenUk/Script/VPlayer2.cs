using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Audio;


public class VPlayer2 : MonoBehaviour {

    //채널넘기는 사운드
    private AudioSource audioSource;
    private AudioClip audioClip;

    //케러셀
    public GameObject[] carousels;

    public int idx=0;
  //  int[] frameTemp = {0,450,630,800,270,1450};

    //시간 딜레이
    private bool Timer;

    //쿼드의 위치를 옴겨줄려고 
    public GameObject[] quad;
    public VideoPlayer[] Vplayer;
    public Transform quadposition;

  
    // Use this for initialization
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioClip = (AudioClip)Resources.Load("Sounds/channel");
        Timer = false;
        for (int a = 0; a < 5; a++)
        {
            quad[a].transform.position = quadposition.position + new Vector3(0, 0, -0.1f);
            Vplayer[a].Pause();
            Vplayer[a].frame = 0;
        }
        carousels[0].GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/GreenButton");
        quad[0].transform.position = quadposition.position + new Vector3(0, 0, 0);
        Vplayer[0].Play();
    }

    void ChangeVideoUp()
    {
        audioSource.PlayOneShot(audioClip);
        //쿼드 인덱스 올려줌
        if (GameManager.instance.QuadIdx<4)
        {
            GameManager.instance.QuadIdx++;
        }
        else
        {
            GameManager.instance.QuadIdx = 0;
        }
        int qudIdx = GameManager.instance.QuadIdx;
        //초기화
        for (int a = 0; a < 5; a++)
        {
            carousels[a].GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/defult");
            quad[a].transform.position = quadposition.position + new Vector3(0, 0, -0.1f);
            Vplayer[a].Pause();
            Vplayer[a].frame = 0;
        }

        //해당 인덱스 다음 케러셀의 머테리얼을 바꿔줌
        carousels[qudIdx].GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/GreenButton");


        //쿼드의 위치를 원래 위치로 옴김
        quad[qudIdx].transform.position = quadposition.position+ new Vector3(0, 0, 0);
        Vplayer[qudIdx].Play();
    }

    void ChangeVideoDown()
    {
        audioSource.PlayOneShot(audioClip);
        //저장중인 쿼드 인덱스를 받아옴

        //쿼드 인덱스 올려줌
        if (GameManager.instance.QuadIdx > 0)
        {
            GameManager.instance.QuadIdx--;
        }
        else
        {
            GameManager.instance.QuadIdx = 4;
        }

        int qudIdx = GameManager.instance.QuadIdx;
        //초기화
        for (int a = 0; a < 5; a++)
        {
            carousels[a].GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/defult");
            quad[a].transform.position = quadposition.position + new Vector3(0, 0, -0.1f);
            Vplayer[a].Pause();
            Vplayer[a].frame = 0;   
        }

        //해당 인덱스 다음 케러셀의 머테리얼을 바꿔줌
        carousels[qudIdx].GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/GreenButton");

        //쿼드의 위치를 원래 위치로 옴김
        quad[qudIdx ].transform.position = quadposition.position + new Vector3(0, 0, 0);
        Vplayer[qudIdx].Play();
    }
   
    /*
    void PageSlide()
    {
        if (idx!=5)
        {
            idx++;
        }
       
        int qudIdx = GameManager.instance.QuadIdx;
        Vplayer[qudIdx].Play();

        Vplayer[qudIdx].frame = frameTemp[idx];
        Debug.Log(frameTemp[idx]);
        Timer2 = false;
    }

    void PageSlideBack()
    {
        int qudIdx = GameManager.instance.QuadIdx;
        Vplayer[qudIdx].Play();
        if (idx!=0)
        {
            idx--;
            Vplayer[qudIdx].frame = frameTemp[idx];
        }
        else
        {
            Vplayer[qudIdx].frame = 0;
        }
        Timer2 = false;
    }
    */
    IEnumerator TimeBool()
    {
        yield return new WaitForSeconds(1.0f);
        Timer = false;
    }

    /*
    IEnumerator TimeBool2()
    {
        yield return new WaitForSeconds(1.0f);
        Timer2 = false;
    }
    */
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.OnTV)
        {
            if ( /*Input.GetAxis("Mouse Y") > 1.6 ||*/ (OVRInput.Get(OVRInput.Button.PrimaryTouchpad) && OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad).y > 0.5) 
                ||( NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.TouchPad) && NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetAxis().y> 0.5))
            {
                if (Timer == false)
                {
                    Debug.Log("change");
                    Timer = true;
                    StartCoroutine(this.TimeBool());
                    ChangeVideoUp();
                }
            }

            if (/*Input.GetAxis("Mouse Y") < -1.6 ||*/ (OVRInput.Get(OVRInput.Button.PrimaryTouchpad) && OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad).y < -0.5)
                  || (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.TouchPad) && NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetAxis().y < -0.5))
            {
                if (Timer == false)
                {
                    Debug.Log("change");
                    Timer = true;
                    StartCoroutine(this.TimeBool());
                    ChangeVideoDown();
                }
            }


            /*
            if (Input.GetAxis("Mouse X") > 1.6 || Input.GetAxis("Oculus_GearVR_DpadX") > 0.6 || NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetAxis().x > 0.6 || Input.GetKeyDown(KeyCode.X))
            {
                if (Timer == false)
                {
                    Debug.Log("PageSlide");
                    Timer = true;
                    StartCoroutine(this.TimeBool());
                    PageSlide();
                }
            }

            if (Input.GetAxis("Mouse X") < -1.6 || Input.GetAxis("Oculus_GearVR_DpadX") < -0.6 || NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetAxis().x < -0.6 || Input.GetKeyDown(KeyCode.Z))
            {
                if (Timer == false)
                {
                    Debug.Log("PageSlide");
                    Timer = true;
                    StartCoroutine(this.TimeBool());
                    PageSlideBack();
                }
            }
            */
        }

    }
}

