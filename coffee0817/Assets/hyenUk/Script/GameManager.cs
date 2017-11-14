using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


    // 사운드
    private AudioSource audioSource;
    private AudioClip suc;
    private AudioClip fail;

    //catch를 위한 list 
    public List<GameObject> catchObject = new List<GameObject>();

    //튜토리얼을 껏다켰다 하기위한 함수
    public bool tuto = true;

    // 한글인지 아닌지
    public bool kor = false;

    //결과창
    public string[] Result;
    public int Score;
    public int ResultInx;

    public Canvas ResultCanvas;

    public bool isGamePlay;

    public Text[] ResultText = new Text[13];

    //손에 잡힌 물건
    public GameObject nearObjectLeft;
    public GameObject nearObjectRight;

    //티비 보여지는 쿼드인덱스 저장
    public int QuadIdx;
    

    //TV근처에 손이 있는지 체크
    public bool OnTV;

    //손님 모델들
    public GameObject[] characters;

    //싱글턴
    public static GameManager instance = null;

    //손과의 거리를 구하기 위한 손의 위치 
    public Transform handPosition;

    //손님
    public GameObject Customer;

    //주문할때 뜨는 말풍선
    public Canvas order;


    //튜토리얼 표시를 위한 오브젝트
    public GameObject TutorialMaterialObj;
    public GameObject TutorialMaterialObj2;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        QuadIdx = 0;
        handPosition = GameObject.FindGameObjectWithTag("RightHand").transform;
        Customer = GameObject.FindGameObjectWithTag("Customer");
        order = Customer.GetComponentInChildren<Canvas>();


        audioSource = this.gameObject.GetComponent<AudioSource>();
        suc = (AudioClip)Resources.Load("Sounds/congratulation");
        fail = (AudioClip)Resources.Load("Sounds/fail");

        order.enabled = false;
        ResultInx = 0;
        Result = new string[11];
        Score = 0;
        OnTV = false;
        for (int a=0; a<Result.Length;a++)
        {
            Result[a] = "";
        }

        StartCoroutine(this.redObj());
        StartCoroutine(this.TutorialMaterialColl());

        ResultText = ResultCanvas.GetComponentsInChildren<Text>();
        ResultCanvas.enabled = false;

        isGamePlay = true;
    }


    //튜토리얼 메터리얼을 지속적으로 콜해준다 
    IEnumerator TutorialMaterialColl()
    {
        yield return new WaitForSeconds(1.0f);
        if(TutorialMaterialObj!=null)
        {
            TutorialMaterialObj.SendMessage("OnTutorial", SendMessageOptions.DontRequireReceiver);
        }
        if (TutorialMaterialObj2 != null)
        {
            TutorialMaterialObj2.SendMessage("OnTutorial", SendMessageOptions.DontRequireReceiver);
        }

        yield return new WaitForSeconds(4.0f);

        StartCoroutine(this.TutorialMaterialColl());
    }


    //캐치에서 사용하는 함수로 가장가까운 오브젝트를 레드머테리얼로 바꿈
    IEnumerator redObj()
    {
        int nearObjIdx = 0;
        if (catchObject.Count != 0)
        {
            for (int a = 0; a < catchObject.Count; a++)
            {
                catchObject[a].GetComponentInChildren<MeshRenderer>().material = catchObject[a].GetComponent<Catch>().metTemp;
                GameObject obj = catchObject[a];
                if (Vector3.Distance(handPosition.position, obj.GetComponent<Transform>().position) <= Vector3.Distance(handPosition.position, catchObject[nearObjIdx].GetComponent<Transform>().position))
                {
                    nearObjIdx = a;
                }
            }
            catchObject[nearObjIdx].GetComponentInChildren<MeshRenderer>().material = catchObject[nearObjIdx].GetComponent<Catch>().met;
        }
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(this.redObj());
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (ResultInx ==10 && isGamePlay)
        {
            Debug.Log("결과");
            ResultCanvas.enabled = true;
            ResultCanvas.GetComponent<CanvasScaler>().dynamicPixelsPerUnit = 2;
            for (int a=0; a<10; a++)
            {
                ResultText[a+1].text = Result[a];
            }

            if (Score>=6)
            {
                Debug.Log("통과");
                audioSource.PlayOneShot(suc);
                ResultText[12].text ="Success : "+Score+" >>> Pass";
                isGamePlay = false;
            }
           else
            {
                Debug.Log("실패");
                audioSource.PlayOneShot(fail);
                ResultText[12].text = "Success : " + Score + " >>> fail";
                isGamePlay = false;
            }
        }
    }
}
