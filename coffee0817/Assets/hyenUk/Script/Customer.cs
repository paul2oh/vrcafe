using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    // 사운드
    private AudioSource audioSource;
    private AudioClip esp;
    private AudioClip ame;
    private AudioClip latt;
    private AudioClip mocha;
    private AudioClip caramel;
    private AudioClip complain;


    public GameObject[] characters;
    public Image fillImage;
    private float waitTime;
    private bool AmericanoMan = false;
    // Use this for initialization
    void Start()
    {
        for(int a= 0; a< GameManager.instance.characters.Length; a++)
        {
            characters[a] = GameManager.instance.characters[a];
        }

        audioSource = this.gameObject.GetComponent<AudioSource>();
        esp = (AudioClip)Resources.Load("Sounds/espresso");
        ame = (AudioClip)Resources.Load("Sounds/americano");
        latt = (AudioClip)Resources.Load("Sounds/cafelatte");
        mocha = (AudioClip)Resources.Load("Sounds/cafemocca");
        caramel = (AudioClip)Resources.Load("Sounds/caramelmacciato");
        complain = (AudioClip)Resources.Load("Sounds/complain");


        OnCustomer();

        for (int a = 0; a < characters.Length; a++)
        {
            characters[a].SetActive(false);
        }

    }

    public void OnCustomer()
    {
        fillImage.fillAmount = 0.0f;
        AmericanoMan = false;
        float delayTime = Random.Range(5.0f, 10.0f);
        StartCoroutine(this.startCostomer(delayTime));
    }


    IEnumerator startCostomer(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        GameManager.instance.order.enabled = true;
        GameManager.instance.order.GetComponentInChildren<Text>().color = Color.green;
        //0 = espresso , 1 = americano, 2= latte, 3= mocha, 4= caramel
          int RandomCoffee = Random.Range(0, 5);
     
        characters [RandomCoffee].SetActive(true);
        CoffeeManager.instance.coffeeKind = RandomCoffee;
        GameManager.instance.order.GetComponent<CanvasScaler>().dynamicPixelsPerUnit = 2;
        if (RandomCoffee == 0)//에스프레소
        {
            audioSource.PlayOneShot(esp);
            CoffeeManager.instance.caramelstate = CoffeeManager.CaramelMacchiatoStatus.defult;
            CoffeeManager.instance.mochastate = CoffeeManager.CafeMochaStatus.defult;
            CoffeeManager.instance.lattestate = CoffeeManager.CafeLatteStatus.defult;
            CoffeeManager.instance.coffeestate = CoffeeManager.CoffeeStatus.defult;
            CoffeeManager.instance.espressostate = CoffeeManager.EspressoStatus.Beans;
            GameManager.instance.order.GetComponentInChildren<Text>().text = "Espresso!";
            CoffeeManager.instance.changeText ();
            //시간 조절
            waitTime = 150.0f;
        }
        else if (RandomCoffee == 1)//아메리카노
        {
            audioSource.PlayOneShot(ame);
            CoffeeManager.instance.caramelstate = CoffeeManager.CaramelMacchiatoStatus.defult;
            CoffeeManager.instance.mochastate = CoffeeManager.CafeMochaStatus.defult;
            CoffeeManager.instance.lattestate = CoffeeManager.CafeLatteStatus.defult;
            CoffeeManager.instance.espressostate = CoffeeManager.EspressoStatus.defult;
            CoffeeManager.instance.coffeestate = CoffeeManager.CoffeeStatus.Beans;
            GameManager.instance.order.GetComponentInChildren<Text>().text = "Americano!";
            CoffeeManager.instance.changeText ();

            waitTime = 160.0f;
        }
        else if (RandomCoffee == 2)//라떼
        {
            audioSource.PlayOneShot(latt);
            CoffeeManager.instance.caramelstate = CoffeeManager.CaramelMacchiatoStatus.defult;
            CoffeeManager.instance.mochastate = CoffeeManager.CafeMochaStatus.defult;
            CoffeeManager.instance.coffeestate = CoffeeManager.CoffeeStatus.defult;
            CoffeeManager.instance.espressostate = CoffeeManager.EspressoStatus.defult;
            CoffeeManager.instance.lattestate = CoffeeManager.CafeLatteStatus.Beans;
            GameManager.instance.order.GetComponentInChildren<Text>().text = "Cafe Latte!";
            CoffeeManager.instance.changeText ();

            waitTime = 170.0f;
        }
        else if (RandomCoffee == 3)//모카
        {
            audioSource.PlayOneShot(mocha);
            CoffeeManager.instance.caramelstate = CoffeeManager.CaramelMacchiatoStatus.defult;
            CoffeeManager.instance.lattestate = CoffeeManager.CafeLatteStatus.defult;
            CoffeeManager.instance.coffeestate = CoffeeManager.CoffeeStatus.defult;
            CoffeeManager.instance.espressostate = CoffeeManager.EspressoStatus.defult;
            CoffeeManager.instance.mochastate = CoffeeManager.CafeMochaStatus.Beans;
            GameManager.instance.order.GetComponentInChildren<Text>().text = "Cafe Mocha!";
            CoffeeManager.instance.changeText ();

            waitTime = 180.0f;
        }
        else if (RandomCoffee == 4)//카라멜
        {
            audioSource.PlayOneShot(caramel);
            CoffeeManager.instance.mochastate = CoffeeManager.CafeMochaStatus.defult;
            CoffeeManager.instance.lattestate = CoffeeManager.CafeLatteStatus.defult;
            CoffeeManager.instance.coffeestate = CoffeeManager.CoffeeStatus.defult;
            CoffeeManager.instance.espressostate = CoffeeManager.EspressoStatus.defult;

            CoffeeManager.instance.caramelstate = CoffeeManager.CaramelMacchiatoStatus.Beans;
            GameManager.instance.order.GetComponentInChildren<Text>().text = "Caramel Macchiato!";
            CoffeeManager.instance.changeText ();

            waitTime = 190.0f;
        }
        AmericanoMan = true;

    }
    // Update is called once per frame
    void Update()
    {
        if (AmericanoMan)
        {
            fillImage.fillAmount += 1.0f / waitTime * Time.deltaTime;
        }

        if (fillImage.fillAmount >= 1.0f)
        {
            fillImage.fillAmount = 0f;
            StartCoroutine(this.Complain());
        }
    }
    IEnumerator Complain()
    {
        AmericanoMan = false;
        GameManager.instance.TutorialMaterialObj = null;
        GameManager.instance.TutorialMaterialObj2 = null;
        GameManager.instance.Result[GameManager.instance.ResultInx++] = GameManager.instance.order.GetComponentInChildren<Text>().text+ " : fail";
        GameManager.instance.order.GetComponentInChildren<Text>().text = "Complain!!!";


        audioSource.PlayOneShot(complain);

        GameManager.instance.order.GetComponentInChildren<Text>().color = Color.red;
        yield return new WaitForSeconds(1.0f);

      // CoffeeManager.instance.coffeestate = CoffeeManager.CoffeeStatus.grinder;

        for (int a = 0; a < characters.Length; a++)
        {
            characters[a].SetActive(false);
        }

        GameManager.instance.order.enabled = false;

        if (GameManager.instance.ResultInx != 11)
        {
            OnCustomer();
        }
    }
}
