using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeOutCup : MonoBehaviour {

    private Transform tr;
    private Vector3 StartPosition;

    public GameObject[] characters;
    public Customer customer;

    private AudioSource audioSource;
    private AudioClip thank;


    private GameObject cover;
    private void Start()
    {

       // cover = GameObject.FindGameObjectWithTag("cover");
       // cover.SetActive(false);
        customer = GameObject.FindGameObjectWithTag("Customer").GetComponent<Customer>();

        audioSource = customer.GetComponent<AudioSource>();
        thank = (AudioClip)Resources.Load("Sounds/ThankYou");

        tr = this.gameObject.GetComponent<Transform>();
        StartPosition = tr.position;

        for (int a = 0; a < GameManager.instance.characters.Length; a++)
        {
            characters[a] = GameManager.instance.characters[a];
        }
    }

    //수정 2017.07.14
    void collCustomer()
    {
            StartCoroutine(this.Customer());
    }
    IEnumerator Customer()
    {
        yield return new WaitForSeconds(1.0f);
        GameManager.instance.TutorialMaterialObj = null;
        GameManager.instance.TutorialMaterialObj2 = null;

        GameManager.instance.Result[GameManager.instance.ResultInx++] = GameManager.instance.order.GetComponentInChildren<Text>().text + " : Success";
        GameManager.instance.Score += 1;
        GameManager.instance.order.GetComponentInChildren<Text>().text = "Thank You";
        audioSource.PlayOneShot(thank);


        GameManager.instance.order.GetComponentInChildren<Text>().color = Color.blue;

        customer.fillImage.fillAmount = 0.0f;
        //손님 애니메이션?대사?
        yield return new WaitForSeconds(1.0f);

       // tr.position = StartPosition;
        GameManager.instance.order.enabled = false;

        for (int a = 0; a < characters.Length; a++)
        {
            characters[a].SetActive(false);
        }
        if (GameManager.instance.ResultInx != 11)
        {
            customer.OnCustomer();
        }
       
    }
}
