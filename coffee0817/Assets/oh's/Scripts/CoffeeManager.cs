using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

//커피 주문 들어왔을때 menual text 바꿔주기
public class CoffeeManager : MonoBehaviour
{
    //메뉴 하나 클리어마다 소리나게
    private AudioSource audioSource;
    private AudioClip audioClip;

    //tutoral을 위한 오브젝트 선언 부
    public GameObject Grinder;
    public GameObject Potafillter;
    public GameObject Temper;
    public GameObject CoffeeMachine;
    public GameObject EspressoCup;
    public GameObject CaramelSauce;
    public GameObject CaramelSyrup;
    public GameObject ChocoSauce;
    public GameObject Milk;
    public GameObject MilkPitcher;
    public GameObject VanillaSyrup;
    public GameObject WhippingCream;
    public GameObject TakeOutCup;
    public GameObject collCustomer;
    public GameObject GrinderBox;
    public GameObject Coffeebutton;
    public GameObject WaterButton;
    public GameObject PotaPosition;
    public GameObject CupPosition;


    public Text [] Step;
    //0 = espresso , 1 = americano, 2= latte, 3= mocha, 4= caramel
    public int coffeeKind = 0;
    public Canvas menu;
    public static CoffeeManager instance = null;
    private float r = 248/255f, g = 95/255f, b = 95/255f;
    private bool isTextKor;

    //americano 11
    public enum CoffeeStatus { Beans, grinder, portafilter, temper, potaposition, cupposition, espresso, takeoutcup, americano, customer , defult};
    public string [] americano = { "Americano" , "grinder" , "portafilter" , "temper" , "potaposition" , "cupposition" , "espresso" , "takeoutcup" , "americano" , "customer" };
    public string [] americanoKO = { "아메리카노" , "그라인더  핸들  1회  회전" , "포타필터에  커피담기" , "템퍼로  꾹꾹  누르기" , "커피머신에 포타필터 장착하기" , "컵을 커피머신에 올려놓기" , "에스프레소 추출 버튼 누르기" , "테이크아웃잔을 커피머신에 올려놓기" , "물 버튼 누르기" , "주문 테이블에 테이크아웃잔 올리기" };

    //espresso 10
    public enum EspressoStatus { Beans, grinder, portafilter, temper, potaposition, cupposition, espresso, takeoutcup, customer, defult }
    public string [] espresso = { "Espresso" , "grinder" , "portafilter" , "temper" , "potaposition" , "cupposition" , "espresso" , "takeoutcup" , "customer" };
    public string [] espressoKO = { "에스프레소" , "그라인더 핸들 1회 회전" , "포타필터에 커피담기" , "템퍼로 꾹꾹 누르기" , "커피머신에 포타필터 장착하기" , "컵을 커피머신에 올려놓기" , "에스프레소 추출 버튼 누르기" , "테이크아웃잔을 커피머신에 올려놓기" , "주문 테이블에 테이크아웃잔 올리기" };

    //cafelatte 11
    public enum CafeLatteStatus { Beans, grinder, portafilter, temper, potaposition, cupposition, espresso, takeoutcup, steammilk, customer, defult }
    public string [] latte = { "Cafe Latte" , "grinder" , "portafilter" , "temper" , "potaposition" , "cupposition" , "espresso" , "takeoutcup" , "steammilk" , "customer" };
    public string [] latteKO = { "카페라떼" , "그라인더 핸들 1회 회전" , "포타필터에 커피담기" , "템퍼로 꾹꾹 누르기" , "커피머신에 포타필터 장착하기" , "컵을 커피머신에 올려놓기" , "에스프레소 추출 버튼 누르기" , "테이크아웃잔을 커피머신에 올려놓기" , "테이크아웃 잔에 스팀밀크 넣기" , "주문 테이블에 테이크아웃잔 올리기" };

    //cafemocha 13
    public enum CafeMochaStatus { Beans, chocolatesauce, grinder, portafilter, temper, potaposition, cupposition, espresso, takeoutcup, steammilk, whippedcream, customer, defult }
    public string [] mocha = { "Cafe Mocha" , "chocolatesauce" , "grinder" , "portafilter" , "temper" , "potaposition" , "cupposition" , "espresso" , "takeoutcup" , "steammilk" , "whippedcream" , "customer" };
    public string [] mochaKO = { "카페모카" , "테이크아웃잔에 초콜릿소스 넣기" , "그라인더 핸들 1회 회전" , "포타필터에 커피담기" , "템퍼로 꾹꾹 누르기" , "커피머신에 포타필터 장착하기" , "컵을 커피머신에 올려놓기" , "에스프레소 추출 버튼 누르기" , "테이크아웃잔을 커피머신에 올려놓기" , "테이크아웃 잔에 스팀밀크 넣기" , "테이크아웃 잔에 휘핑크림 넣기" , "주문 테이블에 테이크아웃잔 올리기" };

    //caramelmacchiato 14
    public enum CaramelMacchiatoStatus { Beans, vanillasyrup, steammilk, caramelsyrup, grinder, portafilter, temper, potaposition, cupposition, espresso, takeoutcup, caramelsauce, customer, defult }
    public string [] macchiato = { "Caramel Macchiato" , "vanillasyrup" , "steammilk" , "caramelsyrup" , "grinder" , "portafilter" , "temper" , "potaposition" , "cupposition" , "espresso" , "takeoutcup" , "caramelsauce" , "customer" };
    public string [] macchiatoKO = { "카라멜마끼아또" , "테이크아웃잔에 바닐라시럽넣기" , "테이크아웃잔에 스팀밀크 넣기" , "테이크아웃잔에 카라멜시럽넣기" , "그라인더 핸들 1회 회전" , "포타필터에 커피담기" , "템퍼로 꾹꾹 누르기" , "커피머신에 포타필터 장착하기" , "컵을 커피머신에 올려놓기" , "에스프레소 추출 버튼 누르기" , "테이크아웃잔을 커피머신에 올려놓기" , "테이크아웃잔에 카라멜소스넣기" , "주문 테이블에 테이크아웃잔 올리기" };

   // public string [] defaultText = { "" , "" , "" , "" , "" , "" , "" , "" , "" , "" , "" , "" , "" };

    public CoffeeStatus coffeestate;
    public EspressoStatus espressostate;
    public CafeLatteStatus lattestate;
    public CafeMochaStatus mochastate;
    public CaramelMacchiatoStatus caramelstate;
    /*
    CoffeeStateValue == 0  이면 기본 커피콩 상태   -- Beans
    CoffeeStateValue == 1  이면 그라인더에서 갈아서 가루가 된 상태 --grinder
    CoffeeStateValue == 2  이면 커피가루를 포타필터에 담은 상태  --portafilter
    CoffeeStateValue == 3  이면 포타필터에서 탬퍼를 사용하여 평평하게 만든 상태 --temper
    CoffeeStateValue == 4  이면 포타필터를 끼운 상태 potaposition
    CoffeeStateValue == 5  이면 에스프레소 컵을 제자리에 놓은 상태 cupposition
    CoffeeStateValue == 6  이면 에스프레소 상태 -- espresso
    CoffeeStateValue == 7  테이크 아웃 컵에 옴겨 담으면 takeoutcup
    CoffeeStateValue == 8  뜨거운 물 부으면 >>>> 아메리카노 상태  --  americano
    */



    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioClip = (AudioClip)Resources.Load("Sounds/ok");
        //튜토리얼 오브젝트 대입
        Grinder = GameObject.FindGameObjectWithTag("Grinder");
      Potafillter = GameObject.FindGameObjectWithTag("Potafillter");
      Temper = GameObject.FindGameObjectWithTag("Temper");
      CoffeeMachine = GameObject.FindGameObjectWithTag("CoffeeMachine");
      EspressoCup = GameObject.FindGameObjectWithTag("EspressoCup");
      CaramelSauce = GameObject.FindGameObjectWithTag("CaramelSauce");
      CaramelSyrup = GameObject.FindGameObjectWithTag("CaramelSyrup");
      ChocoSauce = GameObject.FindGameObjectWithTag("ChocoSauce");
      Milk = GameObject.FindGameObjectWithTag("Milk");
      MilkPitcher = GameObject.FindGameObjectWithTag("MilkPitcher");
      VanillaSyrup = GameObject.FindGameObjectWithTag("VanillaSyrup");
      WhippingCream = GameObject.FindGameObjectWithTag("WhippingCream");
      TakeOutCup = GameObject.FindGameObjectWithTag("TakeOutCup");
     collCustomer = GameObject.FindGameObjectWithTag("Collcustomer");
        GrinderBox = GameObject.FindGameObjectWithTag("GrinderBox");
        PotaPosition = GameObject.FindGameObjectWithTag("PotaPosition");
        CupPosition = GameObject.FindGameObjectWithTag("CupPosition");

        Coffeebutton = GameObject.Find("Coffeebutton");
        WaterButton = GameObject.Find("WaterButton");


        coffeestate = CoffeeStatus.Beans;
        espressostate = EspressoStatus.Beans;
        lattestate = CafeLatteStatus.Beans;
        mochastate = CafeMochaStatus.Beans;
        caramelstate = CaramelMacchiatoStatus.Beans;
        Step = menu.GetComponentsInChildren<Text> ();
        //   DebugCoffeeState();
        StartCoroutine ( this.changeTextKorean () );
    }
    IEnumerator changeTextKorean()
    {
        yield return new WaitForSeconds ( 0.02f );
        isTextKor = GameManager.instance.kor;
        StartCoroutine ( changeTextKorean () );
    }
    //Customer.cs에서 호출됨
    public void changeText()
    {
      //  Debug.Log ( "한글인가?"+isTextKor );
        if ( GameManager.instance.order.GetComponentInChildren<Text> ().text == "Espresso!" )
        {
            GameManager.instance.TutorialMaterialObj = Grinder;
            for ( int i = 0; i < espresso.Length; i++ )
            {
                if ( isTextKor )
                {
                   // Step [ i ].text = defaultText [ i ];
                    Step [ i ].text = espressoKO [ i ];
                }
                else
                {
                    //Step [ i ].text = defaultText [ i ];

                    Step [ i ].text = espresso [ i ];
                }
                Step [ i ].color = Color.black; ;
                Step [ 0 ].color = new Color ( r,g,b);
            }
        }
        if ( GameManager.instance.order.GetComponentInChildren<Text> ().text == "Americano!" )
        {
            GameManager.instance.TutorialMaterialObj = Grinder;
            for ( int i = 0; i < americano.Length; i++ )
            {
                if ( isTextKor )
                {
                   // Step [ i ].text = defaultText [ i ];

                    Step [ i ].text = americanoKO [ i ];
                }
                else
                {
                    //Step [ i ].text = defaultText [ i ];

                    Step [ i ].text = americano [ i ];
                }
                Step [ i ].color = Color.black; ;
                Step [ 0 ].color = new Color ( r , g , b );

            }
        }
        if ( GameManager.instance.order.GetComponentInChildren<Text> ().text == "Cafe Latte!" )
        {
            GameManager.instance.TutorialMaterialObj = Grinder;
            for ( int i = 0; i < latte.Length; i++ )
            {
                if ( isTextKor )
                {
                    //Step [ i ].text = defaultText [ i ];

                    Step [ i ].text = latteKO [ i ];
                }
                else
                {
                    //Step [ i ].text = defaultText [ i ];

                    Step [ i ].text = latte [ i ];
                }
                Step [ i ].color = Color.black; ;
                Step [ 0 ].color = new Color ( r , g , b );

            }
        }
        if ( GameManager.instance.order.GetComponentInChildren<Text> ().text == "Cafe Mocha!" )
        {
            GameManager.instance.TutorialMaterialObj = ChocoSauce;
            GameManager.instance.TutorialMaterialObj2 = TakeOutCup;
            for ( int i = 0; i < mocha.Length; i++ )
            {
                if ( isTextKor )
                {
                    //Step [ i ].text = defaultText [ i ];

                    Step [ i ].text = mochaKO [ i ];
                }
                else
                {
                    //Step [ i ].text = defaultText [ i ];

                    Step [ i ].text = mocha [ i ];
                }
                Step [ i ].color = Color.black; ;
                Step [ 0 ].color = new Color ( r , g , b );
            }
        }
        if ( GameManager.instance.order.GetComponentInChildren<Text> ().text == "Caramel Macchiato!" )
        {
            GameManager.instance.TutorialMaterialObj = VanillaSyrup;
            GameManager.instance.TutorialMaterialObj2 = TakeOutCup;
            for ( int i = 0; i < macchiato.Length; i++ )
            {
                if ( isTextKor )
                {
                    //Step [ i ].text = defaultText [ i ];

                    Step [ i ].text = macchiatoKO [ i ];
                }
                else
                {
                    //Step [ i ].text = defaultText [ i ];

                    Step [ i ].text = macchiato [ i ];
                }

                Step [ i ].color = Color.black; ;
                Step [ 0 ].color = new Color ( r , g , b );
            }
        }
    }

    public void DebugCoffeeState()
    {
        audioSource.PlayOneShot(audioClip);
        //0 = espresso , 1 = americano, 2= latte, 3= mocha, 4= caramel
        if ( GameManager.instance.order.GetComponentInChildren<Text> ().text == "Espresso!" )
        {
            coffeeKind = 0;
            //string [] name = 
            int state = (int) espressostate+1;
            //
            //for ( int i = 0; i < espresso.Length; i++ )
            //{
            //    Step [ i ].text = espresso[i];
            //}
            //
          
            switch ( espressostate )
            {
                case EspressoStatus.Beans:
                    {
                        GameManager.instance.TutorialMaterialObj = Potafillter;
                        GameManager.instance.TutorialMaterialObj2 = GrinderBox;
                        Step [ state ].color = Color.green;
                        espressostate = EspressoStatus.grinder;
                        break;
                    }
                case EspressoStatus.grinder:
                    {
                        GameManager.instance.TutorialMaterialObj = Temper;
                        GameManager.instance.TutorialMaterialObj2 = Potafillter;
                        Step [ state ].color = Color.green;
                        espressostate = EspressoStatus.portafilter;
                        break;
                    }
                case EspressoStatus.portafilter:
                    {
                        GameManager.instance.TutorialMaterialObj = PotaPosition;
                        GameManager.instance.TutorialMaterialObj2 = Potafillter;
                        Step [ state ].color = Color.green;
                        espressostate = EspressoStatus.temper;
                        break;
                    }
                case EspressoStatus.temper:
                    {
                        GameManager.instance.TutorialMaterialObj = EspressoCup;
                        GameManager.instance.TutorialMaterialObj2 = CupPosition;
                        Step [ state ].color = Color.green;
                        espressostate = EspressoStatus.potaposition;
                        break;
                    }
                case EspressoStatus.potaposition:
                    {
                        GameManager.instance.TutorialMaterialObj = Coffeebutton;
                        GameManager.instance.TutorialMaterialObj2 = null;
                        Step [ state ].color = Color.green;
                        espressostate = EspressoStatus.cupposition;
                        break;
                    }
                case EspressoStatus.cupposition:
                    {
                        GameManager.instance.TutorialMaterialObj = TakeOutCup;
                        GameManager.instance.TutorialMaterialObj2 = EspressoCup;
                        Step [ state ].color = Color.green;
                        espressostate = EspressoStatus.espresso;
                        break;
                    }
                case EspressoStatus.espresso:
                    {
                        GameManager.instance.TutorialMaterialObj = TakeOutCup;
                        GameManager.instance.TutorialMaterialObj2 = collCustomer;

                        Step [ state ].color = Color.green;
                        espressostate = EspressoStatus.takeoutcup;
                        break;
                    }
                case EspressoStatus.takeoutcup:
                    {
                        GameManager.instance.TutorialMaterialObj = null;
                        GameManager.instance.TutorialMaterialObj2 = null;

                        Step [ state ].color = Color.green;
                        espressostate = EspressoStatus.customer;
                        break;
                    }
                case EspressoStatus.customer:
                    {
                        espressostate = EspressoStatus.defult;
                        for ( int a = 1; a < 10; a++ )
                        {
                            Step [ a ].color = Color.black;
                        }
                        break;
                    }
            }
            Debug.Log ( "espressostate : " + espressostate );
        }
        //0 = espresso , 1 = americano, 2= latte, 3= mocha, 4= caramel
        if ( GameManager.instance.order.GetComponentInChildren<Text> ().text == "Americano!" )
        {
            coffeeKind = 1;
            int state = (int) coffeestate+1;
            Debug.Log ( Step [ state ].text +":"+Step[state]+ ":" + state );

            //
            //for ( int i = 0; i < americano.Length; i++ )
            //{
            //    Step [ i ].text = americano [ i ];
            //}
            //
            switch ( coffeestate )
            {
                case CoffeeStatus.Beans:
                    {
                        GameManager.instance.TutorialMaterialObj = Potafillter;
                        GameManager.instance.TutorialMaterialObj2 = GrinderBox;
                        Step [ state ].color = Color.green;
                        coffeestate = CoffeeStatus.grinder;
                        break;
                    }
                case CoffeeStatus.grinder:
                    {
                        GameManager.instance.TutorialMaterialObj = Temper;
                        GameManager.instance.TutorialMaterialObj2 = Potafillter;
                        Step [ state ].color = Color.green;
                        coffeestate = CoffeeStatus.portafilter;
                        break;
                    }
                case CoffeeStatus.portafilter:
                    {
                        GameManager.instance.TutorialMaterialObj = PotaPosition;
                        GameManager.instance.TutorialMaterialObj2 = Potafillter;
                        Step [ state ].color = Color.green;
                        coffeestate = CoffeeStatus.temper;
                        break;
                    }
                case CoffeeStatus.temper:
                    {
                        GameManager.instance.TutorialMaterialObj = EspressoCup;
                        GameManager.instance.TutorialMaterialObj2 = CupPosition;
                        Step [ state ].color = Color.green;
                        coffeestate = CoffeeStatus.potaposition;
                        break;
                    }
                case CoffeeStatus.potaposition:
                    {
                        GameManager.instance.TutorialMaterialObj = Coffeebutton;
                        GameManager.instance.TutorialMaterialObj2 = null;
                        Step [ state ].color = Color.green;
                        coffeestate = CoffeeStatus.cupposition;
                        break;
                    }
                case CoffeeStatus.cupposition:
                    {
                        GameManager.instance.TutorialMaterialObj = TakeOutCup;
                        GameManager.instance.TutorialMaterialObj2 = EspressoCup;
                        Step [ state ].color = Color.green;
                        coffeestate = CoffeeStatus.espresso;
                        break;
                    }
                case CoffeeStatus.espresso:
                    {
                        GameManager.instance.TutorialMaterialObj = TakeOutCup;
                        GameManager.instance.TutorialMaterialObj2 = CupPosition;
                        Step [ state ].color = Color.green;
                        coffeestate = CoffeeStatus.takeoutcup;
                        break;
                    }

                case CoffeeStatus.takeoutcup:
                    {
                        GameManager.instance.TutorialMaterialObj = TakeOutCup;
                        GameManager.instance.TutorialMaterialObj2 = collCustomer;

                        Step[ state ].color = Color.green;
                        coffeestate = CoffeeStatus.americano;
                        break;
                    }
                case CoffeeStatus.americano:
                    {
                        GameManager.instance.TutorialMaterialObj = null;
                        GameManager.instance.TutorialMaterialObj2 = null;
                        Step [ state ].color = Color.green;
                        coffeestate = CoffeeStatus.customer;
                        break;
                    }
                case CoffeeStatus.customer:
                    {
                        coffeestate = CoffeeStatus.defult;
                        for ( int a = 1; a < 11; a++ )
                        {
                            Step [ a ].color = Color.black;
                        }
                        break;
                    }
            }
            Debug.Log ( "coffeestate : " + coffeestate );
        }
        //0 = espresso , 1 = americano, 2= latte, 3= mocha, 4= caramel
        if ( GameManager.instance.order.GetComponentInChildren<Text> ().text == "Cafe Latte!" )
        {
            coffeeKind = 2;
            int state = (int) lattestate+1;
            //
            //for ( int i = 0; i < latte.Length; i++ )
            //{
            //    Step [ i ].text = latte [ i ];
            //}
            //
            switch ( lattestate )
            {
                case CafeLatteStatus.Beans:
                    {
                        GameManager.instance.TutorialMaterialObj = Potafillter;
                        GameManager.instance.TutorialMaterialObj2 = GrinderBox;
                        Step [ state ].color = Color.green;
                        lattestate = CafeLatteStatus.grinder;
                        break;
                    }
                case CafeLatteStatus.grinder:
                    {
                        GameManager.instance.TutorialMaterialObj = Temper;
                        GameManager.instance.TutorialMaterialObj2 = Potafillter;
                        Step [ state ].color = Color.green;
                        lattestate = CafeLatteStatus.portafilter;
                        break;
                    }
                case CafeLatteStatus.portafilter:
                    {
                        GameManager.instance.TutorialMaterialObj = PotaPosition;
                        GameManager.instance.TutorialMaterialObj2 = Potafillter;
                        Step [ state ].color = Color.green;
                        lattestate = CafeLatteStatus.temper;
                        break;
                    }
                case CafeLatteStatus.temper:
                    {
                        GameManager.instance.TutorialMaterialObj = EspressoCup;
                        GameManager.instance.TutorialMaterialObj2 = CupPosition;
                        Step [ state ].color = Color.green;
                        lattestate = CafeLatteStatus.potaposition;
                        break;
                    }
                case CafeLatteStatus.potaposition:
                    {
                        GameManager.instance.TutorialMaterialObj = Coffeebutton;
                        GameManager.instance.TutorialMaterialObj2 = null;
                        Step [ state ].color = Color.green;
                        lattestate = CafeLatteStatus.cupposition;
                        break;
                    }
                case CafeLatteStatus.cupposition:
                    {
                        GameManager.instance.TutorialMaterialObj = TakeOutCup;
                        GameManager.instance.TutorialMaterialObj2 = EspressoCup;
                        Step [ state ].color = Color.green;
                        lattestate = CafeLatteStatus.espresso;
                        break;
                    }
                case CafeLatteStatus.espresso:
                    {
                        GameManager.instance.TutorialMaterialObj = TakeOutCup;
                        GameManager.instance.TutorialMaterialObj2 = MilkPitcher;
                        Step [ state ].color = Color.green;
                        lattestate = CafeLatteStatus.takeoutcup;
                        break;
                    }

                case CafeLatteStatus.takeoutcup:
                    {
                        GameManager.instance.TutorialMaterialObj = TakeOutCup;
                        GameManager.instance.TutorialMaterialObj2 = collCustomer;
                        Step [ state ].color = Color.green;
                        lattestate = CafeLatteStatus.steammilk;
                        break;
                    }
                case CafeLatteStatus.steammilk:
                    {
                        GameManager.instance.TutorialMaterialObj = null;
                        GameManager.instance.TutorialMaterialObj2 = null;
                        Step [ state ].color = Color.green;
                        lattestate = CafeLatteStatus.customer;
                        break;
                    }
                //case CafeLatteStatus.cafelatte:
                //    {
                //        Step [ state ].color = Color.green;
                //        lattestate = CafeLatteStatus.customer;
                //        break;
                //    }
                case CafeLatteStatus.customer:
                    {
                        lattestate = CafeLatteStatus.defult;
                        for ( int a = 1; a < 11; a++ )
                        {
                            Step [ a ].color = Color.black;
                        }
                        break;
                    }
            }
            Debug.Log ( "lattestate : " + lattestate );
        }
        //0 = espresso , 1 = americano, 2= latte, 3= mocha, 4= caramel
        if ( GameManager.instance.order.GetComponentInChildren<Text> ().text == "Cafe Mocha!" )
        {
            coffeeKind = 3;
            int state = (int) mochastate+1;
            //
            //for ( int i = 0; i < mocha.Length; i++ )
            //{
            //    Step [ i ].text = mocha [ i ];
            //}
            //
            switch ( mochastate )
            {
                case CafeMochaStatus.Beans:
                    {
                        GameManager.instance.TutorialMaterialObj = Grinder;
                        GameManager.instance.TutorialMaterialObj2 = null;
                        Step [ state ].color = Color.green;
                        mochastate = CafeMochaStatus.chocolatesauce;
                        break;
                    }

                case CafeMochaStatus.chocolatesauce:
                    {
                        GameManager.instance.TutorialMaterialObj = Potafillter;
                        GameManager.instance.TutorialMaterialObj2 = GrinderBox;
                        Step [ state ].color = Color.green;
                        mochastate = CafeMochaStatus.grinder;
                        break;
                    }


                case CafeMochaStatus.grinder:
                    {
                        GameManager.instance.TutorialMaterialObj = Temper;
                        GameManager.instance.TutorialMaterialObj2 = Potafillter;
                        Step [ state ].color = Color.green;
                        mochastate = CafeMochaStatus.portafilter;
                        break;
                    }
                case CafeMochaStatus.portafilter:
                    {
                        GameManager.instance.TutorialMaterialObj = PotaPosition;
                        GameManager.instance.TutorialMaterialObj2 = Potafillter;
                        Step [ state ].color = Color.green;
                        mochastate = CafeMochaStatus.temper;
                        break;
                    }
                case CafeMochaStatus.temper:
                    {
                        GameManager.instance.TutorialMaterialObj = EspressoCup;
                        GameManager.instance.TutorialMaterialObj2 = CupPosition;
                        Step [ state ].color = Color.green;
                        mochastate = CafeMochaStatus.potaposition;
                        break;
                    }
                case CafeMochaStatus.potaposition:
                    {
                        GameManager.instance.TutorialMaterialObj = Coffeebutton;
                        GameManager.instance.TutorialMaterialObj2 = null;
                        Step [ state ].color = Color.green;
                        mochastate = CafeMochaStatus.cupposition;
                        break;
                    }
                case CafeMochaStatus.cupposition:
                    {
                        GameManager.instance.TutorialMaterialObj = TakeOutCup;
                        GameManager.instance.TutorialMaterialObj2 = EspressoCup;
                        Step [ state ].color = Color.green;
                        mochastate = CafeMochaStatus.espresso;
                        break;
                    }
                case CafeMochaStatus.espresso:
                    {
                        GameManager.instance.TutorialMaterialObj = TakeOutCup;
                        GameManager.instance.TutorialMaterialObj2 = MilkPitcher;
                        Step [ state ].color = Color.green;
                        mochastate = CafeMochaStatus.takeoutcup;
                        break;
                    }

                case CafeMochaStatus.takeoutcup:
                    {
                        GameManager.instance.TutorialMaterialObj = TakeOutCup;
                        GameManager.instance.TutorialMaterialObj2 = WhippingCream;
                        Step [ state ].color = Color.green;
                        mochastate = CafeMochaStatus.steammilk;
                        break;
                    }
                case CafeMochaStatus.steammilk:
                    {
                        GameManager.instance.TutorialMaterialObj = TakeOutCup;
                        GameManager.instance.TutorialMaterialObj2 = collCustomer;
                        Step [ state ].color = Color.green;
                        mochastate = CafeMochaStatus.whippedcream;
                        break;
                    }
                case CafeMochaStatus.whippedcream:
                    {
                        GameManager.instance.TutorialMaterialObj = null;
                        GameManager.instance.TutorialMaterialObj2 = null;
                        Step [ state ].color = Color.green;
                        mochastate = CafeMochaStatus.customer;
                        break;
                    }

                //case CafeMochaStatus.cafemocha:
                //    {
                //        Step [ state ].color = Color.green;
                //        mochastate = CafeMochaStatus.customer;
                //        break;
                //    }


                case CafeMochaStatus.customer:
                    {
                        mochastate = CafeMochaStatus.defult;
                        for ( int a = 1; a < 13; a++ )
                        {
                            Step [ a ].color = Color.black;
                        }
                        break;
                    }
            }
            Debug.Log ( "mochastate : " + mochastate );
        }
        //0 = espresso , 1 = americano, 2= latte, 3= mocha, 4= caramel
        if ( GameManager.instance.order.GetComponentInChildren<Text> ().text == "Caramel Macchiato!" )
        {
            coffeeKind = 4;
            int state = (int) caramelstate+1;
            //
            //for ( int i = 0; i < macchiato.Length; i++ )
            //{
            //    Step [ i ].text = macchiato [ i ];
            //}
            //
            switch ( caramelstate )
            {
                case CaramelMacchiatoStatus.Beans:
                    {
                        GameManager.instance.TutorialMaterialObj = MilkPitcher;
                        GameManager.instance.TutorialMaterialObj2 = TakeOutCup;
                        Step [ state ].color = Color.green;
                        caramelstate = CaramelMacchiatoStatus.vanillasyrup;
                        break;
                    }

                case CaramelMacchiatoStatus.vanillasyrup:
                    {
                        GameManager.instance.TutorialMaterialObj = CaramelSyrup;
                        GameManager.instance.TutorialMaterialObj2 = TakeOutCup;
                        Step [ state ].color = Color.green;
                        caramelstate = CaramelMacchiatoStatus.steammilk;
                        break;
                    }

                case CaramelMacchiatoStatus.steammilk:
                    {
                        GameManager.instance.TutorialMaterialObj = Grinder;
                        GameManager.instance.TutorialMaterialObj2 = null;
                        Step [ state ].color = Color.green;
                        caramelstate = CaramelMacchiatoStatus.caramelsyrup;
                        break;
                    }



                case CaramelMacchiatoStatus.caramelsyrup:
                    {
                        GameManager.instance.TutorialMaterialObj = GrinderBox;
                        GameManager.instance.TutorialMaterialObj2 = Potafillter;
                        Step [ state ].color = Color.green;
                        caramelstate = CaramelMacchiatoStatus.grinder;
                        break;
                    }


                case CaramelMacchiatoStatus.grinder:
                    {
                        GameManager.instance.TutorialMaterialObj = Temper;
                        GameManager.instance.TutorialMaterialObj2 = Potafillter;
                        Step [ state ].color = Color.green;
                        caramelstate = CaramelMacchiatoStatus.portafilter;
                        break;
                    }
                case CaramelMacchiatoStatus.portafilter:
                    {
                        GameManager.instance.TutorialMaterialObj = PotaPosition;
                        GameManager.instance.TutorialMaterialObj2 = Potafillter;
                        Step [ state ].color = Color.green;
                        caramelstate = CaramelMacchiatoStatus.temper;
                        break;
                    }
                case CaramelMacchiatoStatus.temper:
                    {
                        GameManager.instance.TutorialMaterialObj = CupPosition;
                        GameManager.instance.TutorialMaterialObj2 = EspressoCup;
                        Step [ state ].color = Color.green;
                        caramelstate = CaramelMacchiatoStatus.potaposition;
                        break;
                    }
                case CaramelMacchiatoStatus.potaposition:
                    {
                        GameManager.instance.TutorialMaterialObj = Coffeebutton;
                        GameManager.instance.TutorialMaterialObj2 = null;
                        Step [ state ].color = Color.green;
                        caramelstate = CaramelMacchiatoStatus.cupposition;
                        break;
                    }
                case CaramelMacchiatoStatus.cupposition:
                    {
                        GameManager.instance.TutorialMaterialObj = TakeOutCup;
                        GameManager.instance.TutorialMaterialObj2 = EspressoCup;
                        Step [ state ].color = Color.green;
                        caramelstate = CaramelMacchiatoStatus.espresso;
                        break;
                    }
                case CaramelMacchiatoStatus.espresso:
                    {
                        GameManager.instance.TutorialMaterialObj = TakeOutCup;
                        GameManager.instance.TutorialMaterialObj2 = CaramelSauce;
                        Step [ state ].color = Color.green;
                        caramelstate = CaramelMacchiatoStatus.takeoutcup;
                        break;
                    }

                case CaramelMacchiatoStatus.takeoutcup:
                    {
                        GameManager.instance.TutorialMaterialObj = TakeOutCup;
                        GameManager.instance.TutorialMaterialObj2 = collCustomer;

                        Step[ state ].color = Color.green;
                        caramelstate = CaramelMacchiatoStatus.caramelsauce;
                        break;
                    }
                case CaramelMacchiatoStatus.caramelsauce:
                    {
                        GameManager.instance.TutorialMaterialObj = null;
                        GameManager.instance.TutorialMaterialObj2 = null;
                        Step [ state ].color = Color.green;
                        caramelstate = CaramelMacchiatoStatus.customer;
                        break;
                    }
                //case CaramelMacchiatoStatus.caramelmacchiato:
                //    {
                //        Step [ state ].color = Color.green;
                //        caramelstate = CaramelMacchiatoStatus.customer;
                //        break;
                //    }

                case CaramelMacchiatoStatus.customer:
                    {
                        caramelstate = CaramelMacchiatoStatus.defult;
                        for ( int a = 1; a < 14; a++ )
                        {
                            Step [ a ].color = Color.black;
                        }
                        break;
                    }
            }
            Debug.Log ( "caramelstate : " + caramelstate );
        }


    }

}
