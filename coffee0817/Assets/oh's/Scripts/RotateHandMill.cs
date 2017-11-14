using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHandMill : MonoBehaviour {


    // 사운드
    private AudioSource audioSource;
    private AudioClip audioClip;

    [SerializeField]
    private  Transform wheels;
    public Material met;
    private Material metTemp;

    public float Speed;

    // Use this for initialization
    void Start () {


        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioClip = (AudioClip)Resources.Load("Sounds/grinder");
        Speed = 50.0f;
    
        wheels = GameObject.Find( "ChamferCyl" ).GetComponent<Transform> ();
        met = (Material) Resources.Load ( "Materials/RightHandNear" );
        metTemp = this.gameObject.GetComponentInChildren<MeshRenderer> ().material;

    }

  
    // Update is called once per frame
    void Update () {

    }

    void OnTriggerEnter( Collider col )
    {
            this.gameObject.GetComponentInChildren<MeshRenderer> ().material = met;
    }

    void OnTriggerStay( Collider col )
    {
        if ( col.gameObject.name == "GearVrController" || col.gameObject.name == "controller")
        {
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioClip);
            }
            wheels.Rotate ( Vector3.up * Time.deltaTime * Speed );
        }
    }

    void OnTriggerExit( Collider col )
    {
        this.gameObject.GetComponentInChildren<MeshRenderer> ().material = metTemp;
    }
}
