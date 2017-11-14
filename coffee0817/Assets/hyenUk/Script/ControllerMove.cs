using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//PC Test용 컨트롤러 이동 스크립트
public class ControllerMove : MonoBehaviour
{

    // 0620 편집중
    public OVRInput.Controller ovrController;
    public float grabRange = 0.1f;
    public LayerMask mask;
    Quaternion currentRotation;
    Quaternion lastRotation;
    float ThrowPower = 5.0f;


    Vector3 deltaPos;
    Vector3 predeltaPos;
    Vector3 postdeltapos;

    public GameObject hand;
    public GameObject fist;


    //속도
    int speed = 1;
    //회전 속도
    int rotSpeed = 120;

    public string name;

    // Use this for initialization
    void Start()
    {
        hand.SetActive(true);
        fist.SetActive(false);

        StartCoroutine(this.timer());
    }


    IEnumerator timer()
    {
        yield return new WaitForSeconds(0.02f);


        if (GameManager.instance.nearObjectRight != null)
        {
               postdeltapos = predeltaPos;
            predeltaPos = GameManager.instance.nearObjectRight.transform.position;
        }
        else
        {
     //       postdeltapos = new Vector3(0,0,0);
       //     predeltaPos = new Vector3(0, 0, 0);

        }

        StartCoroutine(this.timer());
    }

    private void OnTriggerEnter(Collider other)
    {
        object[] _params = new object[1];
        _params[0] = name;
        other.gameObject.SendMessage("OnEnter", _params, SendMessageOptions.DontRequireReceiver);
    }
  

    private void OnTriggerExit(Collider other)
    {
        object[] _params = new object[1];
        _params[0] = name;
        other.gameObject.SendMessage("OnExit", _params, SendMessageOptions.DontRequireReceiver);
    }


    // Update is called once per frame
    void Update()
    {
        
        if (GameManager.instance.nearObjectRight != null)
        {
            lastRotation = currentRotation;

            currentRotation = GameManager.instance.nearObjectRight.transform.rotation;

         
        }

        ///유니티 체크용 
        //오른손 누를때
        if (Input.GetButtonDown("Fire2") && name == "Right")
        {
            CatchObject(true);
            hand.SetActive(false);
            fist.SetActive(true);
        }

        //오른손 땔때
        if (Input.GetButtonUp("Fire2") && name == "Right")
        {
            DropObject(true);
            hand.SetActive(true);
            fist.SetActive(false);
        }

        /////////////////////////////////// nolo vr 
        //컨트롤러 왼손 눌렀을때
        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonDown(NoloButtonID.Trigger) && name == "Left")
        {
            CatchObject(false);
            hand.SetActive(false);
            fist.SetActive(true);
        }
        // 컨트롤러 왼손 땟을때
        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonUp(NoloButtonID.Trigger) && name == "Left")
        {
            DropObject(false);
            hand.SetActive(true);
            fist.SetActive(false);
        }

        //오른손 누를때
        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonDown(NoloButtonID.Trigger) && name == "Right" || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            CatchObject(true);
            hand.SetActive(false);
            fist.SetActive(true);
        }

        //오른손 땔때
        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonUp(NoloButtonID.Trigger) && name == "Right" || OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            DropObject(true);
            hand.SetActive(true);
            fist.SetActive(false);
        }
        /////////////////////////


        //컨트롤러 이동 속도,회전속도
        float conMove = speed * Time.smoothDeltaTime;
        float conRot = rotSpeed * Time.smoothDeltaTime;

        //터치패드 터치 위치 함수
        Vector2 primaryTouchpad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);


        //터치패드로 핸드 움직이기
        if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad))
        {
            Vector3 dir3 = new Vector3(primaryTouchpad.x, 0, primaryTouchpad.y);
            transform.Translate(dir3 * conMove);
        }

        //노로 터치패드 터치 위치 함수
        Vector2 noloTouchpadRight = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetAxis();
        Vector2 noloTouchpadLeft = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetAxis();
        // 노로 터치 패드로 핸드 움직이기
        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.TouchPad) && name == "Right")
        {
            Vector3 dir3 = new Vector3(noloTouchpadRight.x, 0, noloTouchpadRight.y);
            transform.Translate(dir3 * conMove);
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.TouchPad) && name == "Left")
        {
            Vector3 dir3 = new Vector3(noloTouchpadLeft.x, 0, noloTouchpadLeft.y);
            transform.Translate(dir3 * conMove);
        }

        //키보드 손위치이동
        float keyForward = Input.GetAxis("Vertical");
        float keyright = Input.GetAxis("Horizontal");
        float keyUp = Input.GetAxis("UpDown");

        Vector3 dir = new Vector3(keyright, keyUp, keyForward);
        transform.Translate(dir * conMove);

        //손 회전
        float RotRight = Input.GetAxis("RotRight");
        float Rotforward = Input.GetAxis("Rotforward");
        float RotUp = Input.GetAxis("RotUp");

        Vector3 dir2 = new Vector3(RotUp, RotRight, Rotforward);
        transform.Rotate(dir2 * conRot);
    }

    void CatchObject(bool Right)
    {
        //  isGrabbing = true;
        GameObject obj;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit[] hitInfos = Physics.SphereCastAll(ray, grabRange, 0, mask);

        if (hitInfos.Length > 0)
        {
            int nearobj = 0;
            for (int i = 0; i < hitInfos.Length; i++)
            {
                if (hitInfos[i].distance < hitInfos[nearobj].distance)
                {
                    nearobj = i;
                }
            }
            obj = hitInfos[nearobj].transform.gameObject;


            //potaposition 과 cup position 일때 controller 못하도록하기
            if ((CoffeeManager.instance.coffeestate == CoffeeManager.CoffeeStatus.potaposition && obj.name == "Potafillter") || (CoffeeManager.instance.coffeestate == CoffeeManager.CoffeeStatus.cupposition
                && obj.name == "Potafillter"))
            ////
            {
                if (Right)
                {
                    GameManager.instance.nearObjectRight = null;
                }
                else
                {
                    GameManager.instance.nearObjectLeft = null;
                }
            }
            else
            {
                if (Right)
                {
                    GameManager.instance.nearObjectRight = obj;
                }
                else
                {
                    GameManager.instance.nearObjectLeft = obj;
                }
                obj.transform.position = transform.position;
                obj.transform.parent = transform;

                obj.SendMessage("FixPosition", SendMessageOptions.DontRequireReceiver);
                obj.GetComponent<Rigidbody>().isKinematic = true;
                obj.GetComponent<Rigidbody>().useGravity = false;


                //잡힌 오브젝트가 충돌하지 않도록 
                BoxCollider[] BCobj = obj.GetComponentsInChildren<BoxCollider>();
                if (BCobj != null)
                {
                    for (int a = 0; a < BCobj.Length; a++)
                    {
                        BCobj[a].enabled = false;
                    }
                }

                SphereCollider[] SCobj = obj.GetComponentsInChildren<SphereCollider>();
                if (SCobj != null)
                {
                    for (int a = 1; a < SCobj.Length; a++)
                    {
                        SCobj[a].enabled = false;
                    }
                }

                CapsuleCollider[] CCobj = obj.GetComponentsInChildren<CapsuleCollider>();
                if (CCobj != null)
                {
                    for (int a = 0; a < CCobj.Length; a++)
                    {
                        CCobj[a].enabled = false;
                    }
                }

            }
        }
    }

    void DropObject(bool Right)
    {
        GameObject obj;

      
            if (Right)
            {
                obj = GameManager.instance.nearObjectRight;
            }
            else
            {
                obj = GameManager.instance.nearObjectLeft;
            }
        //  isGrabbing = false;
        if (obj != null)
        {
            obj.transform.parent = null;
            obj.GetComponent<Rigidbody>().isKinematic = false;
            obj.GetComponent<Rigidbody>().useGravity = true;

            BoxCollider[] BCobj = obj.GetComponentsInChildren<BoxCollider>();
            if(BCobj!=null)
            {
                for (int a = 0; a < BCobj.Length; a++)
                {
                    BCobj[a].enabled = true;
                }
            }
          
            SphereCollider[] SCobj = obj.GetComponentsInChildren<SphereCollider>();
            if (SCobj != null)
            {
                for (int a = 1; a < SCobj.Length; a++)
                {
                    SCobj[a].enabled = true;
                }
            }

            CapsuleCollider[] CCobj = obj.GetComponentsInChildren<CapsuleCollider>();
            if (CCobj != null)
            {
                for (int a = 0; a < CCobj.Length; a++)
                {
                    CCobj[a].enabled = true;
                }
            }

            obj.GetComponent<Rigidbody>().velocity = GetDeltaPos() * ThrowPower * 2;
            obj.GetComponent<Rigidbody>().angularVelocity = GetAngularVelocity();

            obj.SendMessage("ReFixPosition", SendMessageOptions.DontRequireReceiver);
            //해당 오브젝트가 "어떤"위치에 있는지 체크 하는 함수를 호출
            obj.SendMessage("OnInto", SendMessageOptions.DontRequireReceiver);
            obj = null;
        }
    }

    Vector3 GetAngularVelocity()
    {
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(lastRotation);

        return new Vector3(Mathf.DeltaAngle(0, deltaRotation.eulerAngles.x), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.y), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.z)) * 3;
    }


    //포지션 이동값을 구하는 함수
    Vector3 GetDeltaPos()
    {
        deltaPos = predeltaPos - postdeltapos;

        return deltaPos;
    }
}
