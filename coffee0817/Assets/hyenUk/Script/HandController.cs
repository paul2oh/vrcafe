using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {


    public OVRInput.Controller ovrController;
   // bool isGrabbing;
    GameObject grabbedObject;

    public float grabRange = 0.18f;

    public LayerMask mask;

    Quaternion currentRotation;
    Quaternion lastRotation;

    public float ThrowPower = 2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(grabbedObject != null)
        {
            lastRotation = currentRotation;
            currentRotation = grabbedObject.transform.rotation;
        }

       

       if ( Input.GetButtonDown("Fire2"))
        {
            CatchObject();
        }
        else if ( Input.GetButtonUp("Fire2"))
        {
            DropObject();
        }

        if ( OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            CatchObject();
        }
        else if ( OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            DropObject();
        }
	}

    void CatchObject()
    {
      //  isGrabbing = true;

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit[] hitInfos = Physics.SphereCastAll(ray, grabRange, 0, mask);

        if(hitInfos.Length >0)
        {
            int nearobj = 0;
            for(int i = 0; i< hitInfos.Length; i++)
            {
                if(hitInfos[i].distance < hitInfos[nearobj].distance)
                {
                    nearobj = i;
                }
            }
            grabbedObject = hitInfos[nearobj].transform.gameObject;
            grabbedObject.transform.position = transform.position;
            grabbedObject.transform.parent = transform;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            grabbedObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    void DropObject()
    {
      //  isGrabbing = false;

        Debug.Log("놓음");
        if (grabbedObject != null)
        {
            grabbedObject.transform.parent = null;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.GetComponent<Rigidbody>().useGravity = true;

            grabbedObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(ovrController) * ThrowPower;
            grabbedObject.GetComponent<Rigidbody>().angularVelocity = GetAngularVelocity();

            grabbedObject = null;
        }

    }

    Vector3 GetAngularVelocity()
    {
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(lastRotation);

        return new Vector3(Mathf.DeltaAngle(0, deltaRotation.eulerAngles.x), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.y), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.z));
    }
}
