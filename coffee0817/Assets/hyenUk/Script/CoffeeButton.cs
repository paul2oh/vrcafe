﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoffeeButton : MonoBehaviour {

    private Material met;
    private Material metTemp;
    private bool CheckBool;

    private bool CheckEnter;

    // 사운드
    private AudioSource audioSource;
    private AudioClip audioClip;


    //오른손인지 왼손인지
    private string CheckRight;

    private GameObject objects;

    public GameObject coffee;
    public GameObject coffeewater;
    // Use this for initialization
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioClip = (AudioClip)Resources.Load("Sounds/water");

        met = (Material)Resources.Load("Materials/GreenButton");
        CheckBool = false;
        metTemp = this.gameObject.GetComponent<MeshRenderer>().material;
        objects = coffeewater;
        coffee.SetActive(false);
        coffeewater.SetActive(false);
        objects.SetActive(false);
    }

    private void Update()
    {
      
        if ((Input.GetButtonDown("Fire1") && CheckRight == "Left") || (Input.GetButtonDown("Fire2") && CheckRight == "Right") || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
           
            if (CheckEnter)
            {
                //수정 2017.07.17
                if ( CoffeeManager.instance.coffeestate == CoffeeManager.CoffeeStatus.cupposition || CoffeeManager.instance.espressostate == CoffeeManager.EspressoStatus.cupposition ||
              CoffeeManager.instance.lattestate == CoffeeManager.CafeLatteStatus.cupposition || CoffeeManager.instance.mochastate == CoffeeManager.CafeMochaStatus.cupposition ||
              CoffeeManager.instance.caramelstate == CoffeeManager.CaramelMacchiatoStatus.cupposition )
                {
                    StartCoroutine(this.Timer2());
                    objects = coffee;
                }
                else
                {
                    objects = coffeewater;
                }
                if (!CheckBool)
                {
                    isWaterBool();
                    Debug.Log("켰다");
                    StartCoroutine(this.Timer());
                }
            }
        }
    }

    IEnumerator Timer2()
    {
        audioSource.PlayOneShot(audioClip);
        yield return new WaitForSeconds(3.0f);
        CoffeeManager.instance.DebugCoffeeState();
    }

    IEnumerator Timer()
    {
        audioSource.PlayOneShot(audioClip);
        objects.SetActive(true);
        this.gameObject.GetComponentInChildren<MeshRenderer>().material = met;

        yield return new WaitForSeconds(3.0f);

        objects.SetActive(false);
        this.gameObject.GetComponentInChildren<MeshRenderer>().material = metTemp;
        isWaterBool();
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
