using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//트리거 체크했을때 takeout컵이 들어오면 
//1. 컵의 뚜껑을 없앤다
//2. 물을 눌러서 3초가 지나면 커피 표면 생성하기
//3. ontriggerExit일때 뚜껑 생성하기
public class CupCheck : MonoBehaviour {
    //public GameObject cup;
    public GameObject lid;
    //현재시간
    public float currentTime;
    float plusTime = 0.001f;
    float maxTime = 3.0f;
    public GameObject coffeeSurface;

    void Start()
    {
       // coffeeSurface = GameObject.Find ( "Liquid" ).GetComponent<GameObject> ();
    }

    //뚜껑 없애기
    void OnTriggerEnter(Collider col)
    {
      
    }

    void OnTriggerStay(Collider col )
    {
        if ( CoffeeManager.instance.coffeestate == CoffeeManager.CoffeeStatus.takeoutcup && col.gameObject.name == "TakeOutPosition" )
        {
            //CoffeeManager.instance.coffeestate = CoffeeManager.CoffeeStatus.espresso;
            lid.SetActive ( false );
        }
    }

    void OnTriggerExit( Collider col )
    {
        lid.SetActive ( true );
    }

    void Update()
    {
        currentTime += Time.deltaTime;
   
    }


    void buttonClicked()
    {
        Debug.Log ( "물버튼 나옴" );
        currentTime = 0f;
        if ( CoffeeManager.instance.coffeestate == CoffeeManager.CoffeeStatus.takeoutcup )
        {
            for ( int i = 1; i <= maxTime; i++ )
            {
                if ( currentTime >=3.0f )
                {
                    break;
                }
                else if ( currentTime <= 3.0f )
                {
                    currentTime += i * plusTime;
                }
            }
         
                Debug.Log ( "3초 지남" );
                coffeeSurface.SetActive ( true );
                CoffeeManager.instance.DebugCoffeeState ();
            
           
          
        }
    }

}
