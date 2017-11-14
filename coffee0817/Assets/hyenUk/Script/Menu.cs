using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    //한글 모드인지 체크
    int modeIdx = 0;

    private AudioSource audiosource;
    private AudioClip menusound;
    private AudioClip menuchangesound;
    private AudioClip menumovesound;

    private bool click;
    private float time;
    private Canvas menu;
    private int idx;

    public Image[] menuImage;
    public Color[] TempColor;
    // Use this for initialization
    void Start()
    {
        audiosource = this.gameObject.GetComponent<AudioSource>();

        menusound = (AudioClip)Resources.Load("Sounds/menu");
        menuchangesound = (AudioClip)Resources.Load("Sounds/menuchange");
        menumovesound = (AudioClip)Resources.Load("Sounds/channel");
        click = false;
        time = 0.0f;
        menu = this.gameObject.GetComponent<Canvas>();
        menu.enabled = false;
        idx = 0;
        menuImage = menu.GetComponentsInChildren<Image>();
        for (int a = 0; a < menuImage.Length; a++)
        {
            TempColor[a] = menuImage[a].color;
        }

    }

    // Update is called once per frame
    void Update()
    {

        this.transform.forward = Camera.main.transform.forward;

        //더블클릭시 메뉴 활성화/비활성화
        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonDown(NoloButtonID.Trigger) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.Space))
        {
            if (click == true && Time.time < time + 0.2f)
            {
                Debug.Log("더블클릭");

                audiosource.PlayOneShot(menusound);
                ChangeEnable();
            }
            else
            {
                click = false;
            }
        }

        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonUp(NoloButtonID.Trigger) || OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("클릭업");
            click = true;
            time = Time.time;
        }

        if (menu.enabled)
        {
           
            //메뉴가 활성화 된 이후 트리거를 눌렀을때
            if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonDown(NoloButtonID.Trigger) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.Space))
            {
                audiosource.PlayOneShot(menuchangesound);
                if (idx == 1)
                {
                    StartCoroutine(this.restart());
                    Debug.Log("Restart ");
                }
                else if (idx == 2)
                {
                    Debug.Log("Pause ");
                 //   Time.timeScale = 0;
                }
                else if (idx == 3)
                {
                    Debug.Log("Eng ");

                    if(!GameManager.instance.kor)
                    {
                        menuImage[idx].GetComponentInChildren<Text>().text = "Kor";
                        GameManager.instance.kor = !GameManager.instance.kor;
                    }
                    else
                    {
                        menuImage[idx].GetComponentInChildren<Text>().text = "Eng";
                        GameManager.instance.kor = !GameManager.instance.kor;
                    }
                   
                    //   Time.timeScale = 0;
                }
                else if (idx == 4)
                {
                    Debug.Log("ModeChange ");
                    if (GameManager.instance.tuto)
                    {
                        menuImage[idx].GetComponentInChildren<Text>().text = "Normal";
                        GameManager.instance.tuto = !GameManager.instance.tuto;
                    }
                    else
                    {
                        menuImage[idx].GetComponentInChildren<Text>().text = "Tutorial";
                        GameManager.instance.tuto = !GameManager.instance.tuto;
                    }

                    //   Time.timeScale = 0;
                }
                else if (idx == 5)
                {
                    Debug.Log("Exit ");
                    audiosource.PlayOneShot(menusound);
                    ChangeEnable();
                }
            }

         
            //터치패드로 메뉴 움직이기
            if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad) || Input.GetKey(KeyCode.V)||NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonUp(NoloButtonID.Trigger))
            {
                if (OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad).y < -0.5 || NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetAxis().y < -0.5 || Input.GetKeyDown(KeyCode.C))
                {
                    idx++;
                    if (idx == menuImage.Length)
                    {
                        idx = 1;
                    }
                    Debug.Log("메뉴 다운 : " + idx);
                    ChangeImage();
                }
                else if (OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad).y > 0.5 || NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetAxis().y > 0.5 || Input.GetKeyDown(KeyCode.B))
                {
                    idx--;
                    if (idx <= 0)
                    {
                        idx = menuImage.Length-1;
                    }
                    Debug.Log("메뉴 업 : " + idx);
                    ChangeImage();
                }
            }
        }
    }

    IEnumerator restart()
    {

        yield return new WaitForSeconds(2.0f);

            Debug.Log("재시작");
            SceneManager.LoadScene("play");

    }

    void ChangeImage()
    {
        for (int a = 0; a < menuImage.Length; a++)
        {
            menuImage[a].color = TempColor[a];
        }
        audiosource.PlayOneShot(menumovesound);

        menuImage[idx].color = Color.red;
    }


    void ChangeEnable()
    {
        menu.enabled = !menu.enabled;

        if (menu.enabled)
        {
            idx = 0;

            for (int a = 0; a < menuImage.Length; a++)
            {
                menuImage[a].color = TempColor[a];
            }
        }
    }
}
